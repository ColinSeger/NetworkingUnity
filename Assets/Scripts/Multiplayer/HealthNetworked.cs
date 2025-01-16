using UnityEngine;
using Unity.Netcode;

public class HealthNetworked : NetworkBehaviour
{
    [SerializeField] NetworkVariable<short> health = new NetworkVariable<short>(1);
    void Start(){
        if(health.Value == 0){
            health.Value = 1;
        }
    }
    [Rpc(target:SendTo.Server)]
    private void UpdateDeathRpc(){
        this.gameObject.SetActive(false);
    }
    public void DealDamage(short damage){
        health.Value -= damage;
        if(health.Value <= 0){
            UpdateDeathRpc();
        }
    }
}
