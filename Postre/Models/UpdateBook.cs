using System.ComponentModel.DataAnnotations;

namespace Postre.Models;

public record UpdateBook
{
    [Required] public string Title { get; init; }
    [Required] public string Summary { get; init; }
    [Required] public int Price { get; init; }
}