using Unity.Netcode;
using UnityEngine;

public class EnableOnSpawn : NetworkBehaviour
{
    [SerializeField] Camera myCamera;
    [SerializeField] AudioListener myListener;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if(myCamera == null){
            myCamera = this.gameObject.GetComponent<Camera>();
        }
        if(myListener == null){
            myListener = this.gameObject.GetComponent<AudioListener>();
        }
        if(!myCamera.enabled){
            myCamera.enabled = true;
            myListener.enabled = true;
        }
    }
}
