using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings instance = null;
    public float _gameSpeed;
    public float _startCooldown;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        if (instance == this)
        {
            Destroy(gameObject);
        }
        _gameSpeed = 1f;
        _startCooldown = 1f;
    }

}
