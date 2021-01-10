using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighBerserk : MonoBehaviour
{
    GameSettings gms;
    Player player;
    Transform playerTransform;
    float shotCD = 0;
    float shotSpawn = 0.3f;
    int HP = 3;
    public GameObject shotPrefab;

    // Start is called before the first frame update
    void Start()
    {
        gms = GameSettings.instance;
        player = Player.instance;
        playerTransform = player.transform;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -7.5f * gms._gameSpeed);
        StartCoroutine("Brake");
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
            _pos.x -= 0.75f * gms._gameSpeed * Time.deltaTime;
            transform.position = _pos;
        }
        else if (_pos.x < playerTransform.position.x)
        {
            _pos.x += 0.75f * gms._gameSpeed * Time.deltaTime;
            transform.position = _pos;
        }
    }

    void SpawnShot()
    {
        Vector3 _enemyPos = transform.position;
        _enemyPos.y -= 0.25f;
        Instantiate(shotPrefab, _enemyPos, Quaternion.identity);
        _enemyPos.x -= 0.3f;
        Instantiate(shotPrefab, _enemyPos, Quaternion.identity);
        _enemyPos.x += 0.6f;
        Instantiate(shotPrefab, _enemyPos, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerShot")
        {
            HP -= 1;
            Destroy(collision.gameObject);
            if (HP < 1)
            {
                Destroy(this.gameObject);
            }
        }
    }
    IEnumerator Brake()
    {
        yield return new WaitForSecondsRealtime(0.04f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -0.75f * gms._gameSpeed);
    }
}
