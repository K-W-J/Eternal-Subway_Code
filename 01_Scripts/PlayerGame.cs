using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGame : MonoBehaviour
{
    [SerializeField] private string _sampleSceneName = "SampleScene";

    public void PlayGame()
    {
        SceneManager.LoadScene(_sampleSceneName);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
