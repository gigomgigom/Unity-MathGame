using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_AnimalCloset : MonoBehaviour
{
    [SerializeField] AnimalCard[] animalCards;
    [SerializeField] RectTransform dragObject;
    Animator animator;
    int stuckCount;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        foreach (AnimalCard card in animalCards)
            card.Init(this, dragObject);
    }

    public void StuckCard()
    {
        ++stuckCount;
        if (stuckCount == animalCards.Length)
            animator.SetTrigger("clear");
    }
}
