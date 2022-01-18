using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Networking;

public class FileServerTest
{
    const string host = "http://localhost:8080";
    const string sceneName = "FileServerSampleScene";

    private IEnumerator UploadSample(string body, DownloadHandlerBuffer downloadHandler)
    {
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(body);
        var request = new UnityWebRequest($"{host}/model", "POST");
        request.uploadHandler = new UploadHandlerRaw(postData);
        request.downloadHandler = downloadHandler;

        yield return request.SendWebRequest();
        yield return TestUtil.WaitDoneOrFrame(() => downloadHandler.isDone);
    }

    [UnityTest]
    public IEnumerator TestFileUpload()
    {
        TestUtil.LoadScene(sceneName);
        yield return new WaitForSeconds(0.5f);

        var downloadHandlerBuffer = new DownloadHandlerBuffer();

        yield return UploadSample("uploadSample", downloadHandlerBuffer);

        var resp = JsonUtility.FromJson<UploadResponse>(downloadHandlerBuffer.text);
        Assert.AreNotEqual(resp.ID, "");
    }

    [UnityTest]
    public IEnumerator TestFileDownload()
    {
        TestUtil.LoadScene(sceneName);
        yield return new WaitForSeconds(0.5f);

        var downloadHandlerBuffer = new DownloadHandlerBuffer();

        yield return UploadSample("downloadSample", downloadHandlerBuffer);

        var resp = JsonUtility.FromJson<UploadResponse>(downloadHandlerBuffer.text);

        var downloadRequest = new UnityWebRequest($"{host}/model/" + resp.ID);
        var downloadHandler2 = new DownloadHandlerBuffer();
        downloadRequest.downloadHandler = downloadHandler2;

        yield return downloadRequest.SendWebRequest();
        yield return TestUtil.WaitDoneOrFrame(() => downloadHandler2.isDone);

        Assert.AreEqual("downloadSample", downloadHandler2.text);
    }

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