using Microsoft.AspNetCore.Http;
namespace BookStore.Application.Services.Impl
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int GetCartSum()
        {
            return _httpContextAccessor.HttpContext?.Session.GetInt32("sum-cart-detail") ?? 0;
        }

        public void UpdateCartSum(int sum)
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                _httpContextAccessor.HttpContext.Session.SetInt32("sum-cart-detail", sum);
            }
        }
    }
}
