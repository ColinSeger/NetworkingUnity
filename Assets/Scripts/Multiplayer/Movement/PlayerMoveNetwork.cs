using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
struct UpdateData{
    public Transform playerTransform;
    public Vector3 velocity;
    public ushort ticks;

}
[Serializable]
struct PlayerMoveData{
    public Transform playerTransform;
    public Rigidbody playerRigidBody;
    public float playerSpeed;
}
public class PlayerMoveNetwork : NetworkBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float playerSpeed;
    InputAction inputMoveDirection;
    InputAction lookInput;
    Rigidbody myRigidbody;
    
    public override void OnNetworkSpawn()
    {
        if(!IsOwner){
            enabled = false;
        }
        inputMoveDirection = InputSystem.actions.FindAction("Move");
        lookInput = InputSystem.actions.FindAction("Look");
        myRigidbody = this.gameObject.GetComponent<Rigidbody>();
        playerTransform = this.transform;
    }   
    private void HorizontalMovement(Vector2 moveDirection){
        moveDirection *= playerSpeed;
        Vector3 tst = playerTransform.forward * moveDirection.y;
        Vector3 bro = playerTransform.right * moveDirection.x;
        myRigidbody.AddForce(tst + bro);        
    }
    private void MoveCamera(float lookValue){
        playerTransform.Rotate(0, lookValue, 0);
    }
    [Rpc(target:SendTo.Server)]
    private void UpdateServerRpc(Vector2 moveDirection, float lookValue){
        MoveCamera(lookValue);
        this.HorizontalMovement(moveDirection);
    }
    private void FixedUpdate(){
        if(!IsOwner) return;
        Vector2 moveDirection = inputMoveDirection.ReadValue<Vector2>();
        float lookValue = lookInput.ReadValue<Vector2>().x;
        //updateList[ticket] = HorizontalMovement(moveDirection, playerMoveData);
        
        UpdateServerRpc(moveDirection, lookValue);
    }
}
