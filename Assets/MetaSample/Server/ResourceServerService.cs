using System.Net;
using UnityEngine;
using System;
using System.IO;

/*
Invoke-WebRequest http://127.0.0.1:8080/resource/AliciaSolid
*/

public class ResourceServerService : MonoBehaviour
{
    private ResourceServer resourceServer;

    public int port = 8080;
    public string path = "/";

    public bool startOnAwake = true;

    void Start()
    {
        resourceServer = new ResourceServer(port, path);

        if (startOnAwake)
        {
            resourceServer.Start();
        }
    }

    void OnDestroy()
    {
        resourceServer.Stop();
        resourceServer.Close();
    }
}