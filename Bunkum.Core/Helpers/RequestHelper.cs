using System.Web;
using Bunkum.Core.Endpoints;

namespace Bunkum.Core.Helpers;

public static class RequestHelper
{
    public static bool UriMatchesRoute(RequestedEndpoint requested, OriginalEndpoint original)
    {
        if (requested.Protocol.Name != original.Protocol.Name) return false;
        if (requested.Method != original.Method) return false;
        if (requested.OriginalRoute == original.ParamlessRoute) return true;
        
        // has no route params and route without parsing params didn't match
        if (original.ParameterIndices.Count <= 0) return false;
        
        // this route has parameters
        string paramlessRoute = string.Empty;
        string[] routeParts = requested.OriginalRoute.Split('/');
        for (int i = 0; i < routeParts.Length; i++)
        {
            string routePart = routeParts[i];
            if (i != 0) paramlessRoute += '/';
            
            if (original.ParameterIndices.TryGetValue(i, out RouteParam param))
            {
                paramlessRoute += routePart;
            }
            else
            {
                try
                {
                    string paramStart = routePart.Substring(0, param.Skip);
                    string paramValue = routePart.Substring(param.Skip);
                    
                    requested.RouteParameters.Add(param.Name, HttpUtility.UrlDecode(paramValue));
                    paramlessRoute += paramStart + '_';
                }
                catch
                {
                    paramlessRoute += '_';
                }
            }
        }

        requested.ParamlessRoute = paramlessRoute;
        return paramlessRoute == original.ParamlessRoute;
    }
}