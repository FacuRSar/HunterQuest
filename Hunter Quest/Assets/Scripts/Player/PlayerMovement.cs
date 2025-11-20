using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;
    private float horizontalInput;
    private float verticalInput;
    public float lastHorizontalInput;
    public float lastVerticalInput;
    public GameObject attackDown;
    public GameObject attackLeft;
    private float knockbackForce = 8f, delay = 0.15f;
    public UnityEvent OnBegin, OnDone;
    public bool isKnockbacked = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isKnockbacked) {
            return;
        }
        else if(!animator.GetBool("isAttacking"))
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            transform.Translate(0, verticalInput * Time.deltaTime * speed, 0);
            transform.Translate(horizontalInput * Time.deltaTime * speed, 0, 0);
        }
        FlipSprite();
        Animation();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            
                animator.SetBool("isAttacking", true);
        }
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isKnockbacked)
        {
            PlayFeedBack(collision.gameObject);
        }
    }
    public void OnAttackAnimationEnd()
    {
        animator.SetBool("isAttacking", false);
    }
    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.linearVelocity = Vector3.zero;
        isKnockbacked = false;
        OnDone?.Invoke();
    }
    public void PlayFeedBack(GameObject player)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - player.transform.position).normalized;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        isKnockbacked = true;
        StartCoroutine(Reset());
    }
}
