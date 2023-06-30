using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookiePuzzleManager : MonoBehaviour
{
    public enum PuzzleState //게임 상태
    {
        Standby, Play, Result
    }
    public enum LevelState //레벨 상태
    {
        First, Second, Third, Fourth
    }    
    //게임, 레벨 상태 초기화
    public PuzzleState puzzleState = PuzzleState.Standby;
    public LevelState levelState = LevelState.First;
    public List<Transform> cookieParent = new List<Transform>();
    public List<Transform> outlineParent = new List<Transform>();

    [SerializeField]
    private int tripleDigit; //세자리수
    [SerializeField]
    private int doubleDigit; //두자리수
    [SerializeField]
    private int singleDigit; //한자리수
    [SerializeField]
    private int resultDigit; //결과값

    public Text rectTripleText; //사각형 세자리수 출력
    public Text rectDoubleText; //사각형 두자리수 출력
    public Text rectSingleText; //사각형 한자리수 출력
    public Text circDoubleText; //원형 두자리수 출력
    public Text circSingleText; //원형 한자리수 출력
    public Text totText; //합산 숫자 출력
    public Text resultText;//결과 문자열 출력
    // Start is called before the first frame update
    void Start()
    {
        NextLevel();
        puzzleState = PuzzleState.Play;
    }

    // Update is called once per frame
    void Update()
    {
       
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
            totText.text = resultDigit.ToString();
        } else
        {
            tripleDigit = Random.Range(1, 9);
            doubleDigit = Random.Range(1, 9);
            singleDigit = Random.Range(1, 9);
            resultDigit = tripleDigit * 100 + doubleDigit * 10 + singleDigit;
            rectTripleText.text = tripleDigit.ToString();
            rectDoubleText.text = doubleDigit.ToString();
            rectSingleText.text = singleDigit.ToString();
            totText.text = resultDigit.ToString();
        }
        

    }
    
    void NextLevel()
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
}
