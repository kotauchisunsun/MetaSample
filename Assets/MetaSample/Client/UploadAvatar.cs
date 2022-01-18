using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using SFB;
using System.IO;

public class UploadAvatar : MonoBehaviour
{
    [SerializeField]
    public FileServerConfig config;

    [SerializeField]
    AvatarChanger avatarChanger;

    public void Start()
    {
        this.config = FindObjectOfType<FileServerConfig>();
    }

    public void UploadAvatarToServer()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "vrm", false);
        if (paths.Length > 0)
        {
            StartCoroutine(UploadAvatarToServerCoroutine(paths[0]));
        }
    }

    IEnumerator UploadAvatarToServerCoroutine(string path)
    {
        Debug.Log("UploadAvatarToServerCoroutine");
        var data = File.ReadAllBytes(path);
        var request = new UnityWebRequest($"{config.host}/model", "POST");
        var downloadHandlerBuffer = new DownloadHandlerBuffer();
        request.uploadHandler = new UploadHandlerRaw(data);
        request.downloadHandler = downloadHandlerBuffer;

        var operation = request.SendWebRequest();
        operation.completed += (op) =>
        {
            var resp = JsonUtility.FromJson<UploadResponse>(downloadHandlerBuffer.text);
            avatarChanger.ChangeAvatar($"model/{resp.ID}");
        };

        yield return operation;
    }
}
