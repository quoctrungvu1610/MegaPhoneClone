using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int currentSceneIndex;
    private void Awake() 
    {
        Application.targetFrameRate = 60;
    }
    private void Update() {

    }
    public void RestartLevel()
    {
        int levelToLoadIndex = GetCurrentSceneIndex();
        Manager.Load("Level" + levelToLoadIndex.ToString());
    }

    public void LoadNextLevel()
    {
        int levelToLoadIndex = GetCurrentSceneIndex();
        if(levelToLoadIndex == 4)
        {
            levelToLoadIndex = 1;
        }
        else
        {
            levelToLoadIndex += 1;
        }
        Manager.Load("Level" + levelToLoadIndex.ToString());

    }
    int GetCurrentSceneIndex()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        return currentSceneIndex;
    }
    
}
