using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : MonoBehaviour
{
    public static BossBase instance { get; private set; }

    [Header ("General Settings")]
    [SerializeField] protected int _life;
    private int maxLife;
    [SerializeField] private float currentLifeUI;

    [Header ("Percentage Of Phase")]
    [SerializeField] [Range(0, 100)] private int _FirstStage = 1;

    protected STAGE _stage;
    protected bool _isAttacking;

    [Header("UI")] 
    [SerializeField] private GameObject lifeBar;


    public virtual void Start()
    {
        instance = this;
        _FirstStage = (_FirstStage * _life) / 100;

        if (_FirstStage == 100)
            Debug.LogWarning("Warning : le pourcentage de la 1er phase est �gal � 100%. La deuxi�me sera ignor�");

        maxLife = _life;
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

        currentLifeUI = (float)(_life * 5) / maxLife;
        lifeBar.transform.localScale = new Vector3(currentLifeUI, lifeBar.transform.localScale.y, lifeBar.transform.localScale.z);
        Debug.Log(currentLifeUI);
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