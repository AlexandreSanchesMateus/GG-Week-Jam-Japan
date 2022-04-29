using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yurei : BossBase
{
    [Header("Ghost General Settings")]
    [SerializeField] private GameObject _ghostPrefab;
    [SerializeField] private List<Transform> _spawnPos = new List<Transform>(10);
    [SerializeField] private float _timeToAttack = 10;
    [SerializeField] private float _newTimeToAttack = 7;
    [SerializeField] [Range(0,50)] int _reductionDamage = 10;
    
    private bool reductionDamageEnable;
    private float _currentTimeToAttack;

    private List<GameObject> _yureiGhost = new List<GameObject>();
    private GameObject _realYurei;

    private bool hasBeenHit = false;
    public bool CanChangeWith = false;
    private int _nbChange = 0;

    [Header("Curved Fireball Attack Settings")]
    [SerializeField] private GameObject _curvedProjectil;
    [SerializeField] private Transform _LCurvedProjectilSpawn;
    [SerializeField] private Transform _RCurvedProjectilSpawn;
    [SerializeField] int _PDamage;
    [SerializeField] float _PSpeed;
    [SerializeField] float _PPeriod;
    [SerializeField] float _PAmplitude;

    [Header("Fireball Attack Settings")]
    [SerializeField] private GameObject _fireballPrefab;
    [SerializeField] private float _fireBallSpeed;

    bool isInDefaultPlace;

    public override void Start()
    {
        base.Start();
        _realYurei = Instantiate<GameObject>(_ghostPrefab, _spawnPos[6].position, Quaternion.identity);
        _realYurei.GetComponent<GhostYurei>().ActiveGhost(true);
        _yureiGhost.Add(_realYurei);
        StartCoroutine(SpawnCurvedBullet());
        _currentTimeToAttack = _timeToAttack;
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            KageBunshinNoJutsu();
        }*/

        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(5);
        }

        if (!_isAttacking)
        {
            _isAttacking = true;
            CanChangeWith = false;
            int rd = Random.Range(0, 2);

            switch (_stage)
            {
                case STAGE.NORMAL:
                    reductionDamageEnable = false;
                    _currentTimeToAttack = _timeToAttack;
                    if (rd > 0)
                        CanChangeWith = true;
                    Invoke("KageBunshinNoJutsu", 2);
                    break;

                case STAGE.HARD:
                    reductionDamageEnable = true;
                    _currentTimeToAttack = _newTimeToAttack;
                    if (rd > 0)
                        CanChangeWith = true;
                    Invoke("KageBunshinNoJutsu", 2);
                    break;
            }
        }
    }

    private IEnumerator SpawnCurvedBullet()
    {
        yield return new WaitForSeconds(3f);
        Quaternion rotation = Quaternion.identity;
        Vector3 spawnPos = _RCurvedProjectilSpawn.position;
        if (Random.Range(0, 2) > 0) {
            rotation = Quaternion.Euler(0, 180, 0);
            spawnPos = _LCurvedProjectilSpawn.position;
        }

        GameObject instance = Instantiate<GameObject>(_curvedProjectil, spawnPos, rotation, BossManager.instance.transform.GetChild(0));
        CurvedFireball mouvement = instance.GetComponent<CurvedFireball>();
        mouvement.speed = _PSpeed;
        mouvement.period = _PPeriod;
        mouvement.amplitude = _PAmplitude;

        instance.GetComponent<CollisionPlayer>()._damage = _PDamage;

        StartCoroutine("SpawnCurvedBullet");
    }

    private void KageBunshinNoJutsu()
    {
        Debug.Log("Attack");
        ClearAllGhost();
        isInDefaultPlace = false;
        List<Transform> selectedPlace = new List<Transform>(_spawnPos);
        for(int i = 0; i < 5; i++)
        {
            selectedPlace.Remove(selectedPlace[Random.Range(0,selectedPlace.Count)]);
        }

        foreach (Transform location in selectedPlace)
        {
            GameObject instance = Instantiate<GameObject>(_ghostPrefab, location.position, Quaternion.identity);
            _yureiGhost.Add(instance);
        }

        _realYurei = _yureiGhost[Random.Range(0, _yureiGhost.Count)];
        _realYurei.GetComponent<GhostYurei>().ActiveGhost(true);
        Invoke("Fusion", _currentTimeToAttack);
    }

    private void Fusion()
    {
        ClearAllGhost();
        CancelInvoke();
        _realYurei = Instantiate<GameObject>(_ghostPrefab, _spawnPos[6].position, Quaternion.identity);
        _yureiGhost.Add(_realYurei);
        _realYurei.GetComponent<GhostYurei>().ActiveGhost(true);
        hasBeenHit = false;
        _isAttacking = false;
        isInDefaultPlace = true;
        _nbChange = 0;

        if (CanChangeWith && _nbChange == 3)
            base.TakeDamage(20);
    }

    private void ChangeYurei()
    {
        CancelInvoke();
        _nbChange++;

        _realYurei.GetComponent<GhostYurei>().ActiveGhost(false);
        _realYurei = _yureiGhost[Random.Range(0, _yureiGhost.Count)];
        _realYurei.GetComponent<GhostYurei>().ActiveGhost(true);

        hasBeenHit = false;
        Invoke("Fusion", _currentTimeToAttack);
    }

    private void ClearAllGhost()
    {
        foreach(GameObject ghost in _yureiGhost)
        {
            ghost.GetComponent<GhostYurei>().StartCoroutine("Disappear");
        }
        _yureiGhost.Clear();
    }

    /*private IEnumerator FireballAttack()
    {
        yield return new WaitForSeconds(1);

        // From a ghost
        // GameObject instance = Instantiate<GameObject>(_fireballPrefab, _firePos);
        // Vector2 DirPlayer = _player.transform.position - transform.position;
        // instance.GetComponent<Rigidbody2D>().AddForce(DirPlayer.normalized * _fireBallSpeed);

        yield return new WaitForSeconds(1);
        _isAttacking = false;
    }*/

    public override void TakeDamage(int damage)
    {
        if (!reductionDamageEnable)
            base.TakeDamage(damage);
        else
            base.TakeDamage(damage * _reductionDamage / 100);

        if (!isInDefaultPlace && !hasBeenHit)
        {
            CancelInvoke();
            if (CanChangeWith && _nbChange < 3)
                Invoke("ChangeYurei", 0.8f);
            else
                Invoke("Fusion", 1.5f);
        }
        else
            return;
        hasBeenHit = true;
    }

    public override void BossDeath()
    {
        Debug.Log("Hello Enfant");
    }
}
