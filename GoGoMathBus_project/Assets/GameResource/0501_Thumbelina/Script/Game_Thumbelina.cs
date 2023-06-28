using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Thumbelina : MonoBehaviour
{
    [SerializeField] ThumbelinaNumberCard[] numberCards;
    [SerializeField] ThumbelinaNumberCard correctNumberCard;
    [SerializeField] RectTransform dragObjects;
    [SerializeField] RectTransform correctDropPlace;
    [SerializeField] RectTransform princess;
    [SerializeField] float clearAnimInvokeSec = 3f;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        princess.gameObject.SetActive(false);

        foreach(ThumbelinaNumberCard card in numberCards)
        {
            RectTransform _dropPlace = null;

            if (card == correctNumberCard)
                _dropPlace = correctDropPlace;

            card.Init(this, _dropPlace, dragObjects);
        }
    }

    public void PhaseClear()
    {
        princess.gameObject.SetActive(true);
        Invoke("PhaseClearAnim", clearAnimInvokeSec);
    }

    void PhaseClearAnim()
    {
        animator.SetTrigger("clear");
    }
}
