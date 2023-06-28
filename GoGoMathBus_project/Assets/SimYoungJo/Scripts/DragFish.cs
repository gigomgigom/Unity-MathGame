using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragFish : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public CatFishManager CatFishManager;
    public static Vector2 startPos; //���� ��ġ

    //����� ����
    public enum FishState
    {
        None, RedDish, BlueDish
    }

    public FishState fishState = FishState.None;
    public List<Image> fishSprite = new List<Image>(); //����� �̹��� 
    private Vector2 oringPos; // ����� �ʱ�ȭ ��ġ
    public List<Transform> fishParent = new List<Transform>(); //������� �θ�׷�
    public float detectRange; //���� ����
    
    void Start()
    {
        oringPos = GetComponent<RectTransform>().position; //����� ��ġ �ʱ�ȭ
    }

    void Update()
    {

        int layerMask = 1 << 7; //7�� layer
        var hits = Physics.SphereCastAll(transform.position, detectRange, Vector3.up, 0, layerMask);

        if(hits.Length > 0) //���ð� �����Ǿ�����
        {
            switch (hits[0].transform.name) //������ ������Ʈ�� �̸�
            {
                case "FishDetect_Red": //���������� ������
                    {
                        fishState = FishState.RedDish; //����� ���¸� �������� ���·� ����
                        break;
                    }
                case "FishDetect_Blue": //�Ķ������� ������
                    {
                        fishState = FishState.BlueDish; //����� ���¸� �Ķ����� ���·� ����
                        break;
                    }
            }
        }
        else //���ð� ���������ʾ������
        {
            fishState = FishState.None; //����� ���¸� None���·� ����
        }

    }

    //������ Ŭ�� �ٿ�
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        CatFishManager.FishDragStart(gameObject);
    }
    //������ Ŭ�� ��
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
    //�巡�� ����
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        startPos = eventData.position; //������ġ �ʱ�ȭ
    }
    //�巡�� ��
    void IDragHandler.OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position; //������ġ
        transform.position = currentPos; //������� ��ġ�� ���� ��ġ�� ����
    }
    //�巡�� ����
    void IEndDragHandler.OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        //����ī�޶��� ȭ�鿡�� ���콺 Ŀ���� ��ġ�� ��´�.
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
