using ChatService.Domain.Interfaces;

namespace ChatService.API.Infrastructure.Models.Get.Response
{
    public class CreateResponse : BaseCreateResponse<string>
    {
        public CreateResponse(IMongoModel mongoModel)
        {
            Id = mongoModel.Id;
        }
    }
}
