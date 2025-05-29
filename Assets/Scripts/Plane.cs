using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plane : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    Vector2 speed;
    [SerializeField] float _speedMultiplier;
    
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _rightGun;
    [SerializeField] GameObject _leftGun;
    
    [SerializeField] float _bulletCooldown;
    float _timer;

    [SerializeField] int _health;
    GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _gameManager.SetHealth(_health);
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        speed.x = 0;
        speed.y = 0;

        speed.x = Input.GetAxis("Horizontal") * _speedMultiplier;
        speed.y = Input.GetAxis("Vertical") * _speedMultiplier;

        _rigidBody.velocity = speed;
    }

    void Fire()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (_timer >= _bulletCooldown)
            {
                var newBullet1 = Instantiate(_bullet);
                var newBullet2 = Instantiate(_bullet);
                newBullet1.transform.position = _rightGun.transform.position;
                newBullet2.transform.position = _leftGun.transform.position;
                newBullet1.GetComponent<Bullet>().SetDirection(Directions.Right);
                newBullet2.GetComponent<Bullet>().SetDirection(Directions.Right);
                _timer = 0;
            }
        }
        _timer += Time.deltaTime;
    }

    public void DecreaseHealth(Damages damage) 
    {
        _health -= (int)damage;
        _gameManager.SetHealth(_health);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            DecreaseHealth(Damages.EnemyCollide);
        }

        if (collision.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            DecreaseHealth(Damages.BulletHit);
        }
    }
}
