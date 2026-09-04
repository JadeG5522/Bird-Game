using UnityEngine;
using UnityEngine.InputSystem;


public class Bird : MonoBehaviour
{
    private Rigidbody rb; 
    private float movementX;
    private float movementY;
    public float speed = 0; 
    private float jumpForce = 5;


    void Start()
    {
        rb = GetComponent <Rigidbody>(); 
    }

   void FixedUpdate() 
   {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY) * speed * Time.deltaTime;
        transform.position += movement; 
   }

    void OnJump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>(); 
        movementX = movementVector.x; 
        movementY = movementVector.y; 


    } 
}
