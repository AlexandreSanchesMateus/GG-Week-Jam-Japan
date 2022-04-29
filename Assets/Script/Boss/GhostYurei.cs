using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostYurei : MonoBehaviour
{
    [SerializeField] private BoxCollider2D m_collider2D;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        m_collider2D.enabled = false;
        ActiveGhost(false);
    }

    public void ActiveGhost(bool active)
    {
        if (active)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            m_collider2D.enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            m_collider2D.enabled = false;
        }
    }

    public IEnumerator Disappear()
    {
        animator.SetTrigger("Disappear");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
