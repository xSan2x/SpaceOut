using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    float _rotatingSpeed;
    float _offsetX;
    GameSettings gms;

    // Start is called before the first frame update
    void Start()
    {
        gms = GameSettings.instance;
        _rotatingSpeed = Random.Range(-180, 180);
        _offsetX = Random.Range(-0.2f, 0.2f);
        int _rnd = Random.Range(1, 4);
        GetComponent<ConstantForce2D>().force = new Vector2(_offsetX,-1 * gms._gameSpeed);
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/SpaceGarbage" + _rnd);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0, _rotatingSpeed * Time.deltaTime));
    }
}
