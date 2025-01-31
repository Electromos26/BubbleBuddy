using System;
using DG.Tweening;
using UnityEngine;
using Utilities;

public class BubbleBullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private float damage = 10f;

    [Header("Shoot Effect")]
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeStrength = 0.5f;
    [SerializeField] private float shakeDelay = 0.2f;

    private Vector3 _direction;
    private Rigidbody2D _rb;
    private CountdownTimer _timer;
    private Tween _bulletTween;
    private Tween _destroyTween;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _timer = new CountdownTimer(lifeTime);
        _timer.Start();
    }

    public void Init(Vector2 dir)
    {
         dir = dir.normalized;
        _rb.AddForce(dir * speed, ForceMode2D.Impulse);
        _bulletTween = transform.DOShakeScale(shakeDuration, shakeStrength).SetDelay(shakeDelay);
    }

    private void OnEnable() => _timer.OnTimerStop += DestroyBullet;
    private void OnDisable() => _timer.OnTimerStop -= DestroyBullet;

    private void Update() => _timer.Tick(Time.deltaTime);

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageable = other.GetComponent<IDamageable>();
        damageable?.GetDamaged(damage);
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        //BulletAnimation
        //_destroyTween
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _bulletTween?.Kill();
        
    }
}