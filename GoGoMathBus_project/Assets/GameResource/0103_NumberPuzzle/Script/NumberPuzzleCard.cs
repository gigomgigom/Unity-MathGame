using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NumberPuzzleCard : DragAndStuck
{
    Game_NumberPuzzle numberPuzzle;

    public void InitializeImage(Game_NumberPuzzle _numberPuzzle, Sprite _sprite, RectTransform _dropPlace, bool _isAnim)
    {
        numberPuzzle = _numberPuzzle;
        dropPlace = _dropPlace;
        Image cardImage = GetComponent<Image>();
        cardImage.sprite = _sprite;
        cardImage.SetNativeSize();
    }
    
    protected override void PointerDown()
    {
    }
    protected override void PointerUp()
    {
        numberPuzzle.CardStuck();
    }
    protected override void PointerUpAndCancle()
    {
    }

    protected override void PointerStuck()
    {
    }

}