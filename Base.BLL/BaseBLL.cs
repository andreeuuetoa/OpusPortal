using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;

namespace Base.BLL;

public abstract class BaseBLL<TUOW> : IBaseBLL
    where TUOW: IBaseUOW
{
    protected readonly IBaseUOW uow;

    protected BaseBLL(IBaseUOW uow)
    {
        this.uow = uow;
    }

    public virtual async Task<int> SaveChanges()
    {
        return await uow.SaveChanges();
    }
}