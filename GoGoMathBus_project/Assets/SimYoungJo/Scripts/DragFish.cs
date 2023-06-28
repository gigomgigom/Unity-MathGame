using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragFish : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public CatFishManager CatFishManager;
    public static Vector2 startPos; //시작 위치

    //물고기 상태
    public enum FishState
    {
        None, RedDish, BlueDish
    }

    public FishState fishState = FishState.None;
    public List<Image> fishSprite = new List<Image>(); //물고기 이미지 
    private Vector2 oringPos; // 물고기 초기화 위치
    public List<Transform> fishParent = new List<Transform>(); //물고기의 부모그룹
    public float detectRange; //감지 범위
    
    void Start()
    {
        oringPos = GetComponent<RectTransform>().position; //물고기 위치 초기화
    }

    void Update()
    {

        int layerMask = 1 << 7; //7번 layer
        var hits = Physics.SphereCastAll(transform.position, detectRange, Vector3.up, 0, layerMask);

        if(hits.Length > 0) //접시가 감지되었을때
        {
            switch (hits[0].transform.name) //감지된 오브젝트의 이름
            {
                case "FishDetect_Red": //빨강접시의 감지기
                    {
                        fishState = FishState.RedDish; //물고기 상태를 빨강접시 상태로 변경
                        break;
                    }
                case "FishDetect_Blue": //파랑접시의 감지기
                    {
                        fishState = FishState.BlueDish; //물고기 상태를 파랑접시 상태로 변경
                        break;
                    }
            }
        }
        else //접시가 감지되지않았을경우
        {
            fishState = FishState.None; //물고기 상태를 None상태로 변경
        }

    }

    //포인터 클릭 다운
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        CatFishManager.FishDragStart(gameObject);
    }
    //포인터 클릭 업
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData) 
    {
        CatFishManager.FishDragEnd();

        switch (fishState)
        {
            case FishState.None:
                {
                    //transform.parent = fishParent[0];
                    fishSprite[0].enabled = true;
                    fishSprite[1].enabled = false;
                    fishSprite[2].enabled = false;
                    break;
                }
            case FishState.RedDish:
                {
                    //transform.parent = fishParent[1];
                    fishSprite[0].enabled = false;
                    fishSprite[1].enabled = true;
                    fishSprite[2].enabled = true;
                    break;
                }
            case FishState.BlueDish:
                {
                    //transform.parent = fishParent[2];
                    fishSprite[0].enabled = false;
                    fishSprite[1].enabled = true;
                    fishSprite[2].enabled = true;
                    break;
                }
        }
    }
    //드래그 시작
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        startPos = eventData.position; //시작위치 초기화
    }
    //드래그 중
    void IDragHandler.OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position; //현재위치
        transform.position = currentPos; //물고기의 위치를 현재 위치로 지정
    }
    //드래그 종료
    void IEndDragHandler.OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        //메인카메라의 화면에서 마우스 커서의 위치를 담는다.
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);        
    }

    public void FishReset()
    {
        GetComponent<RectTransform>().position = oringPos;
        fishSprite[0].enabled = true;
        fishSprite[1].enabled = false;
        fishSprite[2].enabled = false;
    }

}
