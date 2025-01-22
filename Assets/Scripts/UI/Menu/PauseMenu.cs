using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class PauseMenu : Menu
{
        
    private bool IsPaused { get; set; }
    
    private float _initialTimeScale;
    
    
    private void Awake()
    {
        IsPaused = false;
        _currentMenu.gameObject.SetActive(IsPaused);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    public void OnTogglePauseMenu()
    {
        IsPaused = !IsPaused;
        _currentMenu.gameObject.SetActive(IsPaused);
        
        Cursor.lockState = IsPaused? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = IsPaused;

        if (IsPaused)
        {
            _initialTimeScale = Time.timeScale;
            Time.timeScale = 0; 
        }
        else
        {
            Time.timeScale = _initialTimeScale;
           
        }

    }
    
    
    public void OnLoadMainMenu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }

    public void OnGiveFeedback()
    {
        Application.OpenURL("");//TODO: Add feedback link or some link 
    }
}
