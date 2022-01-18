using UnityEngine;
using Mirror;

public class LoadServerConfig : MonoBehaviour
{
    public ServerConfig serverConfig;
    public NetworkManager networkManager;
    public kcp2k.KcpTransport kcpTransport;

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
        kcpTransport.Port = (ushort)serverConfig.gameServerPort;
    }
}
