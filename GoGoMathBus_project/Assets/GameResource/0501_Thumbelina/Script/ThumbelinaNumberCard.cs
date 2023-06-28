using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThumbelinaNumberCard : DragAndStuck
{
    Game_Thumbelina thumbelina;

    public void Init(Game_Thumbelina _Thumbelina, RectTransform _dropPlace, RectTransform _dragParent)
    {
        thumbelina = _Thumbelina;
        dropPlace = _dropPlace;
        dragParent = _dragParent;
    }

    protected override void PointerDown()
    {
    }

    protected override void PointerUp()
    {
        if (thumbelina != null)
            thumbelina.PhaseClear();
    }

    protected override void PointerUpAndCancle()
    {
    }

    protected override void PointerStuck()
    {
        
    }
}
