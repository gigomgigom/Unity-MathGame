using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class correct : MonoBehaviour
{

    public GameObject now;
    public GameObject next;


    void Start()
    {
        StartCoroutine(DelayTime(3));

    }
    IEnumerator DelayTime(float time)
    {
        yield return new WaitForSeconds(time);

        now.SetActive(false);
        next.SetActive(true);
    }

}
