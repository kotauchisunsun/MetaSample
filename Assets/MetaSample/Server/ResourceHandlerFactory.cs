using UnityEngine;
using System.Collections.Generic;

public class ResourceHandlerFactory
{
    Dictionary<string, byte[]> resources = new Dictionary<string, byte[]>();

    public ResourceHandlerFactory()
    {
        LoadResource("AliciaSolid");
        LoadResource("AvatarSample_E");
    }

    private void LoadResource(string name)
    {
        var resource = Resources.Load(name) as TextAsset;
        resources[name] = resource.bytes;
    }

    public IHttpHandler GetResourceHandler()
    {
        return new DelegateHttpHandler(
            (method, path) =>
            {
                return method == "GET" && path.StartsWith("/resource/");
            },
            (context) =>
            {
                var path = context.Request.RawUrl;
                var resourceName = path.Substring("/resource/".Length);
                var resource = resources[resourceName];
                context.Response.ContentType = "application/octet-stream";
                context.Response.OutputStream.Write(resource, 0, resource.Length);
                context.Response.OutputStream.Close();
            }
        );
    }
}