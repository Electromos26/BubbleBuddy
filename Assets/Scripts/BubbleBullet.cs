using System;
using UnityEngine;
using Utilities;

public class BubbleBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private float damage = 10f;


    private Vector3 _direction;
    private Rigidbody2D _rb;
    private CountdownTimer _timer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _timer = new CountdownTimer(lifeTime);
        _timer.Start();
    }

    public void Init(Vector2 dir)
    {
        _rb.AddForce(dir * speed, ForceMode2D.Impulse);
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
        Destroy(gameObject);
    }
}