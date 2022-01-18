using UnityEngine;

public class TunnelServerConfig : MonoBehaviour
{
    //Relayが待ち受けるGameサーバーアドレス
    public string tunnelGameServerUrl;
    public string gameServerRendezvous;

    //Relayが待ち受けるFileサーバーアドレス
    public string tunnelFileServerUrl;

    public string fileServerRendezvous;
}