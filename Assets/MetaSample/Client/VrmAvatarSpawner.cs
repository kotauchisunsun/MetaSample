using UnityEngine;
using UniGLTF;
using VRM;

public class VrmAvatarSpawner
{
    public GameObject Spawn(byte[] bytes, Transform parent, Animator rootAnimator)
    {
        var gltfData = new GlbBinaryParser(bytes, null).Parse();
        var vrm = new VRMData(gltfData);
        using (var loader = new VRMImporterContext(vrm))
        {
            var instance = loader.Load();

            instance.transform.SetParent(parent, false);
            instance.ShowMeshes();

            var vrmAnimator = instance.GetComponent<Animator>();
            rootAnimator.avatar = vrmAnimator.avatar;

            return instance.gameObject;
        }
    }
}