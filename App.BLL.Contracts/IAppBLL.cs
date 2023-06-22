using App.BLL.Contracts.Services;
using App.BLL.Contracts.Services.Identity;
using App.BLL.Contracts.Services.Library;
using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IAppBLL : IBaseBLL
{
    IBookService BookService { get; }
    IBookLentOutService BookLentOutService { get; }
    IPersonService PersonService { get; }
}