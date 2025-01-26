using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : Menu
{
    private void Start()
    {
        _startActive = true;
    }

    public void OnPlay()
    {
        Cursor.lockState = CursorLockMode.Locked; //Move this from here later
        Cursor.visible = false; //Move this from here later
        
        SceneManager.LoadScene(1);//TODO: Load the game scene
    }

    public void PlayClickSound()
    {
        //play click sound
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
}