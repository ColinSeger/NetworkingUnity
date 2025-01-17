using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class DealDamage : NetworkBehaviour
{
    [SerializeField] NetworkVariable<short> damage = new NetworkVariable<short>(1);
    HealthNetworked health;
    void Start()
    {
    }
    void OnCollisionEnter(Collision collision){
        health = collision.gameObject.GetComponent<HealthNetworked>();

        if(health){
            UpdateServerRpc();
            //Debug.Log("!");
        }
        UpdateCollisionRpc();
    }    
    [Rpc(target:SendTo.Server)]
    private void UpdateServerRpc(){
        if(health){
            health.DealDamage(damage.Value);
            health = null;            
        }
    }
    [Rpc(target:SendTo.Everyone)]
    private void UpdateCollisionRpc(){
        this.gameObject.SetActive(false);
    }
}
