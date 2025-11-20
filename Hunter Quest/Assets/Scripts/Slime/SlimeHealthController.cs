using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SlimeHealthController : MonoBehaviour
{
    [SerializeField]private float maxHealth = 50;
    public float currentHealth;
    private Animator animator;
    private Rigidbody2D rb;
    private float knockbackForce = 5f, delay = 0.15f;
    public UnityEvent OnBegin, OnDone;
    public bool isKnockbacked = false;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
        }
    }
    public void Death()
    {
        Destroy(gameObject);
    }
    public void TakeDamage(float damage)
    {
        if (currentHealth >= 0 && !isKnockbacked)
        {
            currentHealth -= damage;
            PlayFeedBack(player);
        }
    }
    private IEnumerator Reset() {
        yield return new WaitForSeconds(delay);
        rb.linearVelocity = Vector3.zero;
        isKnockbacked = false;
        OnDone?.Invoke();
    }
    public void PlayFeedBack(GameObject player)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (-(player.transform.position - transform.position)).normalized;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        isKnockbacked = true;
        StartCoroutine(Reset());
    }

}
