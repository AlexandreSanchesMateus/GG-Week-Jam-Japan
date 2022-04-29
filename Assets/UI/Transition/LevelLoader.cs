using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance { get; private set; }

    [SerializeField] private Animator animator;
    [SerializeField] private float transitionTime = 1.5f;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            ExitGame();
        if (Input.GetKeyDown(KeyCode.Space))
            LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadScene(int index)
    {
        StartCoroutine(LoadLevel(index));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator LoadLevel(int index)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(index);
    }
}
