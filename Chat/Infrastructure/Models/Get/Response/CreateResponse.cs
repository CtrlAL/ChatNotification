using ChatService.Domain;

namespace ChatService.Infrastructure.Models.Get.Response
{
    public class CreateResponse : BaseCreateResponse<string>
    {
        public CreateResponse(IMongoModel mongoModel)
        {
            Id = mongoModel.Id;
        }
    }
}
