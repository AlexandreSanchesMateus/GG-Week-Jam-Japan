using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitsune : BossBase
{
    public int b;
    public float c;
    public float d;
    public float angleToInvers;

    [Header("Tails Attack Settings")]
    [SerializeField] private GameObject _tailPrefab;
    [SerializeField] private float _timeBeforeAttack = 1;
    [SerializeField] [Range(0,50)] private int _reductionSecondPhase = 10;

    private GameObject[] _belowTailPlace = new GameObject[9];
    private GameObject[] _sideTailPlace = new GameObject[9];
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
        float spacer = (BossManager.instance._screenBottomRight.x - BossManager.instance._screenTopLeft.x) / 10.0f;
        float distance = spacer;

        for(int i = 0; i < _belowTailPlace.Length; i++)
        {
            _belowTailPlace[i] = Instantiate<GameObject>(_tailPrefab, new Vector3(BossManager.instance._screenTopLeft.x + distance,
                BossManager.instance._screenBottomRight.y + 1, -1), Quaternion.identity, _parentTail);
            distance += spacer;
        }

        spacer = (BossManager.instance._screenTopLeft.y - BossManager.instance._screenBottomRight.y) / 4.0f;
        distance = spacer;
        int index = 0;
        for (int i = 0; i < 3; i++)
        {
            _sideTailPlace[index] = Instantiate<GameObject>(_tailPrefab, new Vector3(BossManager.instance._screenTopLeft.x -1,
                BossManager.instance._screenBottomRight.y + distance, -1), Quaternion.Euler(0,0,-90), _parentTail);

            _sideTailPlace[++index] = Instantiate<GameObject>(_tailPrefab, new Vector3(BossManager.instance._screenBottomRight.x + 1,
                BossManager.instance._screenBottomRight.y + distance, -1), Quaternion.Euler(0,0,90), _parentTail);

            index++;
            distance += spacer;
        }

        _sideTailPlace[6] = _belowTailPlace[2];
        _sideTailPlace[7] = _belowTailPlace[4];
        _sideTailPlace[8] = _belowTailPlace[6];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
            Debug.Log("Life : " + _life + "    Phase : " + _stage.ToString());

            foreach(GameObject tail in _sideTailPlace)
            {
                tail.GetComponent<SpriteRenderer>().color = Color.black;
            }
        }


        float degres = b * Mathf.Deg2Rad;
        d = Mathf.Tan(degres);
        Vector2 Direc = new Vector2(Mathf.Cos(degres), Mathf.Sin(degres));
        Debug.DrawRay(Vector3.zero, Direc * 2, Color.black);
        c = Direc.y / Direc.x;

        Vector3 VectorToInvers = BossManager.instance._screenTopLeft - Vector3.zero;
        angleToInvers = Mathf.Atan2(VectorToInvers.y, VectorToInvers.x);
        Debug.DrawRay(Vector3.zero, VectorToInvers, Color.green);

        float coefAngulair = Mathf.Tan(degres);

        Vector2 positionTail = Vector2.zero;
        if(Mathf.Abs(BossManager.instance._screenBottomRight.x * coefAngulair) < BossManager.instance._screenBottomRight.x / 2)
            positionTail = new Vector2(BossManager.instance._screenBottomRight.x, BossManager.instance._screenBottomRight.x * coefAngulair);
        // inverse -> positionTail = new Vector2(BossManager.instance._screenTopLeft.x, BossManager.instance._screenTopLeft.x * coefAngulair);
        else
            positionTail = new Vector2(BossManager.instance._screenTopLeft.y / coefAngulair, BossManager.instance._screenTopLeft.y);


        //positionTail = new Vector2(BossManager.instance._screenTopLeft.y, BossManager.instance._screenTopLeft.y * coefAngulair);

        /*if (coefAngulair * BossManager.instance._screenTopLeft.x > BossManager.instance._screenBottomRight.y)
            positionTail = new Vector2(BossManager.instance._screenTopLeft.x, BossManager.instance._screenBottomRight.y / coefAngulair);
        else
            positionTail = new Vector2(coefAngulair * BossManager.instance._screenTopLeft.x, BossManager.instance._screenBottomRight.y);*/

        Debug.DrawLine(Vector3.zero, positionTail, Color.red);

        /*for (int i = 0; i < 9; i++)
        {
            float degres = (40 * i - 90) * Mathf.Deg2Rad;
            Vector2 Direc = new Vector2(Mathf.Cos(degres), Mathf.Sin(degres));
            Debug.DrawRay(transform.position, Direc, Color.green);

            float coefAngulair = Mathf.Tan(degres);

            *//*Vector2 positionTail = new Vector2(Screen.height / 2, Screen.width / 2 * coefAngulair);

            if(positionTail.y > transform.position.y + Screen.width/2)
                positionTail = new Vector2((Screen.height / 2 )/ coefAngulair,Screen.height / 2);

            Debug.DrawLine(Vector3.zero, positionTail, Color.red);*//*
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
        // animation d'entrée
        yield return new WaitForSeconds(2);
        // animation idle
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
        // animation de sortie
        _isAttacking = false;
    }

    private IEnumerator AroundTailsAttack()
    {
        yield return new WaitForSeconds(1);
        _isAttacking = false;
    }


    public override void BossDeath()
    {
        Debug.Log("Hello Enfant");
    }
}
