using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCookie : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public CookiePuzzleManager PuzzleManager;
    public static Vector2 startPos; //시작 위치
    public enum CookieState //쿠키 충돌체가 아웃라인 진입 여부 플래그 [In : 진입O, Out : 진입X]
    {
        In, Out
    }
    public CookieState cookieState = CookieState.Out; 
    public Vector2 oringPos; //쿠키 초기화 위치
    public float detectRange; //감지 범위
    public GameObject outline; //틀(틀니딱딱할때 틀 아님 ㅎ)

    public float distance;
    public Transform target;
    public float nearDist;//근사값
    // Start is called before the first frame update
    void Start()
    {
        oringPos = GetComponent<RectTransform>().position; //쿠키 위치 초기화
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 8; //6번 레이어
        var hits = Physics.SphereCastAll(transform.position, detectRange, Vector3.up, 0, layerMask);

        if(hits.Length > 0)
        {
            switch(hits[0].transform.name)
            {
                case "Circle_Detect":
                    {
                        if (cookieState == CookieState.Out)
                        {
                            outline = hits[0].transform.gameObject;
                            outline.GetComponent<DetectCookie>().AddCookie(gameObject);
                            cookieState = CookieState.In;
                        }
                        break;
                    }
                case "Rect_Detect":
                    {
                        outline = hits[0].transform.gameObject;
                        outline.GetComponent<DetectCookie>().AddCookie(gameObject);
                        cookieState = CookieState.In;
                        break;
                    }                    
            }
        }        
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        distance = Vector3.Distance(transform.position, target.position);
        if(distance <= nearDist)
        {
            transform.position = target.position;
        }
    }
    //드래그 시작
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        startPos = eventData.position;
    }
    void IDragHandler.OnDrag(UnityEngine.EventSystems.PointerEventData eventData) 
    {
        Vector2 currentPos = eventData.position;
        transform.position = currentPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public void CookieReset()
    {
        GetComponent<RectTransform>().position = oringPos;
        cookieState = CookieState.Out;
    }
}
