using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
using System.Collections;

public class Emote : NetworkBehaviour
{
    [SerializeField] GameObject[] prefab;
    InputAction emote;
    bool isEmoting;
    void Start(){
        if(!IsOwner){
            enabled = false;
        }
        emote = InputSystem.actions.FindAction("Jump");
    }
    public void Update(){
        if(emote.IsPressed() && !isEmoting){
            isEmoting = true;
            SpawnEmoteRpc();
        }
    }
    IEnumerator UpdateEmote(GameObject emote){
        ushort time = 0;
        while(time < 100){
            yield return new WaitForFixedUpdate();
            time++;
        }
        isEmoting = false;
        var networked = emote.GetComponent<NetworkObject>();
        networked.Despawn();
        Destroy(emote);
    }
    [Rpc(target:SendTo.Server)]
    private void SpawnEmoteRpc(){
        int index = Random.Range(0,prefab.Length);
        GameObject spawned = Instantiate(prefab[index]);
        var networked = spawned.GetComponent<NetworkObject>();
        networked.Spawn();
        StartCoroutine(UpdateEmote(spawned));
    }
}
