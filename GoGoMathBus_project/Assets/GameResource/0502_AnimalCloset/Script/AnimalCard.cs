using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalCard : DragAndStuck
{
    Game_AnimalCloset animalCloset;

    public void Init(Game_AnimalCloset _animalCloset, RectTransform _dragParent)
    {
        animalCloset = _animalCloset;
        dragParent = _dragParent;
    }

    protected override void PointerDown()
    {
    }

    protected override void PointerUp()
    {
        animalCloset.StuckCard();
    }

    protected override void PointerUpAndCancle()
    {

    }

    protected override void PointerStuck()
    {
        
    }
}
