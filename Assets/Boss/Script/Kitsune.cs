using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitsune : BossBase
{
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

        switch (_stage)
        {
            case STAGE.NORMAL:
                break;
            case STAGE.HARD:
                break;
            case STAGE.IMPOSSIBLE:
                break;
        }
    }



    public override void BossDeath()
    {
        Debug.Log("Hello Enfant");
    }
}
