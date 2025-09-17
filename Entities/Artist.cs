namespace Elasticsearch_dotnet.Entities;

public class Artist
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public IEnumerable<Album> Albums { get; set; } = new List<Album>();
}