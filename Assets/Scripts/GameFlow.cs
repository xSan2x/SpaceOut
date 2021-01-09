using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour
{
    GameSettings gms;
    Player player;
    SpriteRenderer playerTP;
    public GameObject TPhint;
    Transform TPhint_transform;
    SpriteRenderer TPhint_sprite;
    Transform playerTransform;
    Camera mainCamera;
    public Slider slider_cooldown;
    public Text slider_txt;
    float tp_max_cooldown;
    float tp_cooldown = 0f;
    int score = 0;
    bool teleport_ready = false;

    private delegate void OnTeleport(Vector3 pos);
    private static OnTeleport onTeleport;


    private void Awake()
    {
        onTeleport += StartTeleport;
    }
    // Start is called before the first frame update
    void Start()
    {
        gms = GameSettings.instance;
        player = Player.instance;
        playerTransform = player.transform;
        playerTP = player.transform.GetChild(0).GetComponent<SpriteRenderer>();
        tp_max_cooldown = gms._startCooldown;
        TPhint_transform = TPhint.transform;
        TPhint_sprite = TPhint.GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(tp_cooldown > tp_max_cooldown)
        {
            teleport_ready = true;
            slider_txt.text = "READY!";
            slider_txt.color = new Color(0,1f,0);
        } else
        {
            tp_cooldown += Time.deltaTime;
            slider_cooldown.value = tp_cooldown;
        }
        if(Input.touchCount > 0)
        {
            Touch firstTouch = Input.GetTouch(0);
            Vector3 _pos;
            if (teleport_ready)
            {
                switch (firstTouch.phase)
                {
                    case TouchPhase.Began:
                        playerTP.color = new Color(1f, 1f, 1f, 1f);
                        TPhint_sprite.color = new Color(1f, 1f, 1f, 1f);
                        _pos = mainCamera.ScreenToWorldPoint(firstTouch.position);
                        _pos.z = -1;
                        TPhint_transform.position = _pos;
                        break;
                    case TouchPhase.Moved:
                        playerTP.color = new Color(1f, 1f, 1f, 1f);
                        TPhint_sprite.color = new Color(1f, 1f, 1f, 1f);
                        _pos = mainCamera.ScreenToWorldPoint(firstTouch.position);
                        _pos.z = -1;
                        TPhint_transform.position = _pos;
                        break;
                    case TouchPhase.Ended:
                        _pos = mainCamera.ScreenToWorldPoint(firstTouch.position);
                        _pos.z = -1;
                        TPhint_transform.position = _pos;
                        onTeleport(_pos);
                        Debug.Log("TP OUT!");
                        break;
                }
                
            }
            else
            {
                switch (firstTouch.phase)
                {
                    case TouchPhase.Began:
                        playerTP.color = new Color(1f, 1f, 1f, 0);
                        TPhint_sprite.color = new Color(1f, 1f, 1f, 0);
                        break;
                    case TouchPhase.Moved:
                        playerTP.color = new Color(1f, 1f, 1f, 0);
                        TPhint_sprite.color = new Color(1f, 1f, 1f, 0);
                        break;
                    case TouchPhase.Ended:
                        TPhint_sprite.color = new Color(1f, 1f, 1f, 0);
                        Debug.Log("TP in cooldown!");
                        break;
                }
                
            }
            
            
        }
    }

    void StartTeleport(Vector3 _pos)
    {
        TPhint_sprite.color = new Color(1f, 1f, 1f, 1f);
        TPhint_transform.position = _pos;
        playerTransform.position = _pos;
        teleport_ready = false;
        tp_cooldown = 0;
        slider_txt.text = "WAIT!";
        slider_txt.color = new Color(1f, 1f, 0);
    }
}
