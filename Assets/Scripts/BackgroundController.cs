using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject starSprite;
    private List<Transform> starsTransforms = new List<Transform>();
    private GameSettings gms;
    readonly float _max_X = 2.95f;
    readonly float _min_X = -2.95f;

    void Start()
    {
        gms = GameSettings.instance;
        StartCoroutine("StarsSpawner"); 
        StartCoroutine("StarsForce");
    }

    IEnumerator StarsSpawner()
    {
        float _spawnCooldown = 0.1f;
        float _rnd;
        while (true)
        {
            _rnd = Random.Range(_min_X, _max_X);
            Vector3 _pos = new Vector3(_rnd, 5, -1f);
            GameObject newStar = Instantiate(starSprite, _pos, Quaternion.identity);
            starsTransforms.Add(newStar.transform);
            yield return new WaitForSecondsRealtime(Mathf.Abs(_rnd/6)*_spawnCooldown);
            _rnd = Random.Range(_min_X, _max_X);
            _pos = new Vector3(_rnd, 5, -1f);
            newStar = Instantiate(starSprite, _pos, Quaternion.identity);
            starsTransforms.Add(newStar.transform);
            yield return new WaitForSecondsRealtime(Mathf.Abs(_rnd / 6) * _spawnCooldown);
            _rnd = Random.Range(_min_X, _max_X);
            _pos = new Vector3(_rnd, 5, -1f);
            newStar = Instantiate(starSprite, _pos, Quaternion.identity);
            starsTransforms.Add(newStar.transform);
            yield return new WaitForSecondsRealtime(_spawnCooldown);
        }
        
    }
    IEnumerator StarsForce()
    {
        while (true)
        {
            starsTransforms = starsTransforms.Where(i => i != null).ToList();
            foreach(Transform _transform in starsTransforms)
            {
                if (_transform.position.y > -5)
                {
                    _transform.Translate(new Vector3(0, -0.05f, 0) * gms._gameSpeed);
                } else
                {
                    GameObject _gm = _transform.gameObject;
                    Destroy(_gm);
                    
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
