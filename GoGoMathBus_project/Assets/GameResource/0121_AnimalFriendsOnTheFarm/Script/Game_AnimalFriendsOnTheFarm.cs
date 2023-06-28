using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game_AnimalFriendsOnTheFarm : MonoBehaviour
{
    [SerializeField] AnimalsBoundary animalsBoundaryA;
    [SerializeField] TextMeshProUGUI boundaryACountText;
    [SerializeField] Sprite boundaryASprite;
    [SerializeField] int boundaryACount;
    [Space]
    [SerializeField] AnimalsBoundary animalsBoundaryB;
    [SerializeField] TextMeshProUGUI boundaryBCountText;
    [SerializeField] Sprite boundaryBSprite;
    [SerializeField] int boundaryBCount;
    [Space]
    [SerializeField] TextMeshProUGUI animalsTotalCountText;
    [SerializeField] GameObject animalPrefab;
    Animator animator;
    int totalCount;


    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        
        for(int i = 0; i < boundaryACount; i++)
        {
            GameObject instAnimal = Instantiate(animalPrefab, animalsBoundaryA.transform);
            Image instAnimalImage = instAnimal.GetComponent<Image>();
            instAnimalImage.sprite = boundaryASprite;
            instAnimalImage.SetNativeSize();

        }
        boundaryACountText.text = boundaryACount.ToString();

        for (int i = 0; i < boundaryBCount; i++)
        {
            GameObject instAnimal = Instantiate(animalPrefab, animalsBoundaryB.transform);
            Image instAnimalImage = instAnimal.GetComponent<Image>();
            instAnimalImage.sprite = boundaryBSprite;
            instAnimalImage.SetNativeSize();
        }
        boundaryBCountText.text = boundaryBCount.ToString();

        totalCount = boundaryACount + boundaryBCount;
        animalsTotalCountText.text = totalCount.ToString();
    }

    public void CheckFenceChildCount(int fenceChildCount)
    {
        if (fenceChildCount == totalCount)
            animator.SetTrigger("exit");
    }

    void PhaseClear()
    {
        GameClearController.gameClearController.UpdateClearCount();
    }
}
