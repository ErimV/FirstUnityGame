using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    [SerializeField] float _speedModifier;
    [SerializeField] int _health;

    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _gun1;
    [SerializeField] GameObject _gun2;
    [SerializeField] GameObject _gun3;
    [SerializeField] float _bulletCooldown;
    float _timer;
    float _angle = 0;

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
            var newBullet1 = Instantiate(_bullet);
            var newBullet2 = Instantiate(_bullet);
            var newBullet3 = Instantiate(_bullet);
            newBullet1.transform.position = _gun1.transform.position;
            newBullet2.transform.position = _gun2.transform.position;
            newBullet3.transform.position = _gun3.transform.position;

            switch ((int)_rigidBody.rotation % 360)
            {
                case 0:
                    newBullet1.GetComponent<Bullet>().SetDirection(Directions.TopLeft);
                    newBullet2.GetComponent<Bullet>().SetDirection(Directions.TopRight);
                    newBullet3.GetComponent<Bullet>().SetDirection(Directions.Bottom);
                    break;
                case 60:
                    newBullet1.GetComponent<Bullet>().SetDirection(Directions.BottomLeft);
                    newBullet2.GetComponent<Bullet>().SetDirection(Directions.Top);
                    newBullet3.GetComponent<Bullet>().SetDirection(Directions.BottomRight);
                    break;
                case 120:
                    newBullet1.GetComponent<Bullet>().SetDirection(Directions.Bottom);
                    newBullet2.GetComponent<Bullet>().SetDirection(Directions.TopLeft);
                    newBullet3.GetComponent<Bullet>().SetDirection(Directions.TopRight);
                    break;
                case 180:
                    newBullet1.GetComponent<Bullet>().SetDirection(Directions.BottomRight);
                    newBullet2.GetComponent<Bullet>().SetDirection(Directions.BottomLeft);
                    newBullet3.GetComponent<Bullet>().SetDirection(Directions.Top);
                    break;
                case 240:
                    newBullet1.GetComponent<Bullet>().SetDirection(Directions.TopRight);
                    newBullet2.GetComponent<Bullet>().SetDirection(Directions.Bottom);
                    newBullet3.GetComponent<Bullet>().SetDirection(Directions.TopLeft);
                    break;
                case 300:
                    newBullet1.GetComponent<Bullet>().SetDirection(Directions.Top);
                    newBullet2.GetComponent<Bullet>().SetDirection(Directions.BottomRight);
                    newBullet3.GetComponent<Bullet>().SetDirection(Directions.BottomLeft);
                    break;
            }
            
            _angle += 60f;
            _rigidBody.MoveRotation(_angle);
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
