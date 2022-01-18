using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConfigGenerator : MonoBehaviour
{
    public ServerConfig serverConfig;
    public TunnelServerConfig tunnelServerConfig;

    public ClientConfig clientConfig;
    public TunnelClientConfig tunnelClientConfig;

    public string rendezvous;

    private System.Random r = new System.Random();

    void Start()
    {
    }

    private int generateRandomPort()
    {
        return r.Next(10000, 20000);
    }

    private string generateRandomServerUrl()
    {
        var port = generateRandomPort();
        return "localhost:" + port;
    }

    private string generateRandomColor()
    {
        var colors = new string[] { "Red", "Green", "Blue", "Yellow", "Orange", "Purple", "Pink", "Cyan", "White", "Black" };
        return colors[r.Next(0, colors.Length)];
    }

    private string generateRandomAnimal()
    {
        var animals = new string[] { "Cat", "Dog", "Horse", "Bird", "Fish", "Rabbit", "Cow", "Pig", "Sheep", "Chicken" };
        return animals[r.Next(0, animals.Length)];
    }

    private string generateRandomBuilding()
    {
        var buildings = new string[] { "House", "Castle", "Mansion", "Apartment", "Hotel", "Office", "Restaurant", "Bar", "Cafe", "Bank" };
        return buildings[r.Next(0, buildings.Length)];
    }

    public string generateRandomRendezvous()
    {
        return generateRandomColor() + generateRandomAnimal() + "In" + generateRandomBuilding();
    }

    public void SetUp(string rendezvous)
    {
        serverConfig.fileServerHost = "localhost";
        serverConfig.fileServerPort = generateRandomPort();
        serverConfig.gameServerHost = "localhost";
        serverConfig.gameServerPort = generateRandomPort();

        tunnelClientConfig.tunnelGameServerUrl = serverConfig.buildGameServerUrl();
        tunnelClientConfig.gameServerRendezvous = rendezvous + "-game";
        tunnelClientConfig.tunnelFileServerUrl = serverConfig.buildFileServerUrl();
        tunnelClientConfig.fileServerRendezvous = rendezvous + "-file";

        clientConfig.fileServerHost = "localhost";
        clientConfig.fileServerPort = generateRandomPort();
        clientConfig.gameServerHost = "localhost";
        clientConfig.gameServerPort = generateRandomPort();

        tunnelServerConfig.tunnelGameServerUrl = clientConfig.buildGameServerUrl();
        tunnelServerConfig.gameServerRendezvous = rendezvous + "-game";
        tunnelServerConfig.tunnelFileServerUrl = clientConfig.buildFileServerUrl();
        tunnelServerConfig.fileServerRendezvous = rendezvous + "-file";
    }
}
