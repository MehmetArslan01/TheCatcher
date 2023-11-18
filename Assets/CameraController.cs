
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class CameraController : NetworkBehaviour
{
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
          gameObject.SetActive(true);
        }
        
    }
}