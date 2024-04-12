using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using GzipClient;
using TietoEvry.Shared.Helpers;

namespace TietoEvry.Shared.Services;

public interface IMyRestClient
{
    Task<Result<T>> ExecutePostAsync<T>(string quary, object? data = null, Dictionary<string, string>? parameters = null, bool autoLogInOnUnauthorized = true);
   // Task<Result<bool>> ExecutePostBoolAsync(string quary, object? data = null, Dictionary<string, string>? parameters = null, bool autoLogInOnUnauthorized = true);
   // Task<Result<T>> ExecuteGetAsync<T>(string quary, Dictionary<string, string>? parameters = null, bool autoLogInOnUnauthorized = true);
   // Task<Result<bool>> ExecuteDeleteBoolAsync(string quary, Dictionary<string, string>? parameters = null, bool autoLogInOnUnauthorized = true);
   // Task<Result<T>> ExecuteDeleteAsync<T>(string quary, Dictionary<string, string>? parameters = null, bool autoLogInOnUnauthorized = true);
   // Task<Result<T>> ExecutePatchAsync<T>(string quary, object? data = null, Dictionary<string, string>? parameters = null, bool autoLogInOnUnauthorized = true);
    public bool RemoveTokens();
    public void ReInit();
    public void ClearCookieContainer();
    Result<bool> UpdateToken(Dictionary<string, string> defaultRequestHeaders);
    
}
public interface IMyRestClientGeneric
{
    Task<bool> SilentLogin(bool navigateToLoginOnError = false);
}

public class MyRestClient : IMyRestClient
{
    HttpClient client;
    private bool initIsDone;
    private readonly JsonSerializerOptions jsonOptions;
    private readonly bool logTimeToDebugWindows = true;
    private HttpsClientHandlerService httpservice = new HttpsClientHandlerService();


