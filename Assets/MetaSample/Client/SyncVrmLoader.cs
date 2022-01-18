using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Mirror;

public class SyncVrmLoader : NetworkBehaviour
{
    [SerializeField]
    public static FileServerConfig config;

    [SyncVar(hook = nameof(SyncAvatarPath))]
    public string myAvatarPath;

    GameObject avatar;

    public void Start()
    {
        if (SyncVrmLoader.config == null)
        {
            SyncVrmLoader.config = FindObjectOfType<FileServerConfig>();
        }

        if (isLocalPlayer)
        {
            var avatarChanger = FindObjectOfType<AvatarChanger>();
            avatarChanger.loader = this;
        }
    }

    public void SetAvatarPath(string avatarPath)
    {
        CmdSetAvatarPath(avatarPath);
    }

    [Command]
    void CmdSetAvatarPath(string avatarPath)
    {
        Debug.Log("CmdSetAvatarPath");
        myAvatarPath = avatarPath;
    }

    void SyncAvatarPath(string oldPath, string newPath)
    {
        Debug.Log("SyncAvatarPath");
        Debug.Log($"Call LoadAvatar {oldPath} -> {newPath}");
        StartCoroutine("LoadAvatar", newPath);
    }

    private IEnumerator LoadAvatar(string avatarPath)
    {
        Debug.Log($"Load From ResourceServer {avatarPath}");
        var downloadRequest = new UnityWebRequest($"{config.host}/{avatarPath}");
        var downloadHandler = new DownloadHandlerBuffer();
        downloadRequest.downloadHandler = downloadHandler;

        var operation = downloadRequest.SendWebRequest();
        yield return operation;

        var spawner = new VrmAvatarSpawner();
        var downloadBytes = downloadHandler.data;
        var animator = GetComponent<Animator>();
        var newAvatar = spawner.Spawn(downloadBytes, transform, animator);
        Destroy(avatar);
        avatar = newAvatar;
    }
}
