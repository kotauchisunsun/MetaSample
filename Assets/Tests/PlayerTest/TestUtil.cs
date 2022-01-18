using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEditor;
using System;

public class TestUtil
{
    public static void LoadScene(string sceneName)
    {
        var guids = AssetDatabase.FindAssets("t:scene " + sceneName);
        if (guids.Length != 1)
        {
            throw new Exception("Scene name is not unique.");
        }
        var path = AssetDatabase.GUIDToAssetPath(guids[0]);
        EditorSceneManager.LoadSceneInPlayMode(path, new LoadSceneParameters(LoadSceneMode.Single));
    }

    public static IEnumerator WaitDoneOrFrame(Func<bool> isDone, int frameNum = 300)
    {
        int frameCount = 0;
        while (true)
        {
            if (frameCount > frameNum)
            {
                break;
            }
            if (isDone())
            {
                yield break;
            }
            frameCount++;
            yield return null;
        }
    }
}
