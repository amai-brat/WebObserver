using Hangfire.Dashboard;

namespace WebObserver.Main.API.Helpers;

public class AnonymousAuthorizaiontFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        return true;
    }
}