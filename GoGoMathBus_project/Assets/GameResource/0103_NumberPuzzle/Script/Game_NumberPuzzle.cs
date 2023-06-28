using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_NumberPuzzle : MonoBehaviour
{
    [SerializeField] CanvasGroup numberCardPosCanvasGroup;
    [SerializeField] RectTransform[] numberCardPosPool;
    [Space]
    [SerializeField] GameObject numberCardPrefabs;
    [SerializeField] RectTransform[] numberSetPlaces;
    [SerializeField] Sprite[] numberCardSprites;
    [Space]
    [SerializeField] float sortSpeed = 5f;
    public float CloseEnoughDistance = 0.1f;
    int currentCardIndex, stuckCardCount;

    void Start()
    {
        Restock(false);
    }

    void Restock(bool isAnim = true)
    {
        if (numberCardPosPool.Length > 0 && numberCardSprites.Length > currentCardIndex)
        {
            RectTransform parentRT = numberCardPosPool[numberCardPosPool.Length - 1];

            if (parentRT.childCount == 0)
            {
                GameObject numberCardInstant = Instantiate(numberCardPrefabs, parentRT);
                NumberPuzzleCard instantNumberCard = numberCardInstant.GetComponent<NumberPuzzleCard>();

                instantNumberCard.InitializeImage(this, numberCardSprites[currentCardIndex], numberSetPlaces[currentCardIndex], isAnim);
                StartCoroutine(SortCardPlaceCoroutine(isAnim));
            }
        }
    }

    public void CardStuck(bool isAnim = true)
    {
        stuckCardCount++;
        if (stuckCardCount == numberCardSprites.Length)//GameController.gameController.PhaseClear();
            GameClearController.gameClearController.UpdateClearCount();
        else
            StartCoroutine(SortCardPlaceCoroutine(isAnim));
    }

    IEnumerator SortCardPlaceCoroutine(bool isAnim)
    {
        WaitForEndOfFrame delay = new WaitForEndOfFrame();
        RectTransform emptyPos = null;
        List<Transform> moveChild = new List<Transform>();

        foreach (RectTransform rect in numberCardPosPool)
        {
            if (emptyPos == null)
            {
                if (rect.childCount == 0)
                    emptyPos = rect;
                    
            }
            else if (rect.childCount > 0)
                moveChild.Add(rect.GetChild(0)); 
        }
        if (emptyPos == null)
            yield break;

        if (isAnim)
        {
            numberCardPosCanvasGroup.blocksRaycasts = false;

            while (moveChild.Count > 0)
            {
                for (int i = moveChild.Count - 1; i >= 0; i--)
                {
                    Transform tf = moveChild[i];
                    Transform moveChildParent = i > 0 ? moveChild[i - 1].parent : emptyPos;

                    Vector2 targetPosition = new Vector2(tf.position.x, moveChildParent.position.y);
                    tf.position = Vector2.Lerp(tf.position, targetPosition, sortSpeed * Time.deltaTime);

                    if (Vector2.Distance(tf.position, targetPosition) < CloseEnoughDistance)
                    {
                        tf.position = targetPosition;
                        if (i == 0)
                            goto endLoop;
                    }
                }
                yield return delay;
            }
            endLoop: for (int i = moveChild.Count - 1; i >= 0; i--)
            {
                Transform moveChildParent = i > 0 ? moveChild[i - 1].parent : emptyPos;
                moveChild[i].SetParent(moveChildParent);
            }
            numberCardPosCanvasGroup.blocksRaycasts = true;
        }
        else
        {
            for (int i = moveChild.Count - 1; i >= 0; i--)
            {
                Transform moveChildParent = i > 0 ? moveChild[i - 1].parent : emptyPos;
                moveChild[i].SetParent(moveChildParent, isAnim);
            }
        }
        ++currentCardIndex;
        Restock(isAnim);
    }
}