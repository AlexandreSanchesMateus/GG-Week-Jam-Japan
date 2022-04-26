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

    public virtual void SpecialAttack()
    {
        Debug.Log("Spécial Attack deals : " + damage );
    }

    private void TakeDamage()
    {
        Debug.Log("The player have " + life);
    }
 
}
