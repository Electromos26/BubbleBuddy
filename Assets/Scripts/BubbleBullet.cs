using UnityEngine;

public class BubbleBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 2f;


    private Rigidbody2D _rb;
    
    //private float _damage = 1f;
    private float _timer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _timer = lifeTime;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        _rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("BubbleBullet collided with " + other.name);
        if (!other.CompareTag("Player"))
            Destroy(gameObject);
    }
}