using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menu
{
    public class StoryMenu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("02_Game");
        }
    }
}
