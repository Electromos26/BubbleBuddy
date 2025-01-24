using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : Menu
{
        
    [SerializeField] private float duration = 3f;
    
    private bool IsPaused { get; set; }
    private float _initialTimeScale;
    
    private Vector3 _startPos;
    private Vector3 _endPos;
    
    private RectTransform _rectTransform;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        
        IsPaused = false;
        currentMenu.gameObject.SetActive(IsPaused);
        
        _startPos = currentMenu.transform.localPosition;
        _endPos = Vector3.zero;
        
        /*Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;*/
    }

    public async void Resume()
    {
        try
        {
            await AnimateOut();
            currentMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        catch (Exception e)
        {
            Debug.Log("Pause Menu error: " + e);
        }
    }

    private void Pause()
    {
        currentMenu.SetActive(true);
        Time.timeScale = 0f;
        AnimateIn();
    }

    private void AnimateIn()
    {
        _rectTransform.DOAnchorPos(_endPos, duration).SetUpdate(true);
    }

    private async Task AnimateOut()
    {
        await  _rectTransform.DOAnchorPos(_startPos, duration).SetUpdate(true).AsyncWaitForCompletion();
    }


    public void OnLoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void OnGiveFeedback()
    {
        Application.OpenURL("");//TODO: Add feedback link or some link 
    }
}
