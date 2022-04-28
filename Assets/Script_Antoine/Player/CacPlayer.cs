using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacPlayer : PlayerBase
{
    [Header("Melee Settings")]
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _range;
    [SerializeField] private LayerMask _enemyLayer;

    public override void BasicAttack()
    {
        base.BasicAttack();

        // animation
        // animator.SetTrigger("Attack")
        
        // detect the enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _range, _enemyLayer);
        
        // damage them
        foreach(Collider2D Enemy in hitEnemies)
        {
            Enemy.GetComponent<BossBase>().TakeDamage(5);
        }
    }
}
