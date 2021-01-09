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
    bool gameStarted = false;
    public Transform bigShip;
    public GameObject garbagePrefab;
    public GameObject lowEnemyPrefab;
    public GameObject commetPrefab;
    public GameObject bigEnemyPrefab;
    public GameObject berserkEnemyPrefab;

    private delegate void OnTeleport(Vector3 pos);
    private static OnTeleport onTeleport;

    float spawnCooldown = 0f;

    float _garbageSpawn = 1.5f;
    float _lightEnemySpawn = 2.5f;
    float _commetSpawn = 4f;
    float _2hpEnemySpawn = 5.5f;
    float _berserkEnemySpawn = 20f;
    float _highBerserkEnemySpawn = 50f;
    float _miniBossSpawn = 120f;
    float _bossSpawn = 300f;

    float _garbageCD = 0;
    float _lightEnemyCD = 0;
    float _commetCD = 0;
    float _2hpEnemyCD = 0;
    float _berserkEnemyCD = 0;
    float _highBerserkEnemyCD = 0;
    float _miniBossCD = 0;
    float _bossCD = 0;

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
        if (!gameStarted)
        {
            bigShip.Translate(new Vector3(0, -0.01f, 0) * gms._gameSpeed);
            if (bigShip.position.y < -11)
            {
                Destroy(bigShip.gameObject);
                gameStarted = true;
                //StartCoroutine("StartShooting");
            }
        } else
        {
            _garbageCD += Time.deltaTime;
            if (_garbageCD > _garbageSpawn)
            {
                _garbageCD = 0;
                SpawnGarbage();
            }
            _lightEnemyCD += Time.deltaTime;
            if(_lightEnemyCD > _lightEnemySpawn)
            {
                _lightEnemyCD = 0;
                SpawnLowEnemy();
            }
            _commetCD += Time.deltaTime;
            if (_commetCD > _commetSpawn)
            {
                _commetCD = 0;
                SpawnCommet();
            }
            _2hpEnemyCD += Time.deltaTime;
            if (_2hpEnemyCD > _2hpEnemySpawn)
            {
                _2hpEnemyCD = 0;
                SpawnBigEnemy();
            }
            _berserkEnemyCD += Time.deltaTime;
            if (_berserkEnemyCD > _berserkEnemySpawn)
            {
                _berserkEnemyCD = 0;
                SpawnBerserkEnemy();
            }
        }
        if(tp_cooldown > tp_max_cooldown && gameStarted)
        {
            teleport_ready = true;
            slider_txt.text = "READY!";
            slider_txt.color = new Color(0,1f,0);
        } else
        {
            if (gameStarted)
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
                        playerTP.color = new Color(1f, 1f, 1f, 0);
                        StartCoroutine(TeleportOnCooldown(slider_txt.color));
                        break;
                }
            }
        }
    }

    void StartTeleport(Vector3 _pos)
    {
        StartCoroutine(TeleportAnimation(_pos));
    }
    IEnumerator TeleportAnimation(Vector3 _pos)
    {
        TPhint_transform.position = _pos;
        TPhint_sprite.sprite = Resources.Load<Sprite>("Images/Teleportation2");
        playerTP.sprite = Resources.Load<Sprite>("Images/Teleportation4");
        yield return new WaitForFixedUpdate();
        TPhint_sprite.sprite = Resources.Load<Sprite>("Images/Teleportation3");
        playerTP.sprite = Resources.Load<Sprite>("Images/Teleportation3");
        yield return new WaitForFixedUpdate();
        playerTP.sprite = Resources.Load<Sprite>("Images/Teleportation2");
        yield return new WaitForFixedUpdate();
        TPhint_sprite.sprite = Resources.Load<Sprite>("Images/Teleportation1");
        playerTP.sprite = Resources.Load<Sprite>("Images/Teleportation1");
        playerTransform.position = _pos;
        teleport_ready = false;
        playerTP.color = new Color(1f, 1f, 1f, 0);
        TPhint_sprite.color = new Color(1f, 1f, 1f, 0);
        tp_cooldown = 0;
        slider_txt.text = "WAIT!";
        slider_txt.color = new Color(1f, 1f, 0);
    }
    IEnumerator TeleportOnCooldown(Color _color)
    {
        slider_txt.color = new Color(1, 0, 0);
        yield return new WaitForSecondsRealtime(0.1f);
        slider_txt.color = _color;
        yield return new WaitForSecondsRealtime(0.1f);
        slider_txt.color = new Color(1, 0, 0);
        yield return new WaitForSecondsRealtime(0.1f);
        slider_txt.color = _color;
    }
    void SpawnGarbage()
    {
        float _rnd = Random.Range(-2.75f, 2.75f);
        Instantiate(garbagePrefab, new Vector3(_rnd, 4.9f, -1.4f), Quaternion.identity);
    }
    void SpawnLowEnemy()
    {
        float _rnd = Random.Range(-2.75f, 2.75f);
        Instantiate(lowEnemyPrefab, new Vector3(_rnd, 4.75f, -1.4f), Quaternion.identity);
    }
    void SpawnCommet()
    {
        float _rnd = Random.Range(-2.75f, 2.75f);
        Instantiate(commetPrefab, new Vector3(_rnd, 4.8f, -1.4f), Quaternion.identity);
    }
    void SpawnBigEnemy()
    {
        float _rnd = Random.Range(-2.5f, 2.5f);
        Instantiate(bigEnemyPrefab, new Vector3(_rnd, 4.75f, -1.4f), Quaternion.identity);
    }
    void SpawnBerserkEnemy()
    {
        float _rnd = Random.Range(-2.5f, 2.5f);
        Instantiate(berserkEnemyPrefab, new Vector3(_rnd, 4.75f, -1.4f), Quaternion.identity);
    }
}
