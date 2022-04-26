using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailAttack : MonoBehaviour
{
    private bool isExtended;
    private SpriteRenderer _sp;

    [SerializeField] private Sprite _normalTailSp;
    [SerializeField] private Sprite _extendedTailSp;

    [SerializeField] private PolygonCollider2D _normalTailCollider;
    [SerializeField] private PolygonCollider2D _extendedTailCollider;

    void Start()
    {
        _sp = gameObject.GetComponent<SpriteRenderer>();
        _sp.sprite = _normalTailSp;
        _normalTailCollider.enabled = true;
        _extendedTailCollider.enabled = false;
        isExtended = false;
    }

    public void SmashTail()
    {

    }

    public void TransformTail(bool extend)
    {
        isExtended = extend;
        if (extend)
        {
            _sp.sprite = _extendedTailSp;

            _normalTailCollider.enabled = false;
            _extendedTailCollider.enabled = true;
        }
        else
        {
            _sp.sprite = _normalTailSp;
            _normalTailCollider.enabled = true;
            _extendedTailCollider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision)
        {
            if (isExtended)
            {
                if(collision.CompareTag("Player"))
                    Debug.Log("Player Damaged");
            }
            else
            {
                if(collision.CompareTag("Weapon"))
                    BossManager.instance.DamageBoss(5);
            }
        }
    }
}
