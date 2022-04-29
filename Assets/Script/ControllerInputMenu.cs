using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputMenu : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            LevelLoader.instance.LoadNextLevel();
        if (Input.GetKeyDown(KeyCode.C))
            LevelLoader.instance.ExitGame();
    }
}
