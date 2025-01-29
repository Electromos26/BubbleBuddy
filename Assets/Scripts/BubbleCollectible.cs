using UnityEngine;
using DG.Tweening;

public class BubbleCollectible : MonoBehaviour, ICollectable
{
    [Header("Bubble Collectible Properties")] [SerializeField]
    private int recoverAmount;

    [Header("Bubble Animation")] [SerializeField]
    private Transform bubble;

    [SerializeField] private float bobHeight = 0.5f;
    [SerializeField] private float bobDuration = 0.5f;
    [SerializeField] private Ease bobEase = Ease.InOutBounce;

    [Header("Collect Animation")] [SerializeField]
    private float duration = 0.5f;

    [SerializeField] private Ease ease = Ease.InOutBounce;

    private Tween _collectTween;
    private Tween _bobTween; 
    private Tween _spawnTween;

    private void Start()
    {
        transform.localScale = Vector3.zero;
        _spawnTween?.Kill();
        _spawnTween = transform.DOScale(new Vector3(2, 2, 2), duration * 0.5f).SetEase(ease);
        HoverAnimate();
    }

    private void HoverAnimate()
    {
        _bobTween?.Kill();
        _bobTween = bubble.DOLocalMoveY(bobHeight, bobDuration).SetEase(bobEase).SetLoops(-1, LoopType.Yoyo);
    }

    public void Collect()
    {
        _collectTween?.Kill();

        _collectTween = transform.DOScale(Vector3.zero, duration).SetEase(ease)
            .OnComplete(() => { Destroy(gameObject); });
    }

    public int RestoreAmount()
    {
        return recoverAmount;
    }

    private void OnDestroy()
    {
        _spawnTween?.Kill();
        _bobTween?.Kill();
        _collectTween?.Kill();
    }
}