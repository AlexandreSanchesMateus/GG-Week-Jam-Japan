using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : MonoBehaviour
{
    [Header ("General Settings")]
    [SerializeField]
    protected int _life;

    [Header ("Percentage Of Phase")]
    [SerializeField]
    [Range (0,100)]
    private int _FirstStage = 1;
    [SerializeField]
    [Range(0, 100)]
    private int _SecondStage = 1;

    protected STAGE _stage;


    public virtual void Start()
    {
        _FirstStage = (_FirstStage * _life) / 100;

        if ((_SecondStage * _life) / 100 <= _life - _FirstStage)
            _SecondStage = _life - (_FirstStage + (_SecondStage * _life) / 100);
        else
        {
            _SecondStage = 0;
            Debug.LogWarning("Warning : La somme des porcentages de chaque phase est supérieur à 100%. La deuxième et/ou troisième phase sera ignoré");
        }
    }

    protected void TakeDamage(int damage)
    {
        _life -= damage;

        if (_life <= 0)
        {
            _stage = STAGE.DEAD;
            BossDeath();
        }
        else if (_life >= _FirstStage)
            _stage = STAGE.NORMAL;
        else if (_life >= _SecondStage)
            _stage = STAGE.HARD;
        else
            _stage = STAGE.IMPOSSIBLE;
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
    IMPOSSIBLE,
    DEAD
}