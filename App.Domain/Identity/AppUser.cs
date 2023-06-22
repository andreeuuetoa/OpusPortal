using Base.Domain.Contracts;
using Domain.Competitions;
using Domain.Concerts;
using Domain.Library;
using Domain.Studying_logic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId
{
    public Guid AppRoleId { get; set; }
    public AppRole? AppRole { get; set; }

    public Guid? PersonId { get; set; }
    public Person? Person { get; set; }

    public DateTime From { get; set; }
    public DateTime? Until { get; set; }

    public ICollection<BookLentOut>? BooksLentOut { get; set; }
    public ICollection<PersonAtConcert>? PersonAtConcerts { get; set; }
    public ICollection<PersonAtCompetition>? PersonAtCompetitions { get; set; }
    public ICollection<PersonOnCurriculum>? PersonOnCurricula { get; set; }
    public ICollection<PersonOnSubject>? PersonSubjects { get; set; }
    public ICollection<SubjectTeacher>? SubjectTeachers { get; set; }

    public ICollection<RefreshToken>? AppRefreshTokens { get; set; } 
}