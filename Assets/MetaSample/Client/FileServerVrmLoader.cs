using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class FileServerVrmLoader : MonoBehaviour
{
    [SerializeField]
    private string host;
    public void Spawn(string avatar_id)
    {
        StartCoroutine("Load", avatar_id);
    }

    private IEnumerator Load(string avatar_id)
    {
        var downloadRequest = new UnityWebRequest($"{host}/model/${avatar_id}");
        var downloadHandler = new DownloadHandlerBuffer();
        downloadRequest.downloadHandler = downloadHandler;

        yield return downloadRequest.SendWebRequest();

        var spawner = new VrmAvatarSpawner();
        var downloadBytes = downloadHandler.data;
        var animator = GetComponent<Animator>();
        spawner.Spawn(downloadBytes, transform, animator);
    }
}
