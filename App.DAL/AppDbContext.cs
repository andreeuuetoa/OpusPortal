using Domain;
using Domain.Competitions;
using Domain.Concerts;
using Domain.Contacts;
using Domain.Identity;
using Domain.Institutions;
using Domain.Library;
using Domain.Studying_logic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Category> Category { get; set; } = default!;
    public DbSet<Competition> Competition { get; set; } = default!;
    public DbSet<CompetitionOrganizer> CompetitionOrganizer { get; set; } = default!;
    public DbSet<Jury> Jury { get; set; } = default!;
    public DbSet<PersonAtCompetition> PersonAtCompetition { get; set; } = default!;
    public DbSet<PersonInJury> PersonInJury { get; set; } = default!;
    public DbSet<Round> Round { get; set; } = default!;
    public DbSet<Concert> Concert { get; set; } = default!;
    public DbSet<ConcertOrganizer> ConcertOrganizer { get; set; } = default!;
    public DbSet<PersonAtConcert> PersonAtConcert { get; set; } = default!;
    public DbSet<Book> Book { get; set; } = default!;
    public DbSet<BookLentOut> BookLentOut { get; set; } = default!;
    public DbSet<Class> Class { get; set; } = default!;
    public DbSet<ClassInRoom> ClassInRoom { get; set; } = default!;
    public DbSet<Curriculum> Curriculum { get; set; } = default!;
    public DbSet<PersonOnCurriculum> PersonOnCurriculum { get; set; } = default!;
    public DbSet<Room> Room { get; set; } = default!;
    public DbSet<Subject> Subject { get; set; } = default!;
    public DbSet<PersonOnSubject> PersonSubject { get; set; } = default!;
    public DbSet<SubjectInCurriculum> SubjectInCurriculum { get; set; } = default!;
    public DbSet<SubjectTeacher> SubjectTeacher { get; set; } = default!;
    public DbSet<TeacherInClass> TeacherInClass { get; set; } = default!;
    public DbSet<Institution> Institution { get; set; } = default!;
    public DbSet<InstitutionType> InstitutionType { get; set; } = default!;
    public DbSet<InstitutionAcronym> InstitutionAcronym { get; set; } = default!;
    public DbSet<Person> Person { get; set; } = default!;
    public DbSet<AppUser> AppUser { get; set; } = default!;
    public DbSet<AppRole> AppRole { get; set; } = default!;
    public DbSet<RefreshToken> AppRefreshTokens { get; set; } = default!;
    public DbSet<Contact> Contact { get; set; } = default!;
    public DbSet<ContactType> ContactType { get; set; } = default!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // let the initial stuff run
        base.OnModelCreating(builder);
        
        // disable cascade delete
        foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}