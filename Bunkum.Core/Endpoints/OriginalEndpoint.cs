using Bunkum.Listener.Protocol;

namespace Bunkum.Core.Endpoints;

public class OriginalEndpoint
{
    public string ParamlessRoute { get; }
    public Method Method { get; }
    public ProtocolInformation Protocol { get; }
    public Dictionary<int, RouteParam> ParameterIndices { get; }
    
    public OriginalEndpoint(EndpointAttribute attribute)
    {
        this.ParamlessRoute = attribute.FullRoute;
        this.Method = attribute.Method;
        this.Protocol = attribute.Protocol;
        this.ParameterIndices = attribute.ParameterIndices;
    }
    
    public OriginalEndpoint(string paramlessRoute, Method method, ProtocolInformation protocol, Dictionary<int, RouteParam> parameterIndices)
    {
        this.ParamlessRoute = paramlessRoute;
        this.Method = method;
        this.Protocol = protocol;
        this.ParameterIndices = parameterIndices;
    }
}