using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class DealDamage : NetworkBehaviour
{
    [SerializeField] short damage;
    HealthNetworked health;
    void Start()
    {
        if(damage == 0){
            damage = 1;
        }
    }
    [Rpc(target:SendTo.Server)]
    private void UpdateServerRpc(short damage){
        if(health){
            health.DealDamage(damage);
            health = null;            
        }
    }
    void OnCollisionEnter(Collision collision){
        health = collision.gameObject.GetComponent<HealthNetworked>();

        if(health){
            UpdateServerRpc(damage);
            //Debug.Log("!");
            
        }
    }
}
