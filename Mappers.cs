using Elasticsearch_dotnet.Elastic;
using Elasticsearch_dotnet.Entities;

namespace Elasticsearch_dotnet
{
    public static class Mappers
    {
        public static ElasticSong ToElasticSong(this Song song)
        => new()
        {
            Id = song.Id,
            Title = song.Title,
            AlbumTitle = song.Album!.Title,
            AlbumReleaseDate = song.Album.ReleaseDate.ToString("yyyy-MM-dd"),
            ArtistName = song.Album.Artist!.Name,
            Genre = song.Album.Genre!.Name
        };


    }
}
