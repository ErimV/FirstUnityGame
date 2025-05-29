using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject _enemy1;
    [SerializeField] GameObject _enemy2;
    [SerializeField] GameObject _enemy3;
    [SerializeField] Transform _topSpawnPoint;
    [SerializeField] Transform _bottomSpawnPoint;
    [SerializeField] float _enemySpawnCooldown;
    float _timer;
    float _minY;
    float _maxY;
    float x;
    float y;
    int _random;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    void Spawn()
    {
        _minY = _bottomSpawnPoint.position.y;
        _maxY = _topSpawnPoint.position.y;
        x = _bottomSpawnPoint.position.x;

        if (_timer >= _enemySpawnCooldown)
        {
            _random = Random.Range(0, 100);
            y = Random.Range(_minY, _maxY);

            if (_random < 50) Instantiate(_enemy1).transform.position = new Vector3(x, y, 0);    //%50 Chance Enemy1

            else if (_random >= 50 && _random < 70) Instantiate(_enemy2).transform.position = new Vector3(x, y, 0); //%20 Chance Enemy2

            else if (_random >= 70 && _random < 100) Instantiate(_enemy3).transform.position = new Vector3(x, y, 0); //%30 Chance Enemy3

            _timer = 0;
        }
        _timer += Time.deltaTime;
    }
}
