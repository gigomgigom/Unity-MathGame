using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeryCard : DragAndStuck
{
    [SerializeField] Game_ABakery aBakery;

    protected override void PointerDown()
    {
    }

    protected override void PointerUp()
    {
        aBakery.StuckCard();
    }

    protected override void PointerUpAndCancle()
    {
    }

    protected override void PointerStuck()
    {
        
    }
}
