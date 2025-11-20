using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDeath : MonoBehaviour
{
    [SerializeField] private GameObject Boss;
    public SlimeHealthController bossHealthController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossHealthController = Boss.GetComponent<SlimeHealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bossHealthController.currentHealth <= 0)
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }
}
