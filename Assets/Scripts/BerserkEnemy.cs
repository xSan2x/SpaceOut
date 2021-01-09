using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkEnemy : MonoBehaviour
{
    float shotCD = 0;
    float shotSpawn = 0.5f;
    public GameObject shotPrefab;
    GameSettings gms;
    Player player;
    Transform playerTransform;

    private void Start()
    {
        gms = GameSettings.instance;
        player = Player.instance;
        playerTransform = player.transform;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1.5f * gms._gameSpeed);
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
        Vector3 _pos = transform.position;
        if (_pos.x > playerTransform.position.x)
        {
            _pos.x -= 1.2f * gms._gameSpeed * Time.deltaTime;
            transform.position = _pos;
        } else if(_pos.x < playerTransform.position.x)
        {
            _pos.x += 1.2f * gms._gameSpeed * Time.deltaTime;
            transform.position = _pos;
        }
    }
    void SpawnShot()
    {
        Vector3 _enemyPos = transform.position;
        _enemyPos.y -= 0.25f;
        _enemyPos.x -= 0.15f;
        Instantiate(shotPrefab, _enemyPos, Quaternion.identity);
        _enemyPos.x += 0.3f;
        Instantiate(shotPrefab, _enemyPos, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerShot")
        {
            Destroy(gameObject);
        }
    }
}
