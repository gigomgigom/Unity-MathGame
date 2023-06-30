using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectRectCookie : MonoBehaviour
{
    public CookiePuzzleManager PuzzleManager;
    public DragRectCookie rect1Cookie;
    public DragRectCookie rect2Cookie;
    public DragRectCookie rect3Cookie;
    public float detectRange;
    public List<GameObject> rectCookieList = new List<GameObject>();
    public Text resultText;
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
        if (rectCookieList.Count == 3)
        {
            ResultText();
            rectCookieList.Clear();
        }
    }
    public void AddCookie(GameObject cookie)
    {
        rectCookieList.Add(cookie);
    }
    public void ResultText()
    {
        resultText.gameObject.SetActive(true);
        resultText.text = "성공!";
        StartCoroutine(cookieReset());
        PuzzleManager.NextLevel();
    }
    IEnumerator cookieReset()
    {
        yield return new WaitForSeconds(2.0f);
        rect1Cookie.CookieReset();
        rect2Cookie.CookieReset();
        resultText.text = "";
        rectCookieList.Clear();
        resultText.gameObject.SetActive(false);
    }
}
