using UnityEngine;
using UnityEngine.Serialization;

public class Menu : MonoBehaviour
{
    [Header("Menu Start Up"),SerializeField] protected bool _startActive;
    
    [Header("Menu Game Objects")]
    [SerializeField] protected GameObject currentMenu;
    protected bool _isMusted;

    //Menu Management

    public void OnToggleCurrentMenu() => currentMenu?.SetActive(!currentMenu.activeSelf);

    public void OnQuitGame()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
/*#elif UNITY_WEBGL
            CloseTab();*/
#else
            Application.Quit();
#endif
    }

    
}
