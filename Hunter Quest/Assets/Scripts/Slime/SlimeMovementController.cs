using System.Collections;
using UnityEngine;

public class SlimeMovementController : MonoBehaviour
{
    [SerializeField] private Transform waypointParent;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float waitTimeAtWaypoint = 0.5f;
    public bool loopWaypoints = true;
    [SerializeField] Transform[] waypoints;
    [SerializeField] private int currentWaypointIndex = 0;
    [SerializeField] private bool isWaiting = false;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField]private GameObject playertoFollow;
    private SlimeHealthController slimeHealthController;
    private float followrange = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        waypoints = new Transform[waypointParent.childCount];
        for (int i = 0; i < waypointParent.childCount; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
        }
        slimeHealthController = GetComponent<SlimeHealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, playertoFollow.transform.position);
        if (slimeHealthController.isKnockbacked)
        {
            return;
        }
        else if(distance <= followrange)
        {
            Vector2 direction = (playertoFollow.transform.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            return;
        }
        else if (isWaiting || animator.GetBool("isDead"))
        {
            return;
        }
        MoveToWaypoint();
    }
    void MoveToWaypoint()
    {
        animator.SetBool("isWalking", true);
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        if (targetWaypoint.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            if (!isWaiting)
            {
                StartCoroutine(WaitAtWaypoint());
            }
        }
    }
    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        animator.SetBool("isWalking", false);
        yield return new WaitForSeconds(waitTimeAtWaypoint);
        currentWaypointIndex = loopWaypoints ? (currentWaypointIndex + 1) % waypoints.Length : Mathf.Min(currentWaypointIndex + 1, waypoints.Length - 1);
        isWaiting = false;
    }

}
