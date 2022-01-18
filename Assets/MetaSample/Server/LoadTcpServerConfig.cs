using UnityEngine;
using Mirror;

public class LoadTcpServerConfig : MonoBehaviour
{
    // Start is called before the first frame update
    public ServerConfig serverConfig;
    public NetworkManager networkManager;

    public bool onAwakeAffect = true;

    void Awake()
    {
        if (onAwakeAffect)
        {
            Affect();
        }
    }

    public void Affect()
    {
        networkManager.networkAddress = serverConfig.gameServerHost;
    }
}
