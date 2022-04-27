using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yurei : BossBase
{
    [Header("Ghost General Settings")]
    [SerializeField] private GameObject _ghostPrefab;
    [SerializeField] private List<Transform> _spawnPos = new List<Transform>(10);
    private List<GameObject> _yureiGhost = new List<GameObject>();

    [Header("Curved Fireball Attack Settings")]
    [SerializeField] private GameObject _curvedProjectil;
    [SerializeField] private Transform _LCurvedProjectilSpawn;
    [SerializeField] private Transform _RCurvedProjectilSpawn;
    [SerializeField] float _PSpeed;
    [SerializeField] float _PPeriod;
    [SerializeField] float _PAmplitude;

    [Header("Fireball Attack Settings")]
    [SerializeField] private GameObject _fireballPrefab;
    [SerializeField] private float _fireBallSpeed;


    public override void Start()
    {
        base.Start();
        _yureiGhost.Add(Instantiate<GameObject>(_ghostPrefab, _spawnPos[6].position, Quaternion.identity));

        InvokeRepeating("SpawnCurvedBullet", 5f, 2.5f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("BOUR");
            _yureiGhost[0].transform.position = GetRandomPos();
        }

        switch (_stage)
        {
            case STAGE.NORMAL:
                break;
            case STAGE.HARD:
                break;
            case STAGE.IMPOSSIBLE:
                break;
        }
    }

    private Vector2 GetRandomPos()
    {
        return _yureiGhost[Random.Range(0, _yureiGhost.Count)].transform.position;
    }

    private void SpawnCurvedBullet()
    {
        Quaternion rotation = Quaternion.identity;
        Vector3 spawnPos = _RCurvedProjectilSpawn.position;
        if (Random.Range(0, 2) > 0) {
            rotation = Quaternion.Euler(0, 180, 0);
            spawnPos = _LCurvedProjectilSpawn.position;
        }

        CurvedFireball instance = Instantiate<GameObject>(_curvedProjectil, spawnPos, rotation, BossManager.instance.transform.GetChild(0)).GetComponent<CurvedFireball>();
        instance.speed = _PSpeed;
        instance.period = _PPeriod;
        instance.amplitude = _PAmplitude;
    }

    private void KageBunshinNoJutsu()
    {
        List<Transform> selectedPlace = _spawnPos;
        for(int i = 0; i < 4; i++)
        {
            selectedPlace.Remove(selectedPlace[Random.Range(0,selectedPlace.Count)]);
        }

        foreach (Transform location in selectedPlace)
        {
            _yureiGhost.Add(Instantiate<GameObject>(_ghostPrefab, location.position, Quaternion.identity));
        }
    }

    private IEnumerator FireballAttack()
    {
        yield return new WaitForSeconds(1);

        // From a ghost
        // GameObject instance = Instantiate<GameObject>(_fireballPrefab, _firePos);
        // Vector2 DirPlayer = _player.transform.position - transform.position;
        // instance.GetComponent<Rigidbody2D>().AddForce(DirPlayer.normalized * _fireBallSpeed);

        yield return new WaitForSeconds(1);
        _isAttacking = false;
    }

    public override void BossDeath()
    {
        Debug.Log("Hello Enfant");
    }
}
