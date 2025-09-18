using Elasticsearch_dotnet.Persistence;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace Elasticsearch_dotnet.Elastic
{
    public class InitializeIndexService(IAppDbContext _appDbContext, IElasticClient _elasticClient , IConfiguration _configuration)
    {

        public async Task Run()
        {
            var index = _configuration.GetValue<string>("Elastic:Index");

            await _elasticClient.Indices.DeleteAsync(index);

            var response = await _elasticClient.Indices.CreateAsync(index
                , x => x.Map<ElasticSong>(xx => xx.AutoMap()));

            var songs = await _appDbContext.Songs.AsNoTracking()
                .Include(x=>x.Album)
                .ThenInclude(x=>x.Genre)
                .Include(x=>x.Album)
                .ThenInclude(x => x.Artist)
                .ToListAsync();

            var elasticSongs = songs.Select(x => x.ToElasticSong());

            await _elasticClient.BulkAsync(x =>
            x.Index(index)
            .IndexMany(elasticSongs));

            

        }
    }
}
