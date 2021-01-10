using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAngularShot : MonoBehaviour
{
    GameSettings gms;
    float gameSpeed;
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(float angle)
    {
        transform.Rotate(new Vector3(0,0,angle));
        gms = GameSettings.instance;
        GetComponent<Rigidbody2D>().velocity = -transform.up * gms._gameSpeed;
    }
}
