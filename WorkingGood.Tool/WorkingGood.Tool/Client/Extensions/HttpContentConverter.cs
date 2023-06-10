using System.Text;
using Newtonsoft.Json;

namespace WorkingGood.Tool.Client.Extensions;

public static class HttpContentConverter
{
    public static HttpContent ToJsonContent(this object obj)
    {
        var json = JsonConvert.SerializeObject(obj);
        var httpContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
        return httpContent;
    }
}