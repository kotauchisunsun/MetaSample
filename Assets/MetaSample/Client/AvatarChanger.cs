using UnityEngine;

public class AvatarChanger : MonoBehaviour
{
    public SyncVrmLoader loader;

    public void ChangeAvatar(string avatarPath)
    {
        loader.SetAvatarPath(avatarPath);
    }
}
