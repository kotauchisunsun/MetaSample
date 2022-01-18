using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInitializer : MonoBehaviour
{
    public InputField roomNameInputField;

    public Initializer initializer;
    // Start is called before the first frame update
    void Start()
    {
        var roomName = initializer.generateRandomRendezvous();
        roomNameInputField.text = roomName;
    }

    public void StartServer()
    {
        initializer.SetUpByRendezvous(roomNameInputField.text);
        initializer.InitializeServer();
        this.gameObject.SetActive(false);
    }

    public void StartClient()
    {
        initializer.SetUpByRendezvous(roomNameInputField.text);
        initializer.InitializeClient();
        this.gameObject.SetActive(false);
    }
}
