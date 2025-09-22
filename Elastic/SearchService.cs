using Nest;

namespace Elasticsearch_dotnet.Elastic;

public class SearchService(IElasticClient _elasticClient)
{

    public async Task<ISearchResponse<ElasticSong>> SearchAsync(SearchParameters parameters
        ,CancellationToken cancellationToken)
    {
        var result = await _elasticClient.SearchAsync<ElasticSong>(
            x => x.Query(q => q.Bool(b => b.Should(s => s.MultiMatch(
                m => m.Fields(f => f
                  .Field(ff => ff.Title)
                  .Field(ff => ff.AlbumTitle)
                  .Field(ff => ff.ArtistName , boost:3)
                  )
                .Query(parameters.SearchText)
                .Fuzziness(Fuzziness.Auto)
                )
            )
            .MinimumShouldMatch(1)
            .Filter(f => f
            .Term(t => t.Genre, parameters.Genre)))).Sort(s => s.Descending(SortSpecialField.Score))
            .Skip(parameters.Skip)
            .Take(parameters.Take),         
            cancellationToken);



        return result;
    }
}
