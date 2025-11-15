using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;
    private float horizontalInput;
    private float verticalInput;
    private float lastHorizontalInput;
    private float lastVerticalInput;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        transform.Translate(0, verticalInput * Time.deltaTime * speed, 0);
        transform.Translate(horizontalInput * Time.deltaTime * speed, 0, 0);
        FlipSprite();
        Animation();
    }
    private void FlipSprite()
    {
        if (horizontalInput < 0 || lastHorizontalInput <0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalInput >= 0 && lastHorizontalInput >=0)
        {
            spriteRenderer.flipX = false;
        }
    }
    private void Animation()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            animator.SetBool("isWalking", true);
            if(horizontalInput != lastHorizontalInput)
            {
                lastHorizontalInput = horizontalInput;
                animator.SetFloat("LastInputX", lastHorizontalInput);
            }
            else if(verticalInput != lastVerticalInput)
            {
                lastVerticalInput = verticalInput;
                animator.SetFloat("LastInputY", lastVerticalInput);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        animator.SetFloat("InputX", horizontalInput);
        animator.SetFloat("InputY", verticalInput);
    }
}
