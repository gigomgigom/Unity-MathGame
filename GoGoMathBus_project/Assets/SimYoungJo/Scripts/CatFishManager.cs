using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //유니티 UI 패키지 사용 선언문

public class CatFishManager : MonoBehaviour
{

    public enum CatFishState
    {
        Standby, Play, Result
    }

    public CatFishState catFishState = CatFishState.Standby;
    //물고기리스트
    public List<GameObject> fishList = new List<GameObject>();
    //전체 물고기의 갯수
    private int totalFishCount = 10;
    //핑크접시, 파란접시에 필요한 물고기 갯수
    [SerializeField] //Private접근자를 가진 객체를 유니티에서 다룰수있게 하는 방법
    private int redFishCount;
    [SerializeField]
    private int blueFishCount;

    public FishDetect redDish; //핑크 접시
    public FishDetect blueDish; //파랑 접시

    //핑크, 파랑 접시 물고기 텍스트
    public Text redFishText;
    public Text blueFishText;

    //결과 텍스트 [성공!, 실패!]
    public Text resultText;

    // Start is called before the first frame update
    void Start()
    {
        RandomFishCount();
    }

    // Update is called once per frame
    void Update()
    {
        switch(catFishState)
        {
           /* case CatFishState.Play:
                {
                    //빨강접시와 파랑접시의 물고기의 합이 전체 물고기 수와 같을때
                    if (redDish.fishCount + blueDish.fishCount == totalFishCount)
                    {
                        //Debug.Log("접시에 다 담음 ㅋ");
                        FishResult();
                        catFishState = CatFishState.Result;
                    }
                    break;
                } */
        }
    }
    //물고기 결과
    void FishResult()
    {
        resultText.gameObject.SetActive(true);
        //빨강접시 물고기 수와 빨강접시 요구수가 같고, 파랑접시 물고기 수와 파랑접시 요구수가 같을 때
        if(redDish.fishCount == redFishCount && blueDish.fishCount == blueFishCount)
        {
            resultText.text = "성공! :D";
        }
        else
        {
            resultText.text = "실패... T.T";
        }
        //그냥 초기화 메서드를 호출을 하게 되면 결과를 확인하기도 전에 초기화가 되버리므로 시간 텀을 두고 메서드를 실행하도록 함.
        StartCoroutine(FishReset());
    }

    //게임 초기화
    IEnumerator FishReset()
    {
        yield return new WaitForSeconds(3.0f);
        RandomFishCount();
        for(int i = 0; i < fishList.Count; i++)
        {
            fishList[i].SendMessage("FishReset");
        }
        resultText.text = "";
        resultText.gameObject.SetActive(false);
        catFishState = CatFishState.Play;
    }

    //빨강접시와 파랑접시에 무작위 개수를 지정하는 함수
    void RandomFishCount()
    {
        //핑크 접시에 필요한 물고기의 갯수를 난수로 지정
        redFishCount = Random.Range(1, totalFishCount);
        //파랑 접시에 필요한 물고기 갯수 ( 전체개수 - redFishCount )
        blueFishCount = totalFishCount - redFishCount;

        redFishText.text = redFishCount.ToString();
        blueFishText.text = blueFishCount.ToString();
    }
    //물고기를 드래그 시작할 때 선택한 물고기만 움직이게 하는 함수
    public void FishDragStart(GameObject fish)
    {
        //전체 물고기 리스트 중에서
        for(int i = 0; i < fishList.Count; i++)
        {
            //선택한 물고기가 아니라면
            if (fishList[i] != fish)
            {
                //해당 물고기의 Image 컴포넌트를 사용 중지한다.
                fishList[i].GetComponent<Image>().enabled = false;
            }
        }
    }
    //물고기 드래그가 끝났을 때의 함수
    public void FishDragEnd()
    {
        //전체 물고기 리스트 중에서
        for(int i=0; i< fishList.Count;i++)
        {   
            //모든 물고기의 Image 컴포넌트를 사용한다.
            fishList[i].GetComponent<Image>().enabled = true;
        }

        redDish.FishInDish();
        blueDish.FishInDish();

        if (redDish.fishCount + blueDish.fishCount == totalFishCount)
        {
            FishResult();
        }
    }
}
