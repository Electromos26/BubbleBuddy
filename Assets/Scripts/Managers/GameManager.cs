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
            blackScreenAnimator.OnAnimateFinished.AddListener(SwitchScene);
        }
        
        private void OnDisable()
        {
            Event.OnEndGame -= End;
            blackScreenAnimator.OnAnimateFinished.RemoveListener(SwitchScene);
        }
        
        private void End()
        {
            blackScreenAnimator.FadeAnimate();
        }
        
        private void SwitchScene()
        {
            SceneManager.LoadScene("03_LeaderBoard");
        }
        
        
    }
}
