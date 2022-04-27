using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostYurei : MonoBehaviour
{
    public void ActiveGhost(bool active)
    {
        if (active)
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        else
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void Disappear()
    {
        //animation de disparition
        Destroy(gameObject);
    }
}
