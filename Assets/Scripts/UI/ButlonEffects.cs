using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButlonEffects : Button
{
    [Header("Tween Settings")]
    public float duration = 0.3f;
    public Ease ease = Ease.InOutSine;
    public float start = 1;
    public float target = 0;

    private Tween _buttonTween;
    private RectTransform _buttonTransform;

    protected override void Awake()
    {
        _buttonTransform = GetComponent<RectTransform>();
        base.Awake();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
      
        _buttonTween?.Kill();
        _buttonTween = _buttonTransform.DOScale(target, duration).SetEase(ease).SetUpdate(true);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        _buttonTween?.Kill();
        _buttonTween = _buttonTransform.DOScale(start, duration).SetEase(ease).SetUpdate(true);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }
}