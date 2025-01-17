using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class HealthNetworked : NetworkBehaviour
{
    [SerializeField] NetworkVariable<short> health = new NetworkVariable<short>(1);
    private short maxHealth = 0;
    void Start(){
        if(health.Value == 0){
            health.Value = 1;
        }else{
            maxHealth = health.Value;
        }
    }

    public void DealDamage(short damage){
        health.Value -= damage;
        if(health.Value <= 0){
            UpdateDeathRpc();
        }
    }
    IEnumerator Respawn(){ //I know all code in this project is bad (Intentional)
        ushort time = 0;
        while(time < 100){
            yield return new WaitForFixedUpdate();
            time++;
        }
        if(IsServer) health.Value = maxHealth;
        
        SwitchState(true);
    } 
    [Rpc(target:SendTo.Everyone)]
    private void UpdateDeathRpc(){
        StartCoroutine(Respawn());
        SwitchState(false);
        if(Score.Instance){
            if(IsHost){
                Score.Instance.AddScoreHost(1);                
            }
            else if(IsClient){
                Score.Instance.AddScoreClient(1);   
            }
        }
    }
    private void SwitchState(bool state){
        var mesh = this.gameObject.GetComponent<MeshRenderer>();
        if(mesh) mesh.enabled = state;
        var hitbox = this.gameObject.GetComponent<CapsuleCollider>();
        if(hitbox) hitbox.enabled = state;
        var body = this.gameObject.GetComponent<Rigidbody>();
        if(body) body.useGravity = state;
        var shoot = this.gameObject.GetComponent<ShootingNetworked>();
        if(shoot && IsOwner) shoot.enabled = state;
    }
}
