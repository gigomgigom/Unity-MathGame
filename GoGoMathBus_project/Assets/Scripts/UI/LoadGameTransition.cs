using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadGameTransition : MonoBehaviour
{
    public static LoadGameTransition Instance { get; private set; }
    [SerializeField] Image thumbnailImage;
    [SerializeField] TextMeshProUGUI gameNameText;
    [SerializeField] Animator transitionAnimator;
    bool transitionIsDone;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        transitionAnimator.gameObject.SetActive(false);
    }

    public void LoadGameScene(string sceneName, Sprite thumbnail, string gameName)
    {
        thumbnailImage.sprite = thumbnail;
        gameNameText.text = gameName;
        StartCoroutine(LoadGameSceneWithTransition(sceneName));
    }

    IEnumerator LoadGameSceneWithTransition(string sceneName)
    {
        WaitForEndOfFrame delay = new WaitForEndOfFrame();
        DontDestroyOnLoad(gameObject);

        transitionAnimator.gameObject.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f && transitionIsDone)
            {
                asyncLoad.allowSceneActivation = true;
                transitionAnimator.SetTrigger("done");
                yield return delay;
            }
            else
            {
                yield return delay;
            }
        }
    }

    public void TransitionIsDone()
    {
        transitionIsDone = true;
    }

    public void TransitionIsEnd()
    {
        Destroy(gameObject);
    }
}
