using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy2 : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    [SerializeField] float _speedModifier;
    [SerializeField] int _health;
    GameManager _gameManager;
    float _angle = 0;
    bool _collidedBefore = false;
    Transform _target;
    Vector2 _direction;

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        _direction = (Vector2)_target.position - _rigidBody.position;
        _direction.Normalize();
        _rigidBody.velocity = _direction * _speedModifier;
        _angle += 0.5f;
        _rigidBody.MoveRotation(_angle);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_collidedBefore && (collision.CompareTag("TopBorder") || collision.CompareTag("BottomBorder") || collision.CompareTag("RightBorder") || collision.CompareTag("LeftBorder")))
        {
            Destroy(gameObject);
            _collidedBefore = false;
        }
        else _collidedBefore = true;
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
