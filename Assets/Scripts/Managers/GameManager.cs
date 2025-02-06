using Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public GameEvent Event;
        [SerializeField] private UIAnimator blackScreenAnimator;

        private void OnEnable()
        {
            Event.OnEndGame += End;
        }
        
        private void OnDisable()
        {
            Event.OnEndGame -= End;
        }
        
        private void End()
        {
            Time.timeScale = 0;
            blackScreenAnimator.FadeInAnimate(true);
        }
        
        public void SwitchScene()
        {
            SceneManager.LoadScene("03_LeaderBoard");
        }
        
        
    }
}
