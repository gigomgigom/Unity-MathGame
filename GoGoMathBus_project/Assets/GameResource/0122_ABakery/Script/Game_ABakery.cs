using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_ABakery : MonoBehaviour
{
    [SerializeField] List<RectTransform> cuttingBoards;
    [SerializeField] RectTransform formula;
    [SerializeField] int maxEmptyCardPlaceCount = 2;
    [SerializeField] RectTransform dragObect;
    public RectTransform DragObject { get => dragObect; }
    List<RectTransform> emptyBreadPlaces;
    Animator animator;
    int stuckCount;
    int[] breadPlaceCountinCuttingBoards;

    void Start()
    {
        animator = GetComponent<Animator>();
        formula.gameObject.SetActive(false);
        emptyBreadPlaces = new List<RectTransform>();
        breadPlaceCountinCuttingBoards = new int[cuttingBoards.Count];

        int cuttingBoardIndex = 0;
        foreach (RectTransform cuttingBoard in cuttingBoards)
        {
            foreach (RectTransform breadPlace in cuttingBoard)
            {
                if (breadPlace.childCount == 0)
                    emptyBreadPlaces.Add(breadPlace);
                else
                {
                    ++breadPlaceCountinCuttingBoards[cuttingBoardIndex];
                    breadPlace.GetChild(0).GetComponent<BakeryBread>().Init(this);
                }
            }
            ++cuttingBoardIndex;
        }
    }

    public RectTransform GetNearEmptyBreadPlaces(Vector3 _position)
    {
        RectTransform nearEmptyBreadPlaces = new RectTransform();
        float minDis = 0;

        foreach(RectTransform emptyPlace in emptyBreadPlaces)
        {
            float offset = (emptyPlace.position - _position).sqrMagnitude;
            
            if(minDis == 0 || offset < minDis)
            {
                nearEmptyBreadPlaces = emptyPlace;
                minDis = offset;
            }
        }

        return nearEmptyBreadPlaces;
    }

    public void AddEmptyBreadPlace(RectTransform _place)
    {
        emptyBreadPlaces.Add(_place);
        int cuttingBoardIndex = cuttingBoards.FindIndex(rt => rt == _place.parent.GetComponent<RectTransform>());
        --breadPlaceCountinCuttingBoards[cuttingBoardIndex];
    }
    
    public void RemoveEmptyBreadPlace(RectTransform _place)
    {
        emptyBreadPlaces.Remove(_place);
        int cuttingBoardIndex = cuttingBoards.FindIndex(rt => rt == _place.parent.GetComponent<RectTransform>());
        ++breadPlaceCountinCuttingBoards[cuttingBoardIndex];

        bool isFull = false;
        for (int i = 0; i < breadPlaceCountinCuttingBoards.Length; i++)
        {
            if (breadPlaceCountinCuttingBoards[i] == cuttingBoards[i].childCount)
            { 
                isFull = true;
                break;
            }
        }

        if (isFull)
        {
            formula.gameObject.SetActive(true);
            animator.SetTrigger("formula");
        }
    }

    public void StuckCard()
    {
        ++stuckCount;
        if (stuckCount == maxEmptyCardPlaceCount)
            animator.SetTrigger("clear");
    }
}