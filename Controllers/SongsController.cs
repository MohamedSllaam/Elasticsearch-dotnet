using Elasticsearch_dotnet.Contracts;
using Elasticsearch_dotnet.Elastic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController(SearchService _searchService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] SearchSongsRequest request , CancellationToken cancellationToken)
        {
            var parameters = request.ToSearchParameters();
            var songsResponse = await _searchService.SearchAsync(parameters, cancellationToken);
            var songs =  songsResponse.Documents;
            var count = songsResponse.Total;
            var response = new SearchSongsResponse(songs.Select(x => x.ToSongResponse()), request.PageNumber,
            request.PageSize, count, (int)Math.Ceiling(count / (double)request.PageSize));

            return Ok(response);
        }

        [HttpGet("include-score")]
        public async Task<IActionResult> SearchIncludeScore([FromQuery] SearchSongsRequest request , CancellationToken cancellationToken)
        {
            var parameters = request.ToSearchParameters();
            var songsResponse = await _searchService.SearchAsync(parameters, cancellationToken);
            var count = (int)songsResponse.Total;
            var response = new SearchSongsIncludeScoresResponse(songsResponse.Hits.Select(x => x.ToSongResponseWithScore()), request.PageNumber,
            request.PageSize, count, (int)Math.Ceiling(count / (double)request.PageSize));

            return Ok(response);
        }
    }
}
