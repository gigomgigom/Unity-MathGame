using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameSceneBtn : MonoBehaviour
{
    [SerializeField] Sprite thumbnail;
    [SerializeField] string gameName;
    public void LoadScene(string sceneName)
    {
        if(LoadGameTransition.Instance != null)
            LoadGameTransition.Instance.LoadGameScene(sceneName, thumbnail, gameName);
    }
}
