using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    int shotsCount = 1;
    float shotCD = 0;
    float shotSpawn = 1f;
    float livingChance = 0;
    int HP = 3;
    public GameObject shotPrefab;

    // Start is called before the first frame update
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
    }

    // Update is called once per frame
    void Update()
    {
        shotCD += Time.deltaTime;
        if (shotCD > shotSpawn)
        {
            shotCD = 0;
            SpawnShot();
        }
    }

    void SpawnShot()
    {
        Vector3 _playerPos = transform.position;
        _playerPos.y += 0.25f;
        int _num = 0;
        for(int i = 0; i < shotsCount; i++)
        {
            if (shotsCount % 2 == 0)
            {
                _num = Mathf.FloorToInt(i / 2);
                if (i % 2 == 0)
                {
                    _playerPos.x -= 0.12f * (_num + 1);
                } else
                {
                    _playerPos.x += 0.12f * (_num + 1);
                }
                Instantiate(shotPrefab, _playerPos, Quaternion.identity);
            } else
            {
                _num = Mathf.CeilToInt(i / 2);
                if (i % 2 == 0)
                {
                    _playerPos.x -= 0.12f * _num;
                }
                else
                {
                    _playerPos.x += 0.12f * _num;
                }
                Instantiate(shotPrefab, _playerPos, Quaternion.identity);
            }
        }
    }
}
