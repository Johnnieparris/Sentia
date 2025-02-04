using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUIScript : MonoBehaviour
{
    public void OnRetry()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
