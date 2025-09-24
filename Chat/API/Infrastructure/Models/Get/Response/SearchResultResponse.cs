namespace ChatService.API.Infrastructure.Models.Get.Response
{
    public class SearchResultResponse<TObject>
    {
        public List<TObject> Results { get; set; } = new List<TObject>();
        public int Total { get; set; }

        public SearchResultResponse(List<TObject> results)
        {
            Results = results;
            Total = results.Count;
        }
    }
}
