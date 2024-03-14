using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ToDoList.Models;


namespace ToDoList.Web._services
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public bool IsRegistered()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var userId = session.GetString("UserSession");
            return !string.IsNullOrEmpty(userId);
        }

        public bool SetRegisteredSession(Guid guid)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.SetString("UserSession", guid.ToString());
            return IsRegistered();
        }

        public Guid? GetUserId()
        {
            var session = _httpContextAccessor.HttpContext.Session;

            var userId = session.GetString("UserSession");
            if (Guid.TryParse(userId, out Guid parsedUserId))
            {
                return parsedUserId;
            }
            else
            {
                return null;
            }
        }

    }
}
