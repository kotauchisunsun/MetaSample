using System.Net;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UploadResponse
{
    public string ID;
}

public class FilePersistentHandlerFactory
{
    private string savePath;
    private Dictionary<string, byte[]> cache = new Dictionary<string, byte[]>();

    public FilePersistentHandlerFactory(string savePath)
    {
        this.savePath = savePath;
    }

    public IHttpHandler getUploadHandler()
    {
        return new DelegateHttpHandler(
            (method, path) =>
            {
                return method == "POST" && path == "/model";
            },
            (context) =>
            {
                Guid g = Guid.NewGuid();
                SaveFile(g, context.Request.InputStream);
                var upload = new UploadResponse
                {
                    ID = g.ToString()
                };
                Debug.Log($"ID: {g.ToString()}");
                var data = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(upload));
                context.Response.StatusCode = 200;
                context.Response.ContentType = "text/json";
                context.Response.Close(data, false);
            }
        );
    }

    public IHttpHandler getDownloadHandler()
    {
        return new DelegateHttpHandler(
            (method, path) =>
            { return method == "GET" && path.StartsWith("/model/"); },
            (context) =>
            {
                var id = context.Request.RawUrl.Substring("/model/".Length);
                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/octet-stream";
                var fileStream = LoadFile(id);
                fileStream.CopyTo(context.Response.OutputStream);
                context.Response.OutputStream.Close();
            }
        );
    }

    private void SaveFile(Guid g, Stream inputStream)
    {
        var fileName = g.ToString();
        var filePath = Path.Combine(savePath, fileName);
        Debug.Log($"Save To {filePath}");

        var memoryStream = new MemoryStream();
        inputStream.CopyTo(memoryStream);
        cache[g.ToString()] = memoryStream.ToArray();

        var fileStream = new FileStream(filePath, FileMode.Create);
        memoryStream.CopyTo(fileStream);
        fileStream.Close();
    }

    private Stream LoadFile(string id)
    {
        if (cache.ContainsKey(id))
        {
            return new MemoryStream(cache[id]);
        }

        var filePath = Path.Combine(savePath, id);
        Debug.Log($"Load From {filePath}");
        var fileStream = new FileStream(filePath, FileMode.Open);
        return fileStream;
    }
}