using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager instance { get; private set; }

    [Header("Screen Parameter")]
    [SerializeField] private Transform _topLeft;
    [SerializeField] private Transform _bottomRight;

    [HideInInspector] public Vector3 _screenTopLeft { get; private set; }
    [HideInInspector] public Vector3 _screenBottomRight { get; private set; }

    private void Start()
    {
        instance = this;
        _screenTopLeft = Camera.main.ScreenToWorldPoint(_topLeft.position);
        _screenBottomRight = Camera.main.ScreenToWorldPoint(_bottomRight.position);
    }

    public void DamageBoss(int damage)
    {

    }
}
