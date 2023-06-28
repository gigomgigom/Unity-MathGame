using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] Image fadeImage;
    [SerializeField] float fadeDuration = 1f;
    Coroutine loadSceneCo;
    List<string> sceneTree;
    enum SceneLoadState
    {
        AddToTree,
        RenewTree,
        BackInTree,
        None
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            sceneTree = new List<string>();
            DontDestroyOnLoad(gameObject);
            sceneTree.Add(SceneManager.GetActiveScene().name);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName, bool inTree)
    {
        if(loadSceneCo == null)
        {
            SceneLoadState state = inTree ? SceneLoadState.AddToTree : SceneLoadState.None; //SceneLoadState.RenewTree ���� ���� �� ����.
            loadSceneCo = StartCoroutine(FadeAndLoadScene(sceneName, state));
        }
    }

    public void RestartGameScene()
    {
        if (loadSceneCo == null)
        {
            SceneLoadState state = SceneLoadState.None;
            loadSceneCo = StartCoroutine(FadeAndLoadScene(SceneManager.GetActiveScene().name, state));
        }
    }

    public void BackInScene(bool inGame) // ���� ������ ���ư� ���� ���� ����.
    {
        int i = inGame ? 1 : 2;

        if (loadSceneCo == null)
        {
             if(sceneTree.Count > i-1)
                loadSceneCo = StartCoroutine(FadeAndLoadScene(sceneTree[sceneTree.Count - i], SceneLoadState.BackInTree));
             else
                loadSceneCo = StartCoroutine(FadeAndLoadScene(sceneTree[0], SceneLoadState.RenewTree));
        }
            
    }

    IEnumerator FadeAndLoadScene(string sceneName, SceneLoadState state)
    {
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
        fadeImage.transform.parent.gameObject.SetActive(true);

        // ���̵� ��
        yield return StartCoroutine(Fade(1f));

        // �� �ε�
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // ���̵� �ƿ�
        yield return StartCoroutine(Fade(0f));

        //�� Ʈ�� ó��
        switch (state)
        {
            case SceneLoadState.AddToTree:
                sceneTree.Add(sceneName);
                break;
            case SceneLoadState.RenewTree:
                if (sceneTree.Exists(e => e == sceneName))
                {
                    int index = sceneTree.FindLastIndex(e => e == sceneName) + 1;

                    if (index < sceneTree.Count)
                        sceneTree.RemoveRange(index, sceneTree.Count - index);
                }
                else
                {
                    sceneTree.Clear();
                    sceneTree.Add(sceneName);
                }
                break;
            case SceneLoadState.BackInTree:
                sceneTree.RemoveAt(sceneTree.Count - 1);
                break;
            case SceneLoadState.None:
                break;
            default:
                break;
        }

        // �� �ε尡 �Ϸ�Ǹ� ���̵� ������Ʈ�� ��Ȱ��ȭ
        fadeImage.transform.parent.gameObject.SetActive(false);
        loadSceneCo = null;
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);

            Color newColor = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            fadeImage.color = newColor;

            yield return null;
        }
        Color finalColor = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);
        fadeImage.color = finalColor;
    }
}
