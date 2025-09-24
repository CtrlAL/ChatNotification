namespace ChatService.API.Infrastructure.Models.Get.Response
{
    public abstract class BaseCreateResponse<TId>
    {
        public TId Id {  get; set; }
    }
}
