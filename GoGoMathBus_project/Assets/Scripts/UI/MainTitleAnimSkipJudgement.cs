using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainTitleAnimSkipJudgement : MonoBehaviour
{
    public static bool isSkip = false;
    [SerializeField] Animator mainTitleAnimator;

    void Start()
    {
        if (isSkip)
            mainTitleAnimator.SetTrigger("skip");
    }

    public void UpdateSkipState(bool state)
    {
        isSkip = state;
    }
}
