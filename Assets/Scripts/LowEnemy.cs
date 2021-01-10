using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowEnemy : MonoBehaviour
{
    float shotCD = 0;
    float shotSpawn = 1f;
    public GameObject shotPrefab;
    GameSettings gms;

    private void Start()
    {
        gms = GameSettings.instance;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5 * gms._gameSpeed);
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
        if(collision.gameObject.tag == "PlayerShot")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
    IEnumerator Brake()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * gms._gameSpeed);
    }
}
