using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    GameSettings gms;

    // Start is called before the first frame update
    void Start()
    {
        gms = GameSettings.instance;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5 * gms._gameSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
