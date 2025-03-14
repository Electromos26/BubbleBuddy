using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : Menu
{
    
    
    [SerializeField] private UIAnimator blackScreenUIAnimator;


    private void Awake()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void Start() => blackScreenUIAnimator.FadeAnimate();

    public void ReturnToMainMenu()
    {
        blackScreenUIAnimator.OnAnimateFinished.AddListener(LoadMainMenu);
        blackScreenUIAnimator.Duration = 0.5f; 
        blackScreenUIAnimator.FadeAnimate();
    }
    
    public void RePlayGame()
    {
        blackScreenUIAnimator.OnAnimateFinished.AddListener(LoadGameScene);
        blackScreenUIAnimator.Duration = 0.5f; 
        blackScreenUIAnimator.FadeAnimate();
    }

    private void LoadMainMenu() => SceneManager.LoadScene(0);
    
    private void LoadGameScene() => SceneManager.LoadScene(2);
}