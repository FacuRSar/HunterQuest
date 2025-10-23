using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        transform.Translate(0, vertical * Time.deltaTime * speed, 0);
        transform.Translate(horizontal * Time.deltaTime * speed, 0,0);
    }
}
