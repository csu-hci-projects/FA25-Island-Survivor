using StarterAssets;
using UnityEngine;

public class GameWin : MonoBehaviour
{
    [SerializeField] private Canvas userInterface;
    public void WinGame()
    {
        Time.timeScale = 0.0f;

        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().enabled = false;
        userInterface.gameObject.SetActive(false);
        //play cutscene?
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }
}
