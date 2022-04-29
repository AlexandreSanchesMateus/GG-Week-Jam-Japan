using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; private set; }

    public List<Dialogue> _Sensei = new List<Dialogue>();

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI TMP;
    [SerializeField] private float range = 1.5f;

    private GameObject _closestSensei;
    private bool isOpen = false;

    void Start()
    {
        instance = this;
    }

    public void Update()
    {
        if (_Sensei.Count == 0)
            return;

        float distance = _Sensei[0].transform.position.x - player.transform.position.x;
        _closestSensei = _Sensei[0].gameObject;
        for (int i = 1; i < _Sensei.Count; i++)
        {
            float newDistance = _Sensei[i].transform.position.x - player.transform.position.x;
            if (newDistance < distance)
            {
                _closestSensei = _Sensei[1].gameObject;
                distance = newDistance;
            }
        }

        if (Mathf.Abs(distance) < range)
        {
            if (!isOpen)
            {
                animator.SetBool("Start", true);
                StopAllCoroutines();
                StartCoroutine(DisplayDialogue(_closestSensei, _closestSensei.GetComponent<Dialogue>()._dialogue));
                isOpen = true;
            }
        }
        else
            if (isOpen)
            {
                animator.SetBool("Start", false);
                isOpen = false;
            }
    }

    public IEnumerator DisplayDialogue(GameObject sensei, string dialogue, float transitionTime = 0.5f)
    {
        Vector2 cameraPos = Camera.main.WorldToScreenPoint(new Vector3(sensei.transform.position.x + 1, sensei.transform.position.y + 2));
        gameObject.transform.position = cameraPos;

        if (dialogue.Length != 0)
        {
            TMP.text = "";
            yield return new WaitForSeconds(transitionTime);

            foreach (char letter in dialogue.ToCharArray())
            {
                TMP.text += letter;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public void SetDialogue(GameObject himself, string newDialogue)
    {
        StopAllCoroutines();
        StartCoroutine(DisplayDialogue(himself, newDialogue, 0.1f));
    }
}
