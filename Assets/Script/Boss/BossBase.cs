using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : MonoBehaviour
{
    public static BossBase instance { get; private set; }

    [Header ("General Settings")]
    [SerializeField] protected int _life;

    [Header ("Percentage Of Phase")]
    [SerializeField] [Range(0, 100)] private int _FirstStage = 1;

    protected STAGE _stage;
    protected bool _isAttacking;


    public virtual void Start()
    {
        instance = this;
        _FirstStage = (_FirstStage * _life) / 100;

        if (_FirstStage == 100)
            Debug.LogWarning("Warning : le porcentage de la 1er phase est égal à 100%. La deuxième sera ignoré");
    }

    public virtual void TakeDamage(int damage)
    {
        _life -= damage;

        if (_life <= 0)
        {
            _stage = STAGE.DEAD;
            BossDeath();
        }
        else if (_life >= _FirstStage)
            _stage = STAGE.NORMAL;
        else 
            _stage = STAGE.HARD;
    }

    public virtual void BossDeath()
    {
        Debug.Log("Hello Parent");
    }
}

public enum STAGE
{
    NORMAL,
    HARD,
    DEAD
}