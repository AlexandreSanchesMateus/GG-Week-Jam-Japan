using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
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
                GetComponent<PlayerMouvement>().allowDJ = power[0];
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
        Debug.Log("Start Inv");
        yield return new WaitForSeconds(_invTime);
        Debug.Log("Stop Inv");

        isInv = false;
        isInvOnCd = true;

        yield return new WaitForSeconds(_invCd);
        Debug.Log("CD Inv OFF");
        isInvOnCd = false;
    }

    public void TakeDamage()
    {
        if (!isInv)
        {
            Debug.Log("The player took damage and now have " + life);
            
        }
    }
 
}
