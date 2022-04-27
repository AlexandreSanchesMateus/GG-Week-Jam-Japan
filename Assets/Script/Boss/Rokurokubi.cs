using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rokurokubi : BossBase
{
    public override void Start()
    {
        base.Start();
    }
    
    void Update()
    {
        int random = Random.Range(0, 3);

        switch (_stage)
        {
            case STAGE.NORMAL:
                break;
            case STAGE.HARD:
                break;
        }
    }

    public override void BossDeath()
    {
        Debug.Log("Hello Enfant");
    }
}
