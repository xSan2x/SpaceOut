using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    GameSettings gms;

    // Start is called before the first frame update
    void Start()
    {
        gms = GameSettings.instance;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 6 * gms._gameSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Shot")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
