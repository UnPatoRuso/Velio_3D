using UnityEngine;
using UnityEngine.InputSystem;


public class Movement_Velio : MonoBehaviour
{

    public float speed = 5f; // Speed of the player
    public float jumpForce = 5f; // Force of the jump
    
    public Rigidbody rb; // Reference to the Rigidbody component
    public SpriteRenderer sr;
    public Animator anim; // Reference to the Animator component
    private bool isGrounded = true; // Check if the player is on the ground


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0;
        float y = 0;

        if (Keyboard.current.aKey.isPressed)
            x = -1;

        if (Keyboard.current.dKey.isPressed)
            x = 1;

        if (Keyboard.current.sKey.isPressed)
            y = -1;

        if (Keyboard.current.wKey.isPressed)
            y = 1;

        rb.linearVelocity = new Vector3(
            x * speed,
            rb.linearVelocity.y,
            y * speed
        );

        if (x != 0 || y != 0)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (x > 0)
            sr.flipX = true;
        else if (x < 0)
            sr.flipX = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}