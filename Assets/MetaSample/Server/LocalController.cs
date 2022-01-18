using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LocalController : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            var unitychan = GetComponent<UnityChan.UnityChanControlScriptWithRgidBody>();
            unitychan.enabled = true;

            var cumPosGroup = this.transform.Find("CumPosGroup").gameObject;
            cumPosGroup.SetActive(true);

            var cameraObject = GameObject.FindWithTag("MainCamera");
            var component = cameraObject.GetComponent<UnityChan.ThirdPersonCamera>();
            component.enabled = true;
        }
    }
}
