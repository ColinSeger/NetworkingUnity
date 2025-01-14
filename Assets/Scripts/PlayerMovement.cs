using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float playerSpeed;
    InputAction inputMoveDirection;
    InputAction lookInput;
    Rigidbody myRigidbody;
    
    void Start()
    {
        if(!IsOwner){
            enabled = false;
        }
        inputMoveDirection = InputSystem.actions.FindAction("Move");
        lookInput = InputSystem.actions.FindAction("Look");
        myRigidbody = this.gameObject.GetComponent<Rigidbody>();
    }   

    void FixedUpdate()
    {
        HorizontalMovement();
        playerTransform.Rotate(0, lookInput.ReadValue<Vector2>().x, 0);
    }
    private void HorizontalMovement(){
        Vector2 moveDirection = inputMoveDirection.ReadValue<Vector2>();
        moveDirection *= playerSpeed;
        var tst = playerTransform.forward * moveDirection.y;
        var bro = playerTransform.right * moveDirection.x;
        myRigidbody.AddForce(tst + bro);        
    }
}
