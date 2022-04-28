using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public GameObject crossAir;
    public Vector2 dir;

    [Header("Stats")]
    [SerializeField] private int _life;
    public int life
    {
        get { return _life; }
        set { _life = value; }
    }

    [SerializeField] private int _dammage;
    public int damage
    {
        get { return _dammage; }
        set { _dammage = value; }
    }


    public bool[] power = new bool[1];

    [Header("Invincibilité")] 
    [SerializeField] private int invCdTime;
    private bool isInvOnCd;
    [SerializeField] private int invTime;
    private bool isInv;
    [SerializeField] private GameObject Shield;

    [Header("UI")] 
    [SerializeField] private Image[] powersUp;
    [SerializeField] private Image powerUpHolder;
    private bool _spin = false;
    [SerializeField] private Color[] _test;
    private int _curentSpin;
    public AnimationCurve spinSpeed;
    private float deltaSpin;

    public void Aim()
    {
        if (Input.GetKeyDown(KeyCode.H))// PI
        {
            crossAir.transform.localPosition = new Vector2(-3, 0);
        }

        if (Input.GetKeyDown(KeyCode.U))// 3PI/4
        {
            crossAir.transform.localPosition = new Vector2(-2, 2);
        }

        if (Input.GetKeyDown(KeyCode.I))// PI/2
        {
            crossAir.transform.localPosition = new Vector2(0, 3);
        }

        if (Input.GetKeyDown(KeyCode.O))// PI/4
        {
            crossAir.transform.localPosition = new Vector2(2, 2);
        }

        if (Input.GetKeyDown(KeyCode.M))// 0
        {
            crossAir.transform.localPosition = new Vector2(3, 0);
        }
    }
    public virtual void BasicAttack()
    {
        Debug.Log("Basic Attack deals : " + damage);
    }

    public void SpecialAttack()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (power[0])//kitsune = double jump
            {
                PlayerMouvement pm = GetComponent<PlayerMouvement>();
                Rigidbody2D rb = GetComponent<Rigidbody2D>();

                if (pm.hasDJ && !pm.isGrounded)
                {
                    rb.velocity = new Vector2(rb.velocity.x,0);
                    rb.AddForce(new Vector2(0.0f, pm.jumpForce));
                    pm.hasDJ = false;
                }
            }

            if (power[1] && !isInvOnCd)//Yurei = invincibilité
            {
                StartCoroutine(Invinsible(invTime, invCdTime));
            }
        }
    }

    IEnumerator Invinsible(int _invTime, int _invCd)
    {
        isInv = true;
        isInvOnCd = true;
        Shield.SetActive(true);
        yield return new WaitForSeconds(_invTime);
        Shield.SetActive(false);

        isInv = false;
        isInvOnCd = true;

        yield return new WaitForSeconds(_invCd);
        Debug.Log("CD Inv OFF");
        isInvOnCd = false;
    }

    public void RollPowerUp()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _spin = true;

            deltaSpin = 0;
            StartCoroutine(Spin());
            StartCoroutine(StopSpin(5));
        }
    }

    private IEnumerator Spin()
    {
        if (_spin)
        {
            _curentSpin = (_curentSpin + 1) % _test.Length;
            powerUpHolder.color = _test[_curentSpin];
            deltaSpin += Time.deltaTime;
            yield return new WaitForSeconds(spinSpeed.Evaluate(deltaSpin));

            StartCoroutine(Spin());
        }
    }

    private IEnumerator StopSpin(int timer)
    {
        yield return new WaitForSeconds(timer);
        _spin = false;
    }

    public void TakeDamage()
    {
        if (!isInv)
        {
            Debug.Log("The player took damage and now have " + life);
            
        }
    }
 
}
