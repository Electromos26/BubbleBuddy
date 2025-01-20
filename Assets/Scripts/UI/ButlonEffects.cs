using System;
using UnityEngine;
using UnityEngine.UI;

public class ButlonEffects : MonoBehaviour
{
   private Button button;

   private void Awake()
   {
      button = GetComponent<Button>();
   }
   
   public void OnHover()
   {
      //play hover sound
   }
}
