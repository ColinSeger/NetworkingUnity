using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float playerSpeed;
    InputAction inputMoveDirection;
    InputAction inputJump;
    InputAction lookInput;
    Rigidbody myRigidbody;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputMoveDirection = InputSystem.actions.FindAction("Move");
        lookInput = InputSystem.actions.FindAction("Look");
        myRigidbody = this.gameObject.GetComponent<Rigidbody>();
    }   

    // Update is called once per frame
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
