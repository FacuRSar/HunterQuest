using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public GameObject menuCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(menuCanvas.activeSelf)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }
    public void OpenMenu()
    {
        menuCanvas.SetActive(true);
        Time.timeScale = 0;
    }
    public void CloseMenu()
    {
        menuCanvas.SetActive(false);
        Time.timeScale = 1;
    }
}
