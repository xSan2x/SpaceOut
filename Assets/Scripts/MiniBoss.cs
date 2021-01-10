using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    // Start is called before the first frame update
    float shotCD = 0;
    float shotSpawn = 0.5f;
    int HP = 5;
    public GameObject shotPrefab;
    GameSettings gms;

    // Start is called before the first frame update
    void Start()
    {
        gms = GameSettings.instance;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -0.5f * gms._gameSpeed);
        GetComponent<Rigidbody2D>().angularVelocity = 35f;
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
        GameObject _shot;
        float _dist = 0.5f;
        float _y = 0;
        float _x = 0;
        Vector3 _enemyPos = transform.position;
        _y = _dist * Mathf.Cos(Mathf.Deg2Rad*transform.rotation.eulerAngles.z);
        _x = _dist * Mathf.Sin(Mathf.Deg2Rad*transform.rotation.eulerAngles.z);
        _enemyPos.y -= _y;
        _enemyPos.x += _x;
        _shot = Instantiate(shotPrefab, _enemyPos, Quaternion.identity);
        _shot.GetComponent<EnemyAngularShot>().Init(transform.rotation.eulerAngles.z);
        _enemyPos = transform.position;
        _y = _dist * Mathf.Cos(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z+45));
        _x = _dist * Mathf.Sin(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z+45));
        _enemyPos.y -= _y;
        _enemyPos.x += _x;
        _shot = Instantiate(shotPrefab, _enemyPos, Quaternion.identity);
        _shot.GetComponent<EnemyAngularShot>().Init(transform.rotation.eulerAngles.z+45);
        _enemyPos = transform.position;
        _y = _dist * Mathf.Cos(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + 90));
        _x = _dist * Mathf.Sin(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + 90));
        _enemyPos.y -= _y;
        _enemyPos.x += _x;
        _shot = Instantiate(shotPrefab, _enemyPos, Quaternion.identity);
        _shot.GetComponent<EnemyAngularShot>().Init(transform.rotation.eulerAngles.z + 90);
        _enemyPos = transform.position;
        _y = _dist * Mathf.Cos(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + 135));
        _x = _dist * Mathf.Sin(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + 135));
        _enemyPos.y -= _y;
        _enemyPos.x += _x;
        _shot = Instantiate(shotPrefab, _enemyPos, Quaternion.identity);
        _shot.GetComponent<EnemyAngularShot>().Init(transform.rotation.eulerAngles.z + 135);
        _enemyPos = transform.position;
        _y = _dist * Mathf.Cos(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + 180));
        _x = _dist * Mathf.Sin(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + 180));
        _enemyPos.y -= _y;
        _enemyPos.x += _x;
        _shot = Instantiate(shotPrefab, _enemyPos, Quaternion.identity);
        _shot.GetComponent<EnemyAngularShot>().Init(transform.rotation.eulerAngles.z + 180);
        _enemyPos = transform.position;
        _y = _dist * Mathf.Cos(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + 225));
        _x = _dist * Mathf.Sin(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + 225));
        _enemyPos.y -= _y;
        _enemyPos.x += _x;
        _shot = Instantiate(shotPrefab, _enemyPos, Quaternion.identity);
        _shot.GetComponent<EnemyAngularShot>().Init(transform.rotation.eulerAngles.z + 225);
        _enemyPos = transform.position;
        _y = _dist * Mathf.Cos(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + 270));
        _x = _dist * Mathf.Sin(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + 270));
        _enemyPos.y -= _y;
        _enemyPos.x += _x;
        _shot = Instantiate(shotPrefab, _enemyPos, Quaternion.identity);
        _shot.GetComponent<EnemyAngularShot>().Init(transform.rotation.eulerAngles.z + 270);
        _enemyPos = transform.position;
        _y = _dist * Mathf.Cos(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + 315));
        _x = _dist * Mathf.Sin(Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + 315));
        _enemyPos.y -= _y;
        _enemyPos.x += _x;
        _shot = Instantiate(shotPrefab, _enemyPos, Quaternion.identity);
        _shot.GetComponent<EnemyAngularShot>().Init(transform.rotation.eulerAngles.z + 315);
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
        yield return new WaitForSecondsRealtime(0.05f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -0.25f * gms._gameSpeed);
    }
}
