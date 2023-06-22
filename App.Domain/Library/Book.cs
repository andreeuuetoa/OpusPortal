using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace Domain.Library;

public class Book : DomainEntityId
{
    [MinLength(1)]
    [MaxLength(256)]
    public string Title { get; set; } = default!;

    [MinLength(1)]
    [MaxLength(256)]
    public string Authors { get; set; } = default!;

    public int YearReleased { get; set; }

    public ICollection<BookLentOut>? BooksLendedOut { get; set; }
}