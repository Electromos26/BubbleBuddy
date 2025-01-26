using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButlonEffects : Button
{

    private RectTransform rectTransform;

    protected override void Awake()
    {
        base.Awake();
        rectTransform = GetComponent<RectTransform>();

        // Adding Event Trigger listener for OnPointerEnter and OnPointerExit
        var trigger = gameObject.AddComponent<UnityEngine.EventSystems.EventTrigger>();
        
        UnityEngine.EventSystems.EventTrigger.Entry entry = new UnityEngine.EventSystems.EventTrigger.Entry
        {
            eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter
        };
        entry.callback.AddListener((data) => OnHoverStart());
        trigger.triggers.Add(entry);

        entry = new UnityEngine.EventSystems.EventTrigger.Entry
        {
            eventID = UnityEngine.EventSystems.EventTriggerType.PointerExit
        };
        entry.callback.AddListener((data) => OnHoverStop());
        trigger.triggers.Add(entry);
    }

    private void OnHoverStart()
    {
        transform.DOScale(new Vector3(2,2,2), 0.5f);
        Debug.Log("OnHoverStart");
    }

    private void OnHoverStop()
    {
        transform.DOScale(Vector3.one, 0.5f);
    }
    
}
