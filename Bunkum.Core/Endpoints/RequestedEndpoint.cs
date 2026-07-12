using Bunkum.Listener.Protocol;
using Bunkum.Listener.Request;

namespace Bunkum.Core.Endpoints;

public class RequestedEndpoint
{
    public string OriginalRoute { get; }
    public string ParamlessRoute { get; set; } // assume this is the same as original until overwritten by RequestHelper.UriMatchesRoute()
    public Method Method { get; }
    public ProtocolInformation Protocol { get; }
    
    public Dictionary<string, string> RouteParameters { get; } = new();

    public RequestedEndpoint(ListenerContext context)
    {
        this.OriginalRoute = context.Uri.AbsolutePath;
        this.ParamlessRoute = context.Uri.AbsolutePath;
        this.Method = context.Method;
        this.Protocol = context.Protocol;
    }

    public RequestedEndpoint(string originalRoute, Method method, ProtocolInformation protocol)
    {
        this.OriginalRoute = originalRoute;
        this.ParamlessRoute = originalRoute;
        this.Method = method;
        this.Protocol = protocol;
    }
}