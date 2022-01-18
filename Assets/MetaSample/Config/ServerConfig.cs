using UnityEngine;

public class ServerConfig : MonoBehaviour
{
    public string fileServerHost;
    public int fileServerPort;
    public string gameServerHost;
    public int gameServerPort;

    public string buildGameServerUrl()
    {
        return gameServerHost + ":" + gameServerPort;
    }

    public string buildFileServerUrl()
    {
        return fileServerHost + ":" + fileServerPort;
    }
}
