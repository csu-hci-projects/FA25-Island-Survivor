using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void QuitGame()
    {
        #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
        SceneManager.LoadScene("GameScene");
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().enabled = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<Player>().isPaused = true;
    }
}
