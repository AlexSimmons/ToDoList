
namespace ToDoList.Web._services
{
    public interface ISessionService
    {
        Guid? GetUserId();
        bool IsRegistered();
        bool SetRegisteredSession(Guid guid);
    }
}