using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoHPEnemy : MonoBehaviour
{
    float shotCD = 0;
    float shotSpawn = 1f;
    int HP = 2;
    public GameObject shotPrefab;
    GameSettings gms;

    // Start is called before the first frame update
    void Start()
    {
        gms = GameSettings.instance;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -0.5f * gms._gameSpeed);
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
        Vector3 _enemyPos = transform.position;
        _enemyPos.y -= 0.25f;
        Instantiate(shotPrefab, _enemyPos, Quaternion.identity);
        _enemyPos.x -= 0.25f;
        Instantiate(shotPrefab, _enemyPos, Quaternion.identity);
        _enemyPos.x += 0.5f;
        Instantiate(shotPrefab, _enemyPos, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerShot")
        {
            HP -= 1;
            if (HP < 1)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
