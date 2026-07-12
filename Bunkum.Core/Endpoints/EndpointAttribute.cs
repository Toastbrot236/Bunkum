using Bunkum.Listener.Protocol;
using JetBrains.Annotations;

namespace Bunkum.Core.Endpoints;

[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public abstract class EndpointAttribute : Attribute
{
    public string FullRoute { get; private set; }
    public readonly Dictionary<int, RouteParam> ParameterIndices = new();

    public Method Method { get; protected set; }
    public string ContentType { get; protected set; }
    
    /// <summary>
    /// The protocol this endpoint type is meant for.
    /// </summary>
    public abstract ProtocolInformation Protocol { get; }
    
    protected EndpointAttribute(string route) => this.GetRouteParameters(route);

    private void GetRouteParameters(string route)
    {
        // Skip scanning for parameters if the route obviously doesn't contain one
        // Maybe can optimize further in the future with source generators?
        // Only runs once per endpoint either way, so whatever
        if (route.IndexOf('{') == -1)
        {
            this.FullRoute = route;
            return;
        }
        
        // Scan for route parameters

        string fullRoute = string.Empty;
        
        string[] routeSplit = route.Split('/');
        for (int i = 0; i < routeSplit.Length; i++)
        {
            string s = routeSplit[i];
            if (i != 0) fullRoute += '/';

            bool parsingParam = false;
            string param = string.Empty;

            int j = 0; // easier flow, this foreach uses lots of continues
            foreach (char c in s)
            {
                if (parsingParam)
                {
                    if (c == '}')
                    {
                        this.ParameterIndices.Add(i, new RouteParam(j, param));
                        fullRoute += '_';
                        parsingParam = false;
                        continue;
                    }
                    
                    param += c;
                    continue;
                }
                
                if (c == '{')
                {
                    parsingParam = true;
                    continue;
                }

                j++;
                fullRoute += c;
            }

            if (parsingParam) throw new ArgumentException("Route parameter was not closed");
        }

        this.FullRoute = fullRoute;
    }
}