using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using App.BLL.Contracts;
using App.DAL.Contracts;
using Asp.Versioning;
using Base.Helpers;
using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Public.DTO.v1._0;
using Public.DTO.v1._0.Identity;
using AppRefreshToken = Domain.Identity.RefreshToken;
using AppRole = Domain.Identity.AppRole;
using AppUser = Domain.Identity.AppUser;
using Person = Domain.Identity.Person;

namespace WebApp.APIControllers.v1._0.Identity;

/// <summary>
/// Serve the HTTPS requests coming from the users of OpusPortal - sign in, sign out, register, and add a refresh token.
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/identity/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;
    private readonly IAppBLL _bll;

    /// <summary>
    /// Create an instance of AccountController.
    /// </summary>
    /// <param name="signInManager"></param>
    /// <param name="userManager"></param>
    /// <param name="roleManager"></param>
    /// <param name="configuration"></param>
    /// <param name="context"></param>
    /// <param name="bll"></param>
    public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration configuration, AppDbContext context, IAppBLL bll)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _context = context;
        _bll = bll;
    }

    /// <summary>
    /// Register new user.
    /// </summary>
    /// <param name="registrationData"></param>
    /// <returns>The result of trying to add a new user to the database.</returns>
    /// <response code="201">New user was successfully created.</response>
    /// <response code="400">User was already created, its creation failed or its claims creation failed.</response>
    /// <response code="404">The role needed to register user was not found from the database.</response>
    [HttpPost]
    [Produces(contentType: MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(JWTResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IdentityResult>> Register([FromBody] RegistrationData registrationData)
    {
        // Is user already registered?
        var registeredUser = await _userManager.FindByEmailAsync(registrationData.Email);
        if (registeredUser != null)
        {
            return BadRequest(new RestApiErrorResponse
            {
                Status = HttpStatusCode.BadRequest,
                Error = $"User with email {registrationData.Email} is already registered!"
            });
        }
        
        // Register user
        // TODO!! Support multiple roles!
        var newUserRole = await _roleManager.FindByNameAsync("Student");
        if (newUserRole == null)
        {
            return NotFound(new RestApiErrorResponse
            {
                Status = HttpStatusCode.BadRequest,
                Error = $"App role '{registrationData.AppRoleName}' not found!"
            });
        }

        var userPerson = _context.Person.FirstOrDefault(p => 
            p.FirstName == registrationData.FirstName && 
            p.LastName == registrationData.LastName
        ) ?? new Person
        {
            FirstName = registrationData.FirstName,
            LastName = registrationData.LastName
        };

        var appUser = new AppUser
        {
            Email = registrationData.Email,
            UserName = registrationData.Email,
            AppRoleId = newUserRole.Id,
            AppRole = newUserRole,
            PersonId = userPerson.Id,
            Person = userPerson,
            From = DateTime.UtcNow,
            AppRefreshTokens = new List<AppRefreshToken>()
        };

        var claimsResult = await _userManager.AddClaimsAsync(appUser, new List<Claim>
        {
            new(ClaimTypes.GivenName, appUser.Person!.FirstName),
            new(ClaimTypes.Surname, appUser.Person!.LastName)
        });

        if (!claimsResult.Succeeded)
        {
            return BadRequest(new RestApiErrorResponse
            {
                Status = HttpStatusCode.BadRequest,
                Error = claimsResult.Errors.First().Description
            });
        }

        var userResult = await _userManager.CreateAsync(appUser, registrationData.Password);

        if (!userResult.Succeeded)
        {
            return BadRequest(new RestApiErrorResponse
            {
                Status = HttpStatusCode.BadRequest,
                Error = userResult.ToString()
            });
        }
        
        return Created("api/v1.0/Identity/Account/Register", userResult);
    }

    /// <summary>
    /// Sign in user.
    /// </summary>
    /// <param name="signInData"></param>
    /// <returns>The JWT the signed in user can send HTTP requests to the server with.</returns>
    /// <response code="200">Sign-in was successful.</response>
    /// <response code="400">Sign-in failed due to the user credentials not matching.</response>
    /// <response code="404">User with the input email was not found.</response>
    [HttpPost]
    [Produces(contentType: MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(JWTResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JWTResponse>> SignIn(SignInData signInData)
    {
        var appUser = await _userManager.FindByEmailAsync(signInData.Email);

        if (appUser == null)
        {
            return NotFound(new RestApiErrorResponse
            {
                Status = HttpStatusCode.NotFound,
                Error = "User not found!"
            });
        }

        // Verify username and password
        var result = await _signInManager.CheckPasswordSignInAsync(appUser, signInData.Password, false);
        if (!result.Succeeded)
        {
            await Task.Delay(new Random().Next(100, 1000));
            return BadRequest(new RestApiErrorResponse
            {
                Status = HttpStatusCode.BadRequest,
                Error = $"Sign-in failed. Error: {result}"
            });
        }
        
        // Search for user refresh tokens from the database
        appUser.AppRefreshTokens = await _context
            .Entry(appUser)
            .Collection(u => u.AppRefreshTokens!)
            .Query()
            .Where(t => t.AppUserId == appUser.Id)
            .ToListAsync();
        
        // Remove expired refresh tokens
        foreach (var userRefreshToken in appUser.AppRefreshTokens)
        {
            if (userRefreshToken.ExpirationDateTime < DateTime.UtcNow && (
                    userRefreshToken.PreviousExpirationDateTime == null ||
                    userRefreshToken.PreviousExpirationDateTime < DateTime.UtcNow
                ))
            {
                _context.AppRefreshTokens.Remove(userRefreshToken);
            }
        }
        
        var refreshToken = new AppRefreshToken
        {
            AppUserId = appUser.Id,
            AppUser = appUser,
            RefreshToken = Guid.NewGuid().ToString(),
            ExpirationDateTime = DateTime.UtcNow.AddDays(_configuration.GetValue<int>("RefreshToken:ExpiresInDays"))
        };

        _context.AppRefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();
        
        // Get claims-based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        
        // Generate JWT
        var JWT = IdentityHelpers.GenerateJWT(
            claimsPrincipal.Claims,
            _configuration.GetValue<string>("JWT:Key")!,
            _configuration.GetValue<string>("JWT:Issuer")!,
            _configuration.GetValue<string>("JWT:Audience")!,
            _configuration.GetValue<int>("JWT:ExpiresInSeconds")
        );

        // Return result
        var responseResult = new JWTResponse
        {
            JWT = JWT,
            RefreshToken = refreshToken.RefreshToken
        };

        return Ok(responseResult);
    }

    /// <summary>
    /// Create a refresh token for an already logged in user so that his latest valid JWT can be updated.
    /// </summary>
    /// <param name="refreshTokenModel"></param>
    /// <returns>Refresh token for logged in user.</returns>
    /// <response code="200">Sign-in was successful.</response>
    /// <response code="400">Sign-in failed due to the user credentials not matching.</response>
    /// <response code="404">User with the input email was not found.</response>
    [HttpPost]
    [Produces(contentType: MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(JWTResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> RefreshToken(RefreshTokenModel refreshTokenModel)
    {
        JwtSecurityToken JWTToken;
        try
        {
            JWTToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshTokenModel.JWT);
            if (JWTToken == null)
            {
                return BadRequest(new RestApiErrorResponse
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "No token!"
                });
            }
        }
        catch (Exception e)
        {
            return BadRequest($"Unable to parse incoming refresh token: {e.Message}");
        }

        if (!IdentityHelpers.IsTokenValid(
                refreshTokenModel.JWT,
                _configuration.GetValue<string>("JWT:Key")!,
                _configuration.GetValue<string>("JWT:Issuer")!,
                _configuration.GetValue<string>("JWT:Audience")!
            ))
        {
            return BadRequest("JWT is not valid!");
        }

        var userEmail = JWTToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        if (userEmail == null)
        {
            return BadRequest(new RestApiErrorResponse
            {
                Status = HttpStatusCode.BadRequest,
                Error = "Email not found from the JWT!"
            });
        }

        var appUser = await _userManager.FindByEmailAsync(userEmail);
        if (appUser == null)
        {
            return NotFound(new RestApiErrorResponse
            {
                Status = HttpStatusCode.NotFound,
                Error = $"User with email {userEmail} was not found!"
            });
        }
        
        // load and compare refresh tokens
        appUser.AppRefreshTokens = 
            await _context.Entry(appUser)
                .Collection(u => u.AppRefreshTokens!)
                .Query()
                .Where(refreshToken =>
                    (refreshToken.RefreshToken == refreshTokenModel.RefreshToken && refreshToken.ExpirationDateTime > DateTime.UtcNow) ||
                    (refreshToken.PreviousRefreshToken == refreshTokenModel.RefreshToken &&
                     refreshToken.PreviousExpirationDateTime > DateTime.UtcNow)
                )
                .ToListAsync();

        if (appUser.AppRefreshTokens.IsNullOrEmpty())
        {
            return NotFound("There are no refresh tokens found based on the user!");
        }

        if (appUser.AppRefreshTokens.Count != 1)
        {
            return NotFound("More than one valid refresh token found!");
        }
        
        // Get claims-based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        
        // Generate JWT
        var JWT = IdentityHelpers.GenerateJWT(
            claimsPrincipal.Claims,
            _configuration.GetValue<string>("JWT:Key")!,
            _configuration.GetValue<string>("JWT:Issuer")!,
            _configuration.GetValue<string>("JWT:Audience")!,
            _configuration.GetValue<int>("JWT:ExpiresInSeconds")
        );
        
        // Make a new refresh token, but keep the old one still valid for some time
        var refreshToken = appUser.AppRefreshTokens.First();
        if (refreshToken.RefreshToken == refreshTokenModel.RefreshToken)
        {
            refreshToken.PreviousRefreshToken = refreshTokenModel.RefreshToken;
            refreshToken.PreviousExpirationDateTime = DateTime.UtcNow.AddMinutes(1);

            refreshToken.RefreshToken = Guid.NewGuid().ToString();
            refreshToken.ExpirationDateTime = DateTime.UtcNow.AddDays(_configuration.GetValue<int>("RefreshToken:ExpiresInDays"));

            await _context.SaveChangesAsync();
        }

        var response = new JWTResponse
        {
            JWT = JWT,
            RefreshToken = refreshToken.RefreshToken
        };

        return Ok(response);
    }

    /// <summary>
    /// Change user password.
    /// </summary>
    /// <param name="passwordChangeData"></param>
    /// <returns></returns>
    /// <response code="200">The password for the user was successfully changed.</response>
    /// <response code="404">Something went wrong.</response>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ChangePassword(PasswordChangeData passwordChangeData)
    {
        var appUser = await _userManager.FindByEmailAsync(passwordChangeData.Email);
        
        if (appUser == null)
        {
            return NotFound(new RestApiErrorResponse
            {
                Status = HttpStatusCode.NotFound,
                Error = "User not found!"
            });
        }

        // Verify username and password
        var result = await _userManager.CheckPasswordAsync(appUser, passwordChangeData.CurrentPassword);
        if (!result)
        {
            await Task.Delay(new Random().Next(100, 1000));
            return BadRequest(new RestApiErrorResponse
            {
                Status = HttpStatusCode.BadRequest,
                Error = "The username and password do not match."
            });
        }

        // Check that the new password matches with the confirm password.
        if (passwordChangeData.NewPassword != passwordChangeData.ConfirmPassword)
        {
            await Task.Delay(new Random().Next(100, 1000));
            return BadRequest(new RestApiErrorResponse
            {
                Status = HttpStatusCode.BadRequest,
                Error = "The passwords do not match."
            });
        }

        await _userManager.ChangePasswordAsync(appUser, passwordChangeData.CurrentPassword,
            passwordChangeData.NewPassword);

        return Ok();
    }

    /// <summary>
    /// Sign out user.
    /// </summary>
    /// <param name="signOutData"></param>
    /// <returns></returns>
    /// <response code="200">Sign-out was successful.</response>
    /// <response code="404">User is not logged in.</response>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JWTResponse>> SignOut(SignOutData signOutData)
    {
        var userId = User.GetUserId();

        var appUser = await _context.Users
            .Where(u => u.Id == userId)
            .SingleOrDefaultAsync();

        if (appUser == null)
        {
            return NotFound(new RestApiErrorResponse
            {
                Status = HttpStatusCode.NotFound,
                Error = "User is not logged in!"
            });
        }
        
        await _context.Entry(appUser)
            .Collection(u => u.AppRefreshTokens!)
            .Query()
            .Where(refreshToken =>
                refreshToken.RefreshToken == signOutData.RefreshToken ||
                refreshToken.PreviousRefreshToken == signOutData.RefreshToken
            )
            .ToListAsync();

        foreach (var userRefreshToken in appUser.AppRefreshTokens!)
        {
            _context.AppRefreshTokens.Remove(userRefreshToken);
        }

        var deleteCount = await _context.SaveChangesAsync();
        
        return Ok(new {TokenDeleteCount = deleteCount});
    }
}