using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using Base;
using DAL.Repositories;
using DAL.Repositories.Competitions;
using DAL.Repositories.Concerts;
using DAL.Repositories.Contacts;
using DAL.Repositories.Identity;
using DAL.Repositories.Library;
using DAL.Repositories.Studying_logic;

namespace DAL.UnitOfWork;

public class AppUOW : BaseUOW<AppDbContext>, IAppUOW
{
    private IAppRoleRepository? _appRoleRepository;
    private IAppUserRepository? _appUserRepository;
    private IBookRepository? _bookRepository;
    private ICompetitionRepository? _competitionRepository;
    private IConcertRepository? _concertRepository;
    private IContactRepository? _contactRepository;
    private IPersonOnCurriculumRepository? _curriculumRepository;
    private IBookLentOutRepository? _libraryRepository;
    private IPerformanceRepository? _performanceRepository;
    private IPersonRepository? _personRepository;
    private IPersonOnSubjectRepository? _subjectRepository;

    public IAppRoleRepository AppRoleRepository => _appRoleRepository ??= new AppRoleRepository(UowDbContext);
    public IAppUserRepository AppUserRepository => _appUserRepository ??= new AppUserRepository(UowDbContext);
    public IBookRepository BookRepository => _bookRepository ??= new BookRepository(UowDbContext);
    public ICompetitionRepository CompetitionRepository => _competitionRepository ??= new CompetitionRepository(UowDbContext);
    public IConcertRepository ConcertRepository => _concertRepository ??= new ConcertRepository(UowDbContext);
    public IContactRepository ContactRepository => _contactRepository ??= new ContactRepository(UowDbContext);
    public IPersonOnCurriculumRepository PersonOnCurriculumRepository => _curriculumRepository ??= new PersonOnCurriculumRepository(UowDbContext);
    public IBookLentOutRepository BookLentOutRepository => _libraryRepository ??= new BookLentOutRepository(UowDbContext);
    public IPerformanceRepository PerformanceRepository => _performanceRepository ??= new PerformanceRepository(UowDbContext);
    public IPersonRepository PersonRepository => _personRepository ??= new PersonRepository(UowDbContext);
    public IPersonOnSubjectRepository PersonOnSubjectRepository => _subjectRepository ??= new PersonOnSubjectRepository(UowDbContext);

    public AppUOW(AppDbContext context) : base(context)
    {
    }
}