using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : Menu
{
    public GameEvent gameEvent;

    [Header("Audio"), SerializeField] private Image MuteButtonImage;
    
    [Header("Sensitivity"),SerializeField]
    private Slider sensitivitySlider;

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

    public void OnGiveFeedback()
    {
        Application.OpenURL("");
    }

   
}