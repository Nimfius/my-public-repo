namespace Postre;

public record BookDto
{

    public string Title { get; init; }
    public string Summary { get; init; }
    public int Price { get; init; }
}