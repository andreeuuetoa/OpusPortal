﻿using Microsoft.AspNetCore.Mvc;

namespace WebApp.APIControllers.v1._0;

/// <summary>
/// Root page controller. Used for testing and gathering basic information about the application, such as its name and API version.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}")]
public class HomePageController : ControllerBase
{
    /// <summary>
    /// Get app name and version. This always returns an object with values "OpusPortal" and "v1.0".
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ObjectResult GetAppNameAndVersion()
    {
        var information = new
        {
            Name = "OpusPortal",
            Version = "v1.0"
        };
        return Ok(information);
    }
}