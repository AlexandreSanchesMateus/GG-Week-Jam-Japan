using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitsune : BossBase
{
    [SerializeField] Transform Player;

    private bool _isAttacking = false;

    [Header("Fireball Attack Settings")]
    [SerializeField] private GameObject _fireballPrefab;
    [SerializeField] private Transform _firePos;
    [SerializeField] private float _fireBallSpeed;

    [Header("Tails Attack Settings")]
    [SerializeField] private GameObject _tailPrefab;
    [SerializeField] private Transform _startPlacement;
    [SerializeField] private Transform _endPlacement;
    [SerializeField] private float _timeBeforeAttack = 1;
    [SerializeField] [Range(0,50)] private int _reductionSecondPhase = 10;

    private GameObject[] _belowTailPlace = new GameObject[9];
    // private GameObject[] _aroundTailPlace = new GameObject[9];
    private Transform _parentTail;

    [Header("Roar Attack Settings")]
    [SerializeField] private GameObject _roarPrefab;
    [SerializeField] private Transform _roarStartPoint;
    [SerializeField] private float _roarSpeed;

    public override void Start()
    {
        base.Start();

        _parentTail = BossManager.instance.transform.GetChild(0);

        _belowTailPlace[0] = Instantiate<GameObject>( _tailPrefab, new Vector3(_startPlacement.position.x, _startPlacement.position.y - 3, -1), Quaternion.identity, _parentTail);
        float spacer = (_endPlacement.position.x - _startPlacement.position.x) / 8.0f;


        for(int i = 1; i < _belowTailPlace.Length; i++)
        {
            _belowTailPlace[i] = Instantiate<GameObject>(_tailPrefab, new Vector3(_belowTailPlace[i - 1].transform.position.x + spacer, _startPlacement.position.y - 3, -1), Quaternion.identity, _parentTail);
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
            Debug.Log("Life : " + _life + "    Phase : " + _stage.ToString());
        }

        /*for (int i = 0; i < 9; i++)
        {
            float degres = 40 * i * Mathf.Deg2Rad;
            // Vector2 Direc = new Vector2(Mathf.Cos(degres), Mathf.Sin(degres));
            // Debug.DrawRay(transform.position, Direc, Color.green);

            float coefAngulair = Mathf.Tan(degres);

            Vector2 positionTail = new Vector2(Screen.height / 2, Screen.width / 2 * coefAngulair);

            if(positionTail.y > transform.position.y + Screen.width/2)
                positionTail = new Vector2((Screen.height / 2 )/ coefAngulair,Screen.height / 2);

            Debug.DrawLine(Vector3.zero, positionTail, Color.red);
        }*/

        if (!_isAttacking)
        {
            _isAttacking = true;
            switch (_stage)
            {
                case STAGE.NORMAL:
                    switch (Random.Range(0, 3))
                    {
                        case 0:
                            StartCoroutine(BelowTailsAttack(_timeBeforeAttack));
                            break;
                        case 1:
                            StartCoroutine(SideTailAttack());
                            break;
                        case 2:
                            StartCoroutine(RoarAttack());
                            break;
                    }
                    break;
                case STAGE.HARD:
                    switch (Random.Range(0, 4))
                    {
                        case 0:
                            StartCoroutine(BelowTailsAttack((_timeBeforeAttack * _reductionSecondPhase)/100));
                            break;
                        case 1:
                            StartCoroutine(SideTailAttack());
                            break;
                        case 2:
                            StartCoroutine(AroundTailsAttack());
                            break;
                        case 3:
                            StartCoroutine(RoarAttack());
                            break;
                    }
                    break;
                case STAGE.IMPOSSIBLE:
                    break;
            }
        }
    }


    private IEnumerator SideTailAttack()
    {
        yield return new WaitForSeconds(2);
        _isAttacking = false;
    }


    private IEnumerator RoarAttack()
    {
        yield return new WaitForSeconds(2);
        GameObject instance = Instantiate<GameObject>( _roarPrefab, _roarStartPoint);
        instance.GetComponent<Rigidbody2D>().AddForce(Vector2.left * _roarSpeed);
        yield return new WaitForSeconds(3);
        _isAttacking = false;
    }


    private IEnumerator BelowTailsAttack(float delay)
    {
        yield return new WaitForSeconds(2);
        _parentTail.transform.position = new Vector3(0,3,0);
        yield return new WaitForSeconds(delay);

        foreach(GameObject tail in _belowTailPlace)
        {
            tail.GetComponent<TailAttack>().TransformTail(true);
        }

        yield return new WaitForSeconds(2);

        foreach (GameObject tail in _belowTailPlace)
        {
            tail.GetComponent<TailAttack>().TransformTail(false);
        }

        yield return new WaitForSeconds(1);

        _parentTail.transform.position = new Vector3(0, 0, 0);
        _isAttacking = false;
    }

    private IEnumerator AroundTailsAttack()
    {
        yield return new WaitForSeconds(1);
        _isAttacking = false;
    }

    /*private IEnumerator FireballAttack()
    {
        yield return new WaitForSeconds(1);

        GameObject instance = Instantiate<GameObject>(_fireballPrefab, _firePos);
        Vector2 DirPlayer = Player.transform.position - transform.position;
        instance.GetComponent<Rigidbody2D>().AddForce(DirPlayer.normalized * _fireBallSpeed);

        yield return new WaitForSeconds(1);
        _isAttacking = false;
    }*/


    public override void BossDeath()
    {
        Debug.Log("Hello Enfant");
    }
}
