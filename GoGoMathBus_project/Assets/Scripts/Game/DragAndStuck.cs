using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class DragAndStuck : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] protected float stickDistance = 50f;
    [SerializeField] protected RectTransform dragParent;
    [SerializeField] protected RectTransform dropPlace;
    [SerializeField] Vector2 dragScale = new Vector2(1.1f, 1.1f);
    [SerializeField] float lerpSpeed = 20f;
    protected RectTransform originParent;
    protected Vector2 originPos, originScale;
    protected bool touched, stuck, done;
    protected Coroutine dragCoroutine;
    protected Coroutine lerpScaleCoroutine;

//#if !UNITY_EDITOR
    //Touch currentTouch;
//#endif

    protected virtual void Awake()
    {
        originScale = transform.localScale;
        dragScale = originScale * dragScale;
        originParent = transform.parent.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!touched && !done)
        {
            touched = true;
            originPos = transform.position;
            if (dragParent != null)
                transform.SetParent(dragParent);
            PointerDown();
            dragCoroutine = StartCoroutine(OnDrag());
            OnLerpScale(dragScale);

//#if !UNITY_EDITOR
            //currentTouch = Input.touches[eventData.pointerId];
//#endif
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
//#if !UNITY_EDITOR
        //if (touched && currentTouch.fingerId == eventData.pointerId)
//#else
        if (touched)
//#endif
        {
            StopCoroutine(dragCoroutine);

            if (stuck && dropPlace != null)
            {
                transform.SetParent(dropPlace);
                done = true;
                PointerUp();
            }
            else
            {
                if (dragParent != null)
                    transform.SetParent(originParent);
                transform.position = originPos;
                PointerUpAndCancle();
            }
            OnLerpScale(originScale);
            touched = false;
        }
    }

    IEnumerator OnDrag()
    {
        WaitForEndOfFrame delay = new WaitForEndOfFrame();
        while (touched)
        {
            yield return delay;

            if (dropPlace != null)
            {
//#if !UNITY_EDITOR
//                float dis = Vector3.Distance(currentTouch.position, dropPlace.position);
//#else
                float dis = Vector3.Distance(Input.mousePosition, dropPlace.position);
//#endif

                if (dis < stickDistance)
                {
                    stuck = true;
                    transform.position = dropPlace.position;
                    PointerStuck();
                    continue;
                }
            }

            stuck = false;

//#if !UNITY_EDITOR
//            transform.position = currentTouch.position;
//#else
            transform.position = Input.mousePosition;
//#endif
        }
    }
    void OnLerpScale(Vector2 tartgetScale)
    {
        if (lerpScaleCoroutine != null)
            StopCoroutine(lerpScaleCoroutine);
        lerpScaleCoroutine = StartCoroutine(LerpScale(tartgetScale));
    }

    IEnumerator LerpScale(Vector2 targetScale)
    {
        WaitForEndOfFrame delay = new WaitForEndOfFrame();

        while (Vector2.Distance(transform.localScale, targetScale) > 0.0001f)
        {
            Vector2 lerpVec2 = Vector2.Lerp(transform.localScale, targetScale, lerpSpeed * Time.deltaTime);
            transform.localScale = lerpVec2;
            yield return delay;
        }
        transform.localScale = targetScale;
    }

    protected abstract void PointerDown();
    protected abstract void PointerUp();
    protected abstract void PointerStuck();
    protected abstract void PointerUpAndCancle();
}
