using App.BLL.Contracts.Services;
using App.BLL.Contracts.Services.Library;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using Base;
using Base.BLL;
using DomainBookLentOut = Domain.Library.BookLentOut;
using BLLBookLentOut = BLL.DTO.Library.BookLentOut;

namespace App.BLL.Services.Library;

public class BookLentOutService : BaseEntityService<DomainBookLentOut, BLLBookLentOut, IBookLentOutRepository>, IBookLentOutService
{
    protected readonly IAppUOW Uow;

    public BookLentOutService(IAppUOW uow, IMapper<DomainBookLentOut, BLLBookLentOut> mapper) : base(uow.BookLentOutRepository, mapper)
    {
        Uow = uow;
    }
}