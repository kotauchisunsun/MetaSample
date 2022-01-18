using System.Net;
using System;
using UnityEngine;
using System.Threading.Tasks;

public interface IHttpHandler
{
    bool CanHandle(string method, string path);
    void Handle(HttpListenerContext context);
}

public class DelegateHttpHandler : IHttpHandler
{
    public Func<string, string, bool> canHandle;
    public Action<HttpListenerContext> handle;

    public DelegateHttpHandler(Func<string, string, bool> canHandle, Action<HttpListenerContext> handle)
    {
        this.canHandle = canHandle;
        this.handle = handle;
    }

    public bool CanHandle(string method, string path)
    {
        return canHandle(method, path);
    }

    public void Handle(HttpListenerContext context)
    {
        handle(context);
    }
}

public class HttpServer
{
    private HttpListener httpListener = new HttpListener();
    private IHttpHandler[] handlers;
    public HttpServer(int port, string basePath, IHttpHandler[] handlers)
    {
        this.handlers = handlers;
        httpListener.Prefixes.Add($"http://*:{port}{basePath}");
    }

    public async void Start()
    {
        httpListener.Start();

        while (httpListener.IsListening)
        {
            try
            {
                var context = await httpListener.GetContextAsync();
                Debug.Log($"Request: {context.Request.HttpMethod} {context.Request.RawUrl}");
                foreach (var handler in handlers)
                {
                    if (handler.CanHandle(context.Request.HttpMethod, context.Request.RawUrl))
                    {
                        Task.Run(() =>
                        {
                            handler.Handle(context);
                        });
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }

    public void Stop()
    {
        httpListener.Stop();
    }

    public void Close()
    {
        httpListener.Close();
    }
}