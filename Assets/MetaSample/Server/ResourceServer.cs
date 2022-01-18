using UnityEngine;

public class ResourceServer
{
    private HttpServer httpServer;

    public ResourceServer(int port, string basePath)
    {
        var factory = new ResourceHandlerFactory();
        httpServer = new HttpServer(port, basePath, new IHttpHandler[] { factory.GetResourceHandler() });
    }

    public void Start()
    {
        httpServer.Start();
    }

    public void Stop()
    {
        httpServer.Stop();
    }

    public void Close()
    {
        httpServer.Close();
    }
}
