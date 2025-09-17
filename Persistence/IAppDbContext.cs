using Microsoft.EntityFrameworkCore;
using Elasticsearch_dotnet.Entities;

namespace Elasticsearch_dotnet.Persistence;

public interface IAppDbContext
{
    DbSet<Artist> Artists { get; set; }
    DbSet<Album> Albums { get; set; }
    DbSet<Song> Songs { get; set; }
    DbSet<Genre> Genres { get; set; }
    Task<int> SaveAsync(CancellationToken cancellationToken = default);
}