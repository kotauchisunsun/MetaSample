using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ResourceServerVrmLoader : MonoBehaviour
{
    [SerializeField]
    private string host = "http://localhost:8080";
    public IEnumerator Start()
    {
        yield return StartCoroutine("Load", "AliciaSolid");
    }

    private IEnumerator Load(string resource_name)
    {
        Debug.Log("Load From ResourceServer");
        var downloadRequest = new UnityWebRequest($"{host}/resource/{resource_name}");
        var downloadHandler = new DownloadHandlerBuffer();
        downloadRequest.downloadHandler = downloadHandler;

        yield return downloadRequest.SendWebRequest();

        var spawner = new VrmAvatarSpawner();
        var downloadBytes = downloadHandler.data;
        var animator = GetComponent<Animator>();
        spawner.Spawn(downloadBytes, transform, animator);
    }
}
