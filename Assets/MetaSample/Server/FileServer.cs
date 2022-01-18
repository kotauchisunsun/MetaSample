using UnityEngine;

public class FileServer
{
    private HttpServer httpServer;

    public FileServer(int port, string basePath, string savePath)
    {
        Debug.Log(savePath);
        var resourceHandlerFactory = new ResourceHandlerFactory();
        var filePersistentHandlerFactory = new FilePersistentHandlerFactory(savePath);
        httpServer = new HttpServer(port, basePath, new IHttpHandler[]{
            resourceHandlerFactory.GetResourceHandler(),
            filePersistentHandlerFactory.getUploadHandler(),
            filePersistentHandlerFactory.getDownloadHandler()
        });
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
