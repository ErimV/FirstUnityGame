using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    [SerializeField] float _speedModifier;
    [SerializeField] int _health;

    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _rightGun;
    [SerializeField] GameObject _leftGun;
    [SerializeField] float _bulletCooldown;
    [SerializeField] float _decisionCooldown;
    float _timer;
    [SerializeField] float _speedModifierY;
    [SerializeField] float yDuration;
    [SerializeField] float delayBeforeReturnToX;
    float yMovementStartTime;
    bool isMovingInYDirection = false;

    GameManager _gameManager;

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rigidBody = GetComponent<Rigidbody2D>();
        yMovementStartTime = Time.time;
        _rigidBody.velocity = new Vector2(-_speedModifier, 0);
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        if (!isMovingInYDirection && Time.time - yMovementStartTime >= yDuration)
        {
            if (Random.Range(0, 2) == 0)
            {
                _rigidBody.velocity = new Vector2(0, _speedModifierY);
            }
            else
            {
                _rigidBody.velocity = new Vector2(0, -_speedModifierY);
            }
            isMovingInYDirection = true;
        }
        else if (isMovingInYDirection && Time.time - yMovementStartTime >= yDuration + delayBeforeReturnToX)
        {
            _rigidBody.velocity = new Vector2(-_speedModifier, 0);
            isMovingInYDirection = false;
            yMovementStartTime = Time.time;
        }

    }

    void Fire()
    {
        if (_timer >= _bulletCooldown)
        {
            var newBullet1 = Instantiate(_bullet);
            var newBullet2 = Instantiate(_bullet);
            newBullet1.transform.position = _rightGun.transform.position;
            newBullet2.transform.position = _leftGun.transform.position;
            newBullet1.GetComponent<Bullet>().SetDirection(Directions.Left);
            newBullet2.GetComponent<Bullet>().SetDirection(Directions.Left);
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
        if (collision.CompareTag("TopBorder")) 
        {
            _rigidBody.velocity = new Vector2(0, -_speedModifierY);
        }
        if (collision.CompareTag("BottomBorder"))
        {
            _rigidBody.velocity = new Vector2(0, _speedModifierY);
        }
    }

    public int onHit()
    {
        _health--;
        return _health;
    }
}
