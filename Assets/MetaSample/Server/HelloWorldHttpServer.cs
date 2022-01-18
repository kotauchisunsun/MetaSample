using System.Net;
using UnityEngine;

public class HelloWorldHttpServer : MonoBehaviour
{
    private readonly HttpListener httpListener = new HttpListener();

    public int port = 8080;
    public string path = "/";
    public bool startOnAwake = true;

    void Start()
    {
        httpListener.Prefixes.Add("http://*:" + port + path);

        if (startOnAwake)
        {
            StartServer();
        }
    }

    public async void StartServer()
    {
        httpListener.Start();

        while (true)
        {
            var context = await httpListener.GetContextAsync();

            Debug.Log("Request path: " + context.Request.RawUrl);

            var data = System.Text.Encoding.UTF8.GetBytes("Hello World!");
            context.Response.StatusCode = 200;
            context.Response.ContentType = "text/plain";
            context.Response.Close(data, false);
        }
    }

    void OnDestroy()
    {
        httpListener.Stop();
    }
}