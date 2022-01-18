using UnityEngine;

public class ResourceVrmLoader : MonoBehaviour
{
    void Start()
    {
        var spawner = new VrmAvatarSpawner();
        var text_asset = Resources.Load("AliciaSolid") as TextAsset;
        var animator = GetComponent<Animator>();
        spawner.Spawn(text_asset.bytes, transform, animator);
    }
}
