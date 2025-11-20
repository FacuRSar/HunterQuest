using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackDown;
    public GameObject attackRight;
    public GameObject attackUp;
    public GameObject attackLeft;
    bool isAttacking = false;
    float attackDuration = 0.5f;
    float attackTimer = 0f;
    private PlayerMovement playerMovement;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        MeleeTimer();
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            isAttacking = true;
            AttackDirection();
            attackTimer = 0f;
        }
    }
    void AttackDirection()
    {
        if(Input.GetAxisRaw("Horizontal") > 0 || playerMovement.lastHorizontalInput > 0)
        {
            attackRight.SetActive(true);
        }
        else if(Input.GetAxisRaw("Horizontal") < 0 || playerMovement.lastHorizontalInput <0)
        {
            attackLeft.SetActive(true);
        }
        else if(Input.GetAxisRaw("Vertical") > 0 || playerMovement.lastVerticalInput > 0)
        {
            attackUp.SetActive(true);
        }
        else if(Input.GetAxisRaw("Vertical") < 0 || playerMovement.lastVerticalInput < 0)
        {
            attackDown.SetActive(true);
        }
    }
    void MeleeTimer()
    {
        if(isAttacking)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer >= attackDuration)
            {
                if(attackRight.activeSelf)
                {
                    attackRight.SetActive(false);
                }
                if(attackLeft.activeSelf)
                {
                    attackLeft.SetActive(false);
                }
                if(attackUp.activeSelf)
                {
                    attackUp.SetActive(false);
                }
                if(attackDown.activeSelf)
                {
                    attackDown.SetActive(false);
                }
                attackTimer = 0f;
                isAttacking = false;
            }
        }
    }
}
