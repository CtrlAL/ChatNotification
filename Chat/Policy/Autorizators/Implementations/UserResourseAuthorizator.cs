using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.Domain.Interfaces;
using ChatService.Policy.Autorizators.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ChatService.Policy.Autorizators.Implementations
{
    public class UserResourseAuthorizator<TResourseRepository, TResourse, TFilter> 
        : IUserResourseAuthorizator<TResourseRepository, TResourse, TFilter>
        where TResourse : class, IUserResourse, IMongoModel
        where TResourseRepository : class, IMongoRepository<TResourse, TFilter>
    {
        protected readonly TResourseRepository _repository;
        private readonly IAuthorizationService _authorizationService;

        public UserResourseAuthorizator(TResourseRepository repository, IAuthorizationService authorizationService)
        {
            _repository = repository;
            _authorizationService = authorizationService;
        }

        public async Task<AuthorizationResult> AuthorizeAsync(string resourseId, ClaimsPrincipal user)
        {
            var resourse = await _repository.GetAsync(resourseId);

            if (resourse == null)
            {
                return AuthorizationResult.Failed();
            }

            var authResult = await _authorizationService.AuthorizeAsync(user, resourse, PolicyNames.ResourceOwner);

            return authResult;
        }

        public async Task<AuthorizationResult> AuthorizeAsync(IUserResourse resourse, ClaimsPrincipal user)
        {
            var authResult = await _authorizationService.AuthorizeAsync(user, resourse, PolicyNames.ResourceOwner);

            return authResult;
        }
    }
}
