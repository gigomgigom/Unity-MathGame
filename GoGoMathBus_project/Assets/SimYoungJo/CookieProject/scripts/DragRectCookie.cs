using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragRectCookie : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public CookiePuzzleManager PuzzleManager;
    public DetectRectCookie detectRectCookie;
    public static Vector2 startPos; //���� ��ġ
    public enum CookieState //��Ű �浹ü�� �ƿ����� ���� ���� �÷��� [In : ����O, Out : ����X]
    {
        In, Out
    }
    [SerializeField]
    private List<Vector3> puzzleOriginalPositions;//test
    public List<GameObject> puzzlePieces;//test
    public CookieState cookieState = CookieState.Out;
    public float detectRange; //���� ����
    public GameObject outline; //Ʋ(Ʋ�ϵ����Ҷ� Ʋ �ƴ� ��)

    public float distance;
    public Transform target;
    public float nearDist;//�ٻ簪
    // Start is called before the first frame update
    void Start()
    {
        SaveRectOriginPos();
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 8; //6�� ���̾�
        var hits = Physics.SphereCastAll(transform.position, detectRange, Vector3.up, 0, layerMask);
        
        if(hits.Length > 0 )
        {
            switch (hits[0].transform.name)
            {
                case "Rect_Detect":
                    {
                        if (cookieState == CookieState.Out)
                        {
                            outline = hits[0].transform.gameObject;
                            outline.GetComponent<DetectRectCookie>().AddCookie(gameObject);
                            cookieState = CookieState.In;
                        }
                        break;
                    }
            }
        }
        else
        {
            cookieState = CookieState.Out;
        }
    }
    void SaveRectOriginPos() //test
    {
        puzzleOriginalPositions = new List<Vector3>();

        foreach (GameObject puzzlePiece in puzzlePieces)
        {
            Debug.Log(puzzlePiece.transform.name);
            puzzleOriginalPositions.Add(puzzlePiece.transform.position);
        }
    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        distance = Vector3.Distance(transform.position, target.position);
        if (distance <= nearDist)

            if (distance <= nearDist)
            {
                transform.position = target.position;
            }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position;
        transform.position = currentPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
    public void CookieReset()
    {
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            puzzlePieces[i].transform.position = puzzleOriginalPositions[i];
        }
    }
}
