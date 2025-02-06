using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class Effects : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("Tween Settings")]
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private Ease ease = Ease.InOutSine;
    
    private Tween _buttonTween;
    private RectTransform _buttonTransform;

    private void Awake()
    {
        _buttonTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _buttonTween?.Kill();
        _buttonTween = _buttonTransform.DOScale(1.25f, duration).SetEase(ease).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _buttonTween?.Kill();
        _buttonTween = _buttonTransform.DOScale(1, duration).SetEase(ease).SetUpdate(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _buttonTween?.Kill();
        _buttonTween = _buttonTransform.DOScale(0.75f, duration * 0.5f).SetEase(ease).SetUpdate(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _buttonTween?.Kill();
        _buttonTween = _buttonTransform.DOScale(1, duration).SetEase(ease).SetUpdate(true);
    }

    private void OnDestroy()
    {
        _buttonTween?.Kill();
    }
}
