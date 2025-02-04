using UnityEngine;
using UnityEngine.SceneManagement;


public class pauseMenuScript : MonoBehaviour
{

    [SerializeField] GameObject PauseMenu;

    public bool paused = false;

    public void Pause()
    {
        PauseMenu.SetActive(true);
        paused = true;
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        PauseMenu.SetActive(false);
        paused = false;
        Time.timeScale = 1;
    }


    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
