using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class EndGame : Singleton<EndGame>
{
   public GameEvent Event;

   [SerializeField] TextMeshProUGUI text;

   private void OnEnable()
   {
      Event.FinalScore += EndGameUI;
   }

   private void OnDisable()
   {
      Event.FinalScore -= EndGameUI;
   }

   private void Start()
   {
      if (text ==null) return;
      text.text= "YourScore:"  + Event.Score;
   }

   public void EndGameUI(int score)
   {
      SceneManager.LoadScene("03_Finish");
   }

   public void Backtomenu()
   {
      SceneManager.LoadScene(0);
   }

}
