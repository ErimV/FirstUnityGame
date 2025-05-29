using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _txtPoint;
    [SerializeField] TextMeshProUGUI _txtHealth;
    [SerializeField] TextMeshProUGUI _txtGameOver;
    int _point;
    void Start()
    {
        _point = 0;
        ShowPoint();
    }

    void Update()
    {
        
    }

    void ShowPoint() 
    {
        _txtPoint.text = "Point: " + _point;
    }

    public void SetHealth(int health)
    {
        _txtHealth.text = "Health: " + health;
        if (health <= 0)
        {
            Time.timeScale = 0.0f;
            _txtHealth.text = "Health: " + 0;
            _txtGameOver.text = "GAME OVER";
        }
    }

    public void IncreasePoint(Points newPoint)
    {
        _point += (int)newPoint;
        ShowPoint();
    }
}
