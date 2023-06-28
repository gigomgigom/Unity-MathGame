using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class WanderingAnimal : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Vector2 moveSpeedRange = new Vector2(0.5f, 2.0f);
    [SerializeField] Vector2 waitTimeRange = new Vector2(0.5f, 3.0f);
    [SerializeField] float jumpHeight = 2.0f;
    [SerializeField] float jumpFrequency = 2.0f;

    RectTransform parentRT;
    AnimalsBoundary animalsBoundary;
    Vector2 targetPosition, startPosition, minPosition, maxPosition, fenceMinPosition, fenceMaxPosition, originPos;
    float moveSpeed, waitTime, lerpTime;
    bool touched = false, done;

//    #if !UNITY_EDITOR
    Touch currentTouch;
//    #endif

    private void Start()
    {
        parentRT = transform.parent.GetComponent<RectTransform>();
        animalsBoundary = parentRT.GetComponent<AnimalsBoundary>();
        UpdateBoundary(out minPosition, out maxPosition, parentRT);
        UpdateBoundary(out fenceMinPosition, out fenceMaxPosition, animalsBoundary.FenceRT);
        transform.position = new Vector2(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y));
        StartCoroutine(MoveRandomly());
    }

    void Update()
    {
        if (touched)
        {
            //#if !UNITY_EDITOR
            //transform.position = currentTouch.position;
            //#else
            transform.position = Input.mousePosition;
            //#endif
        }
    }

    private IEnumerator MoveRandomly()
    {
        while (true)
        {
            if (!touched)
            {
                // Generate a random target position within the specified rectangular area
                targetPosition = new Vector2(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y));
                startPosition = transform.position;
                lerpTime = 0;

                // Generate random moveSpeed and waitTime values within the specified ranges
                moveSpeed = Random.Range(moveSpeedRange.x, moveSpeedRange.y);
                waitTime = Random.Range(waitTimeRange.x, waitTimeRange.y);

                // Move the object toward the target position using Lerp
                while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
                {
                    if (touched) break;

                    lerpTime += Time.deltaTime * moveSpeed;
                    float t = Mathf.Clamp01(lerpTime);

                    // Calculate the Y position with a continuous hopping effect
                    float jumpEffect = Mathf.Sin(t * Mathf.PI * jumpFrequency) * jumpHeight;

                    // Calculate the new position with the hopping effect
                    Vector2 newPos = Vector2.Lerp(startPosition, targetPosition, t);
                    newPos.y += jumpEffect;
                    transform.position = newPos;
                    yield return null;
                }

                // Wait for the specified time before generating a new target position
                if (!touched) yield return new WaitForSeconds(waitTime);
            }
            else
            {
                yield return null;
            }
        }
    }

    void UpdateBoundary(out Vector2 minPos, out Vector2 maxPos, RectTransform _rt)
    {
        minPos = new Vector2(_rt.position.x - (_rt.sizeDelta.x * 0.5f), _rt.position.y - ((_rt.sizeDelta.y * 0.5f)));
        maxPos = new Vector2(_rt.position.x + (_rt.sizeDelta.x * 0.5f), _rt.position.y + ((_rt.sizeDelta.y * 0.5f)));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!touched && !done)
        {
            touched = true;
            originPos = transform.position;

            GetComponent<Animator>().SetTrigger("drag");
            transform.SetParent(animalsBoundary.DragRT);

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
            Vector2 pos = transform.position;
            if(pos.x > fenceMinPosition.x && pos.y > fenceMinPosition.y && pos.x < fenceMaxPosition.x && pos.y < fenceMaxPosition.y)
            {
                UpdateBoundary(out minPosition, out maxPosition, animalsBoundary.FenceRT);
                transform.SetParent(animalsBoundary.FenceRT);
                animalsBoundary.CheckFenceChildCount();
                done = true;
            }
            else if(pos.x > minPosition.x && pos.y > minPosition.y && pos.x < maxPosition.x && pos.y < maxPosition.y)
            {
                transform.SetParent(parentRT);
            }
            else
            {
                transform.position = originPos;
                transform.SetParent(parentRT);
            }
            touched = false;
            GetComponent<Animator>().SetTrigger("idle");
        }
    }
}