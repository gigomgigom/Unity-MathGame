using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeryBread : DragAndStuck
{
    Game_ABakery aBakery;
    Coroutine UpdateDropPlaceCoroutine;

    public void Init(Game_ABakery _aBakery)
    {
        aBakery = _aBakery;
        dragParent = aBakery.DragObject;
    }

    protected override void PointerDown()
    {
        aBakery.AddEmptyBreadPlace(originParent);
        UpdateDropPlaceCoroutine = StartCoroutine(UpdateDropPlace());
    }

    protected override void PointerUp()
    {
        StopCoroutine(UpdateDropPlaceCoroutine);
        aBakery.RemoveEmptyBreadPlace(dropPlace);
        originParent = dropPlace;
        originPos = transform.position;
        done = false;
    }

    protected override void PointerUpAndCancle()
    {
        StopCoroutine(UpdateDropPlaceCoroutine);
        aBakery.RemoveEmptyBreadPlace(originParent);
    }

    protected override void PointerStuck()
    {

    }

    IEnumerator UpdateDropPlace()
    {
        WaitForEndOfFrame delay = new WaitForEndOfFrame();

        while(true)
        {
            if(!stuck)
                dropPlace = aBakery.GetNearEmptyBreadPlaces(transform.position);
            yield return delay;
        }
    }
}
