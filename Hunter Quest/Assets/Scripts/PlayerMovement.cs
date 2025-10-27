using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed;
    private Rigidbody2D rb;
    private float jumpForce = 10f;
    private Transform groundCheck;
    private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private bool isGrounded()
    {
        bool Grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        return Grounded;
    }
    private void jump()
    {
        if (isGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");
        //transform.Translate(0, vertical * Time.deltaTime * speed, 0);
        transform.Translate(horizontal * Time.deltaTime * speed, 0,0);
        if (Input.GetButtonDown("Jump"))
            jump();
    }
}
