using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject BackButton;
public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
public void QuitGame ()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    public void UpdateEvent()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(BackButton);
    }
    public void ReverseEvent()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(PlayButton);
    }
}
