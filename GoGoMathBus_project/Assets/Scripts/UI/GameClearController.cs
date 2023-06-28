using UnityEngine;
using TMPro;

public class GameClearController : MonoBehaviour
{

    static GameClearController _gameClearController = null;
    public static GameClearController gameClearController { get => _gameClearController; }

    [SerializeField] string gameName;
    [SerializeField] TextMeshProUGUI gameNameText;
    [SerializeField] RectTransform gameNameTextRT;
    [SerializeField] Vector2 padding;
    [Space]
    [SerializeField] RectTransform gamePhaseCountParent;
    [SerializeField] GameObject gamePhaseCountPrefab;
    [SerializeField] RectTransform gameClearWindow;
    int currentPhase;

    void Start()
    {
        _gameClearController = this;

        gameNameText.text = gameName;
        ResizeParent();
        gameClearWindow.gameObject.SetActive(false);
    }

    public void Init(int phaseCount)
    {
        for(int i = 0; i < phaseCount; i++)
        {
            Instantiate(gamePhaseCountPrefab, gamePhaseCountParent);
        }
    }

    public void UpdateClearCount()
    {
        gamePhaseCountParent.GetChild(currentPhase).GetComponent<Animator>().SetTrigger("clear");
        ++currentPhase;
    }

    public void GameClear()
    {
        gameClearWindow.gameObject.SetActive(true);
    }

    void ResizeParent()
    {
        // �ؽ�Ʈ�� ���̿� ���� ���� ���
        gameNameText.ForceMeshUpdate();
        Vector2 textSize = gameNameText.GetRenderedValues(false);

        // �θ� ������Ʈ�� ũ�⸦ �������� ����
        gameNameTextRT.sizeDelta = new Vector2(textSize.x + padding.x * 2, textSize.y + padding.y * 2);
    }

}
