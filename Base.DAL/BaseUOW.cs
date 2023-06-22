using Base.Contracts;
using Base.DAL.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Base;

public abstract class BaseUOW<TDbContext> : IBaseUOW
    where TDbContext : DbContext
{
    protected readonly TDbContext UowDbContext;

    protected BaseUOW(TDbContext context)
    {
        UowDbContext = context;
    }
    
    public virtual async Task<int> SaveChanges()
    {
        return await UowDbContext.SaveChangesAsync();
    }
}
