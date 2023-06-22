using App.DAL.Contracts.Repositories;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IAppUOW : IBaseUOW
{
    public IAppRoleRepository AppRoleRepository { get; }
    public IAppUserRepository AppUserRepository { get; }
    public IBookRepository BookRepository { get; }
    public ICompetitionRepository CompetitionRepository { get; }
    public IConcertRepository ConcertRepository { get; }
    public IContactRepository ContactRepository { get; }
    public IPersonOnCurriculumRepository PersonOnCurriculumRepository { get; }
    public IBookLentOutRepository BookLentOutRepository { get; }
    public IPerformanceRepository PerformanceRepository { get; }
    public IPersonRepository PersonRepository { get; }
    public IPersonOnSubjectRepository PersonOnSubjectRepository { get; }
}