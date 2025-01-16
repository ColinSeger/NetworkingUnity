using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class Emote : NetworkBehaviour
{
    [SerializeField] GameObject prefab;
    InputAction emote;
    void Start(){
        if(!IsOwner){
            enabled = false;
        }
        emote = InputSystem.actions.FindAction("Jump");
    }
    public void Update(){
        if(emote.IsPressed()){
            SpawnEmoteRpc();
        }
    }
    [Rpc(target:SendTo.Server)]
    private void SpawnEmoteRpc(){
        GameObject spawned = Instantiate(prefab, this.transform);
        var networked = spawned.GetComponent<NetworkObject>();
        networked.Spawn();
    }
}
