namespace Postre;

public class Book
{
    public int Id { get; init; }
    public string Title { get; init; } = null!;
    public string Summary { get; init; } = null!;
    public int Price { get; init; }
}