    public MyRestClient()
    {
        //this.client = new HttpClient(httpservice.GetPlatformMessageHandler());

        this.client = new HttpClient();
       
        jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };
    }

    #region Public
    

    public void ReInit()
    {

        this.client = new HttpClient();
#if IOS
        this.client.BaseAddress = new Uri("https://gzipbug.azurewebsites.net/");
#else
        this.client.BaseAddress = new Uri("https://gzipbug.azurewebsites.net/");
#endif
        this.client.Timeout = new TimeSpan(0, 0, 10);

        this.client.DefaultRequestHeaders.Accept.Clear();
        this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        this.client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

        this.client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

        //if (ApiSettings.defaultRequestHeaders != null)
        //    foreach (var header in ApiSettings.defaultRequestHeaders)
        //    {
        //        client.DefaultRequestHeaders.Add(header.Key, header.Value);
        //    }



        this.initIsDone = true;
    }
    public Result<bool> UpdateToken(Dictionary<string, string> defaultRequestHeaders)
    {
        try
        {
            foreach (var header in defaultRequestHeaders)
            {
                this.client.DefaultRequestHeaders.Remove(header.Key);
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
            return new Result<bool>(true);
        }
        catch (Exception e)
        {

            return new Result<bool>(e.Message);
        }

    }
    public void ClearCookieContainer()
    {
        // DependencyService.Resolve<IMyHttpClientHandler>().ClearCookieContainer(_httpClientHandler);

    }
    private void LogExecuteTime(DateTime start, string quary)
    {
#if DEBUG
        //this.logService.ConsoleWriteLineDebug(String.Format("Log Api: {0} milliseconds for Api call:{1}", (DateTime.Now - start).TotalMilliseconds.ToString(), quary));
#endif
    }
   
    public async Task<Result<T>> ExecutePostAsync<T>(string quary, object? data = null, Dictionary<string, string>? parameters = null, bool autoLogInOnUnauthorized = true)
    {
        DateTime logTime = DateTime.Now;
        int retryCount = 0;
        while (retryCount < 3)
        {
            retryCount++;
            try
            {
                //This so only run this when needded 
                if (!initIsDone)
                    ReInit();
                //if (this.internetConnectionHelper.InternetAccess())
                //{
                    StringContent content = new StringContent("");
                    if (data != null) //, Encoding.UTF8, "application/json")
                        content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

                    if (parameters != null && retryCount == 1)
                        quary = AddParameters(quary, parameters);

                    //Logger
                    logTime = DateTime.Now;


                    var result = await this.client.PostAsync(quary, content);

                    if (this.logTimeToDebugWindows)
                        LogExecuteTime(logTime, quary);

                    using (var stream = await result.Content.ReadAsStreamAsync())
                    {
                        if (result.IsSuccessStatusCode)
                        {
                            var model = await JsonSerializer.DeserializeAsync<T>(stream, jsonOptions);
                            if (model is null)
                                return new Result<T>("Unable to Deserialize model");
                            else
                                return new Result<T>(model);
                        }
                        else if (result.StatusCode == HttpStatusCode.Unauthorized)
                        {

                            if (autoLogInOnUnauthorized)
                            {

                                //var resultLogin = await myRestClientGeneric.SilentLogin(true);
                                //if (resultLogin)
                                //    return await ExecutePostAsync<T>(quary, data, null, false);
                            }
                            return new Result<T>("TmResources.User_Need_To_Log_In_Again");
                        }
                        else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            return new Result<T>("TmResources.Current_API_Not_Found_On_Server");
                        }
                        else if (result.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                        {
                            return new Result<T>("TmResources.Server_Error");
                        }
                        else
                        {
                            try
                            {
                                var t = await JsonSerializer.DeserializeAsync<GenericResponse>(stream, jsonOptions);
                                if (t is null)
                                    return new Result<T>("Unable to convert to GenericResponse");
                                else
                                    return new Result<T>(t);
                            }
                            catch (Exception ex)
                            {
                                var logdic = new Dictionary<string, string>();
                                logdic.Add("Info", "Error in ExecutePostAsync");
                                logdic.Add("Query", quary);
                               // this.logService.ReportErrorToAppCenter(ex, logdic);
                                return new Result<T>("TmResources.Server_Error + Environment.NewLine + result.Content.ReadAsStringAsync().Result");
                            }
                        }
                    }
                //}
                //else
                //{
                //    if (retryCount < ApiSettings.RequestRetryAttempts)
                //        await Task.Delay(ApiSettings.RequestRetryAttemptsSleeptime * 1000);
                //    else
                //        return new Result<T>(TmResources.No_Internet);
                //}
            }
            catch (Exception ex)
            {
                var logdic = new Dictionary<string, string>();
                logdic.Add("Info", "Error in ExecutePostAsync");
                logdic.Add("Query", quary);
                logdic.Add("QueryTime", (DateTime.Now - logTime).TotalSeconds.ToString());
                logdic.Add("RequestAttempts", retryCount.ToString());
                //this.logService.ReportErrorToAppCenter(ex, logdic);

                //if (retryCount < ApiSettings.RequestRetryAttempts)
                //    await Task.Delay(ApiSettings.RequestRetryAttemptsSleeptime * 1000);
                //else
                    return new Result<T>(ex.Message);
            }
        }
        return new Result<T>("Logic error in code");
    }

       public bool RemoveTokens()
    {
        this.client.DefaultRequestHeaders.Remove("AUTHENTICATION_TOKEN");
        this.client.DefaultRequestHeaders.Remove("Authorization");
        return true;

    }
    //public bool AddTokens()
    //{
    //    //_client.DefaultRequestHeaders.Add("AUTHENTICATION_TOKEN", _settingsService.AuthAccessToken);
    //    //_client.DefaultRequestHeaders.Add(AppConstants.Authorization, string.Format("Bearer {0}", _settingsService.AuthAccessToken));
    //    return true;
    //}


#endregion
    #region Private
    private string AddParameters(string query, Dictionary<string, string> parameters)
    {
        bool first = true;

        foreach (var item in parameters)
        {
            if (first)
            {
                var array = item.Value.Split(Convert.ToChar(Environment.NewLine));
                if (array.Count() > 1)
                {
                    foreach (var itemstrig in array)
                    {
                        query = query + string.Format(@"?{0}={1}", item.Key, System.Web.HttpUtility.UrlEncode(itemstrig));
                    }
                }
                else
                {
                    query = query + string.Format(@"?{0}={1}", item.Key, System.Web.HttpUtility.UrlEncode(item.Value));
                }

                first = false;
            }
            else
            {
                var array = item.Value.Split(Convert.ToChar(Environment.NewLine));
                if (array.Count() > 1)
                {
                    foreach (var itemstrig in array)
                    {
                        query = query + string.Format(@"&{0}={1}", item.Key, System.Web.HttpUtility.UrlEncode(itemstrig));
                    }
                }
                else
                {
                    query = query + string.Format(@"&{0}={1}", item.Key, System.Web.HttpUtility.UrlEncode(item.Value));
                }

            }

        }
        return query;

    }
    #endregion


}

public class TokenModel
{
    public string Token { get; set; } = "";

    public DateTime TokenExpire { get; set; }
}
public class BoolModel
{
    private bool apiResponse;
    public bool ApiResponse
    {
        get { return apiResponse; }
        set { apiResponse = value; }
    }

}
