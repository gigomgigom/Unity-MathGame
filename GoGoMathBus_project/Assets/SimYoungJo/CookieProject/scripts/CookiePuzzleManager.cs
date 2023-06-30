using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookiePuzzleManager : MonoBehaviour
{
    public enum PuzzleState //���� ����
    {
        Standby, Play, Result
    }
    public enum LevelState //���� ����
    {
        First, Second, Third, Fourth
    }
    //����, ���� ���� �ʱ�ȭ
    public PuzzleState puzzleState = PuzzleState.Standby;
    public LevelState levelState = LevelState.First;
    public List<Transform> cookieParent = new List<Transform>();
    public List<Transform> outlineParent = new List<Transform>();

    [SerializeField]
    private int tripleDigit; //���ڸ���
    [SerializeField]
    private int doubleDigit; //���ڸ���
    [SerializeField]
    private int singleDigit; //���ڸ���
    [SerializeField]
    private int resultDigit; //�����

    public Text rectTripleText; //�簢�� ���ڸ��� ���
    public Text rectDoubleText; //�簢�� ���ڸ��� ���
    public Text rectSingleText; //�簢�� ���ڸ��� ���
    public Text circDoubleText; //���� ���ڸ��� ���
    public Text circSingleText; //���� ���ڸ��� ���
    public Text resultText; //�ջ� ���� ���
    // Start is called before the first frame update
    void Start()
    {
        puzzleState = PuzzleState.Play;
        SetLevel();
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        
=======
        if (puzzleState == PuzzleState.Standby)
        {
            puzzleState = PuzzleState.Play;
            StartCoroutine(NextStage());
        }
    }
    IEnumerator NextStage()
    {
        yield return new WaitForSeconds(2.0f);
        SetLevel();
>>>>>>> Stashed changes
    }
    void RandomNumber()
    {
        if (levelState == LevelState.First || levelState == LevelState.Second)
        {
            doubleDigit = Random.Range(1, 9);
            singleDigit = Random.Range(1, 9);
            resultDigit = doubleDigit * 10 + singleDigit;
            circDoubleText.text = (doubleDigit * 10).ToString();
            circSingleText.text = singleDigit.ToString();
            resultText.text = resultDigit.ToString();
        } else
        {
            tripleDigit = Random.Range(1, 9);
            doubleDigit = Random.Range(1, 9);
            singleDigit = Random.Range(1, 9);
            resultDigit = tripleDigit * 100 + doubleDigit * 10 + singleDigit;
            rectTripleText.text = tripleDigit.ToString();
            rectDoubleText.text = doubleDigit.ToString();
            rectSingleText.text = singleDigit.ToString();
            resultText.text = resultDigit.ToString();
        }
        

    }
<<<<<<< Updated upstream
    void NextLevel()
=======
    
    void SetLevel()
>>>>>>> Stashed changes
    {
        switch (levelState)
        {
            case LevelState.First:
                RandomNumber();
                cookieParent[0].gameObject.SetActive(true);
                cookieParent[1].gameObject.SetActive(false);
                outlineParent[0].gameObject.SetActive(true);
                outlineParent[1].gameObject.SetActive(false);
                break;
            case LevelState.Second:
                RandomNumber();
                cookieParent[0].gameObject.SetActive(true);
                cookieParent[1].gameObject.SetActive(false);
                outlineParent[0].gameObject.SetActive(true);
                outlineParent[1].gameObject.SetActive(false);
                break;
            case LevelState.Third:
                RandomNumber();
                cookieParent[0].gameObject.SetActive(false);
                cookieParent[1].gameObject.SetActive(true);
                outlineParent[0].gameObject.SetActive(false);
                outlineParent[1].gameObject.SetActive(true);
                break;
            case LevelState.Fourth:
                RandomNumber();
                cookieParent[0].gameObject.SetActive(false);
                cookieParent[1].gameObject.SetActive(true);
                outlineParent[0].gameObject.SetActive(false);
                outlineParent[1].gameObject.SetActive(true);
                break;
        }
    }
    public void NextLevel()
    {
        switch (levelState)
        {
            case LevelState.First:
                levelState = LevelState.Second;
                break;
            case LevelState.Second:
                levelState = LevelState.Third;
                break;
            case LevelState.Third:
                levelState = LevelState.Fourth;
                break;
            case LevelState.Fourth:
                levelState = LevelState.First;
                break;
        }
        puzzleState = PuzzleState.Standby;
    }
}
