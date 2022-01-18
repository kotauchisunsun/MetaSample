using UnityEngine;
using System.Diagnostics;

public class Initializer : MonoBehaviour
{
    public ConfigGenerator configGenerator;
    public Mirror.NetworkManager networkManager;
    public Mirror.TelepathyTransport telepathyTransport;

    public UploadAvatar uploadAvatar;

    public FileServerService fileServerService;

    public FileServerConfig fileServerConfig;

    public void Start()
    {
    }

    public string generateRandomRendezvous()
    {
        return configGenerator.generateRandomRendezvous();
    }

    public void SetUpByRendezvous(string rendezvous)
    {
        configGenerator.SetUp(rendezvous);
    }

    private void StartRelay(string arg)
    {
        Process p = new Process();
        p.StartInfo = new ProcessStartInfo(Application.dataPath + "/StreamingAssets/distributed-tcp-relay.exe");
        p.StartInfo.CreateNoWindow = true;
        p.StartInfo.Arguments = arg;
        p.Start();
    }

    private void InitializeServerModeRelayForGameServer()
    {
        StartRelay("-s -ca " + configGenerator.tunnelClientConfig.tunnelGameServerUrl + " -r " + configGenerator.tunnelClientConfig.gameServerRendezvous);
    }

    private void InitializeServerModeRelayForFileServer()
    {
        StartRelay("-s -ca " + configGenerator.tunnelClientConfig.tunnelFileServerUrl + " -r " + configGenerator.tunnelClientConfig.fileServerRendezvous);
    }

    private void InitializeClientModeRelayForGameServer()
    {
        StartRelay("-sa " + configGenerator.tunnelServerConfig.tunnelGameServerUrl + " -r " + configGenerator.tunnelServerConfig.gameServerRendezvous);
    }

    private void InitializeClientModeRelayForFileServer()
    {
        StartRelay("-sa " + configGenerator.tunnelServerConfig.tunnelFileServerUrl + " -r " + configGenerator.tunnelServerConfig.fileServerRendezvous);
    }

    public void InitializeServer()
    {
        var serverConfig = configGenerator.serverConfig;
        networkManager.networkAddress = serverConfig.gameServerHost;
        telepathyTransport.port = (ushort)serverConfig.gameServerPort;

        fileServerService.port = serverConfig.fileServerPort;
        fileServerConfig.host = serverConfig.buildFileServerUrl();

        InitializeServerModeRelayForGameServer();
        InitializeServerModeRelayForFileServer();

        networkManager.StartHost();
        fileServerService.gameObject.SetActive(true);

        SyncVrmLoader.config = fileServerConfig;
    }

    public void InitializeClient()
    {
        var clientConfig = configGenerator.clientConfig;
        networkManager.networkAddress = clientConfig.gameServerHost;
        telepathyTransport.port = (ushort)clientConfig.gameServerPort;

        fileServerConfig.host = clientConfig.buildFileServerUrl();

        InitializeClientModeRelayForGameServer();
        InitializeClientModeRelayForFileServer();

        networkManager.StartClient();
        fileServerService.gameObject.SetActive(false);

        SyncVrmLoader.config = fileServerConfig;
    }
}