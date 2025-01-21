using TietoEvry.Shared.Services.Interfaces;

namespace MauiApp1test;

public class AnalyticsService: IAnalyticsService
{
    public void LogEvent(string eventId)
    {
       // throw new NotImplementedException();
    }

    public void LogEvent(string eventId, string paramName, string value)
    {
        //throw new NotImplementedException();
    }

    public void LogEvent(string eventName, IDictionary<string, object> parameters)
    {
       // throw new NotImplementedException();
    }

    public void SetUserProperty(string name, string value)
    {
       // throw new NotImplementedException();
    }

    public void SetCurrentScreen(string screenName, string screenClass)
    {
       // throw new NotImplementedException();
    }

    public bool IsTabbedPageViewModel(string viewModelName)
    {
        return true;
    }
}