using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    [SerializeField] float _speedModifier;
    [SerializeField] int _health;

    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _gun;
    [SerializeField] float _bulletCooldown;
    float _timer;

    GameManager _gameManager;

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        _rigidBody.velocity = new Vector2(-_speedModifier, 0);
    }

    void Fire()
    {
        if (_timer >= _bulletCooldown)
        {
            var newBullet = Instantiate(_bullet);
            newBullet.transform.position = _gun.transform.position;
            newBullet.GetComponent<Bullet>().SetDirection(Directions.Left);
            _timer = 0;
        }
        _timer += Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LeftBorder"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            _gameManager.IncreasePoint(Points.EnemyHit);
            if (onHit() == 0)
            {
                Destroy(gameObject);
                _gameManager.IncreasePoint(Points.EnemyDestroyed);
            }
        }
    }

    public int onHit()
    {
        _health--;
        return _health;
    }
}
