using UnityEngine;

public class TunnelClientConfig : MonoBehaviour
{
    //Relayがつなぎに行くサーバーのURL　GemeServerのUrlと一致する
    public string tunnelGameServerUrl;
    public string gameServerRendezvous;

    //Relayがつなぎに行くサーバーのURL FileServerのUrlと一致する
    public string tunnelFileServerUrl;
    public string fileServerRendezvous;
}