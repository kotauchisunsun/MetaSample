using System.Net;
using UnityEngine;
using System;
using System.IO;

public class FileServerService : MonoBehaviour
{
    private FileServer fileServer;

    public int port = 8080;
    public string path = "/";

    private const string savePath = "model";

    public bool startOnAwake = true;

    void Start()
    {
        var modelSavePath = Path.Combine(Application.persistentDataPath, savePath);
        if (!Directory.Exists(modelSavePath))
        {
            Directory.CreateDirectory(modelSavePath);
        }

        fileServer = new FileServer(port, path, modelSavePath);

        if (startOnAwake)
        {
            fileServer.Start();
        }
    }

    void OnDestroy()
    {
        fileServer.Stop();
        fileServer.Close();
    }
}