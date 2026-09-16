using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        // Resetear el progreso al volver al menú
        GameManager.currentNight = 1;
        GameManager.isVictory = false;
        AudioManager.Instance.PlayMenuMusic();
    }

    public void StartGame()
    {
        AudioManager.Instance.PlayStartButton();
        if (SceneTransition.Instance != null)
            SceneTransition.Instance.LoadScene("Night1");
        else
            SceneManager.LoadScene("Night1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}