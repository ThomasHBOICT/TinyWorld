using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartScene : MonoBehaviour
{
    public void RestartCurrentScene()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
