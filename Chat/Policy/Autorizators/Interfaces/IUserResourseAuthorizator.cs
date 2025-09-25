using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ChatService.Policy.Autorizators.Interfaces
{
    public interface IUserResourseAuthorizator<TResourseRepository, TResourse, TFilter>
        where TResourse : class, IUserResourse, IMongoModel
        where TResourseRepository : class, IMongoRepository<TResourse, TFilter>
    {
        Task<AuthorizationResult> AuthorizeAsync(string resourseId, ClaimsPrincipal user);
        Task<AuthorizationResult> AuthorizeAsync(IUserResourse resourse, ClaimsPrincipal user);
    }
}
