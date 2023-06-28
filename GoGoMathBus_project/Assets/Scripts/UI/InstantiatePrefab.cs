using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
    [SerializeField] RectTransform targetParent;
    [SerializeField] GameObject targetPrefab;
    Coroutine tempCo;
        


    public void OnInstantiate()
    {
        if(tempCo == null)
            tempCo = StartCoroutine(InstantiateWithDelay());
    }

    IEnumerator InstantiateWithDelay()
    {
        Instantiate(targetPrefab, targetParent);
        yield return new WaitForSeconds(0.1f);
        tempCo = null;
    }
}
