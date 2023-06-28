using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneBtn : MonoBehaviour
{
    [SerializeField] bool addToTree = true;

    public void LoadScene(string sceneName)
    {
        GameManager.Instance.LoadScene(sceneName, addToTree);
    }

    public void backScene(bool inGame)
    {
        GameManager.Instance.BackInScene(inGame);
    }

    public void RestartGameScene()
    {
        GameManager.Instance.RestartGameScene();
    }
}
