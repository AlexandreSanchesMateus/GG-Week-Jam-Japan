using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour
{
    private Animator animator;
    private RectTransform m_transform;

    private Vector2 m_centerPos;
    private Vector2 m_minX;
    private Vector2 m_maxX;
    private Vector2 m_minY;
    private Vector2 m_maxY;

    private void Start()
    {
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        m_transform = gameObject.GetComponent<RectTransform>();

        m_centerPos = Camera.main.ScreenToWorldPoint(m_transform.transform.position);
        m_minX = Camera.main.ScreenToWorldPoint(new Vector3(m_transform.transform.position.x - m_transform.rect.width/2, 0, 0));
        Debug.Log("Center : " + m_centerPos + "    m_minX : " + m_minX);
        //m_maxX;
        //m_minY;
        //m_maxY;
    }

    private void Update()
    {
        /*Vector3 MouseCoordinate = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);

        if ((MouseCoordinate.x >= m_transform.rect.max.x && MouseCoordinate.x <= m_transform.rect.min.x) && (MouseCoordinate.y >= m_transform.rect.max.y && MouseCoordinate.y <= m_transform.rect.min.y))
            Debug.Log("Dedant !");
        else
            Debug.Log("Dehors !");*/
    }

    private void OnMouseOver()
    {
        Debug.Log("Debant !");
    }
}
