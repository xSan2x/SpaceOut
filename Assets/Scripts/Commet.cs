using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commet : MonoBehaviour
{
    GameSettings gms;
    float _offsetX;

    // Start is called before the first frame update
    void Start()
    {
        gms = GameSettings.instance;
        _offsetX = Random.Range(-0.1f, 0.1f);
        GetComponent<ConstantForce2D>().force = new Vector2(_offsetX, -10 * gms._gameSpeed);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Shot":
                Destroy(collision.gameObject);
                break;
            case "PlayerShot":
                Destroy(collision.gameObject);
                break;
            default:
                break;
        }
    }
}
