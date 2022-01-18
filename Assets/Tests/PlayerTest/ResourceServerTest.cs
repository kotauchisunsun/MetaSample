using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Networking;

public class ResourceServerTest
{
    const string host = "http://localhost:8080";
    const string sceneName = "ResourceServerSampleScene";

    [UnityTest]
    public IEnumerator TestResourceDownload()
    {
        TestUtil.LoadScene(sceneName);
        yield return new WaitForSeconds(0.5f);

        var downloadRequest = new UnityWebRequest($"{host}/resource/AliciaSolid");
        var downloadHandler = new DownloadHandlerBuffer();
        downloadRequest.downloadHandler = downloadHandler;

        yield return downloadRequest.SendWebRequest();
        yield return TestUtil.WaitDoneOrFrame(() => downloadHandler.isDone);

        Assert.AreEqual(7878712, downloadHandler.data.Length);
    }
}