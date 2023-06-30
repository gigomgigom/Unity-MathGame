using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectCookie : MonoBehaviour
{
    public CookiePuzzleManager PuzzleManager;
    public float detectRange;
    public List<GameObject> cookieList = new List<GameObject>();
    public Text resultText;
    //기즈모를 그리는 함수
    public void OnDrawGizmos()
    {
        //와이어 스피어를 그린다(그릴 위치, 반지름)
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cookieList.Count == 2)
        {
            ResultText();
            cookieList.Clear();
        }
    }
    public void AddCookie(GameObject cookie)
    {
        cookieList.Add(cookie);
    }
    public void ResultText()
    {
        resultText.gameObject.SetActive(true);
        resultText.text = "성공!";
        StartCoroutine(CookieReset());
    }
    IEnumerator CookieReset()
    {
        yield return new WaitForSeconds(3.0f);
        for (int i = 0; i < cookieList.Count; i++)
        {
            cookieList[i].SendMessage("CookieReset");
        }
        resultText.text = "";
        resultText.gameObject.SetActive(false);
    }
}
