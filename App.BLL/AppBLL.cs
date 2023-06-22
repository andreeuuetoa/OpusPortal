using App.BLL.Contracts;
using App.BLL.Contracts.Services;
using App.BLL.Contracts.Services.Identity;
using App.BLL.Contracts.Services.Library;
using App.BLL.Mappers.Identity;
using App.BLL.Mappers.Library;
using App.BLL.Services.Identity;
using App.BLL.Services.Library;
using App.DAL.Contracts;
using AutoMapper;
using Base.BLL;

namespace App.BLL;

public class AppBLL : BaseBLL<IAppUOW>, IAppBLL
{
    private IAppUOW _uow;
    private readonly IMapper _mapper;

    private IAppUserService? _appUserService;
    private IBookService? _bookService;
    private IBookLentOutService? _bookLentOutService;
    private IPersonService? _personService;
    
    public AppBLL(IAppUOW uow, IMapper mapper) : base(uow)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public IAppUserService AppUserService => _appUserService ??= new AppUserService(_uow, new AppUserMapper(_mapper));
    public IBookService BookService => _bookService ??= new BookService(_uow, new BookMapper(_mapper));
    public IBookLentOutService BookLentOutService => _bookLentOutService ??= new BookLentOutService(_uow, new BookLentOutMapper(_mapper));
    public IPersonService PersonService => _personService ??= new PersonService(_uow, new PersonMapper(_mapper));
}