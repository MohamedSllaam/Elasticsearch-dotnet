using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Elasticsearch_dotnet.Entities;

namespace Elasticsearch_dotnet.Persistence.Configurations;

public class SongConfiguration : IEntityTypeConfiguration<Song>
{
    public void Configure(EntityTypeBuilder<Song> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Album)
            .WithMany(x => x.Songs)
            .HasForeignKey(x => x.AlbumId);
    }
}