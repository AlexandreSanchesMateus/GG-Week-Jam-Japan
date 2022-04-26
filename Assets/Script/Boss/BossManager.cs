using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager instance { get; private set; }

    private void Start()
    {
        instance = this;
    }

    public void DamageBoss(int damage)
    {

    }
}
