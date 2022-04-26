using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitsune : BossBase
{
    [SerializeField] Transform Player;

    private bool _isAttacking = false;

    [Header ("Fireball Settings")]
    [SerializeField] private Transform _firePos;
    [SerializeField] private GameObject _fireballPrefab;
    [SerializeField] private float _speed;

    public override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
            Debug.Log("Life : " + _life + "    Phase : " + _stage.ToString());
        }

        int random = Random.Range(0, 3);

        if (!_isAttacking)
        {
            _isAttacking = true;
            switch (_stage)
            {
                case STAGE.NORMAL:
                    StartCoroutine(FireballAttack());
                    break;
                case STAGE.HARD:
                    break;
                case STAGE.IMPOSSIBLE:
                    break;
            }
        }
    }

    private IEnumerator FireballAttack()
    {
        yield return new WaitForSeconds(1);

        GameObject instance = Instantiate<GameObject>(_fireballPrefab, _firePos);
        Vector2 DirPlayer = Player.transform.position - transform.position;
        instance.GetComponent<Rigidbody2D>().AddForce(DirPlayer.normalized * _speed);

        yield return new WaitForSeconds(1);
        _isAttacking = false;
    }


    public override void BossDeath()
    {
        Debug.Log("Hello Enfant");
    }
}
