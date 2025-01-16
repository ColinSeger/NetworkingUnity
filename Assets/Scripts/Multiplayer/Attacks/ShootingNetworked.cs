using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingNetworked : NetworkBehaviour
{
    [SerializeField] GameObject projectile;
    InputAction shootKey;
    void Start(){
        if(!IsOwner){
            enabled = false;
        }
        shootKey = InputSystem.actions.FindAction("Attack");
    }
    [Rpc(target:SendTo.Server)]
    private void UpdateServerRpc(float speed){
        GameObject spawned = Instantiate(projectile, this.transform.position + this.transform.forward, new Quaternion(1,1,1,1));
        var networked = spawned.GetComponent<NetworkObject>();
        networked.Spawn();
        StartCoroutine(UpdateProjectile(spawned, speed));
    }
    IEnumerator UpdateProjectile(GameObject projectile, float speed){
        ushort time = 0;
        projectile.transform.rotation = this.transform.rotation;
        while(time < 500){
            yield return new WaitForFixedUpdate();
            //projectile.transform.right += Vector3.forward * speed * Time.fixedDeltaTime;
            projectile.transform.position += projectile.transform.forward;
            time++;
        }
        var networked = projectile.GetComponent<NetworkObject>();
        networked.Despawn();
        Destroy(projectile);
    }
    void FixedUpdate(){
        if(shootKey.IsPressed()){
            UpdateServerRpc(2f);
        }
    }
}
