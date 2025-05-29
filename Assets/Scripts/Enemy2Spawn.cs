using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Spawn : MonoBehaviour
{
    [SerializeField] GameObject _enemy;
    [SerializeField] Transform _topLeftSpawnPoint;
    [SerializeField] Transform _topRightSpawnPoint;
    [SerializeField] Transform _bottomSpawnPoint;
    [SerializeField] float _spawnCooldown;
    float _timer;
    float _minX;
    float _maxX;
    float x;
    float y;
    int _random;

    void Start()
    {
        
    }

    void Update()
    {
        Spawn();
    }

    void Spawn()
    {
        _minX = _topLeftSpawnPoint.position.x;
        _maxX = _topRightSpawnPoint.position.x;

        if (_timer >= _spawnCooldown)
        {
            x = Random.Range(_minX, _maxX);
            _random = Random.Range(0, 2);
            if (_random == 0)   //%50 Change of spawning
            {
                if (Random.Range(0, 2) == 0) y = _topLeftSpawnPoint.position.y;

                else y = _bottomSpawnPoint.position.y;

                Instantiate(_enemy).transform.position = new Vector3(x, y, 0);
            }
            _timer = 0;
        }
        _timer += Time.deltaTime;
    }
}
