using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollRectEx : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ScrollRect targetScrollRect;
    Button targetButton;
    [SerializeField] int btnUnablePoint = 15;
    int dragPoint;

    private void Awake()
    {
        targetScrollRect = transform.parent.parent.parent.parent.GetComponent<ScrollRect>();
        targetButton = GetComponent<Button>();
    }

    public void OnBeginDrag(PointerEventData e)
    {
        targetScrollRect.OnBeginDrag(e);
        dragPoint = 0;
    }
    public void OnDrag(PointerEventData e)
    {
        targetScrollRect.OnDrag(e);
        ++dragPoint;
        if(dragPoint >= btnUnablePoint)
            targetButton.interactable = false;
    }
    public void OnEndDrag(PointerEventData e)
    {
        targetScrollRect.OnEndDrag(e);
        targetButton.interactable = true;
    }
}