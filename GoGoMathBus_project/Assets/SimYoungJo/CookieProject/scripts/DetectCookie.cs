using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCookie : MonoBehaviour
{
<<<<<<< Updated upstream
    public float detectRange;
    public List<GameObject> cookieList = new List<GameObject>();
=======
    public CookiePuzzleManager PuzzleManager;
    public DragCookie circLCookie;
    public DragCookie circRCookie;
    public float detectRange;
    public List<GameObject> circCookieList = new List<GameObject>();
    public Text resultText;
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        
=======
        if (circCookieList.Count == 2)
        {
            ResultText();
            circCookieList.Clear();
        }
>>>>>>> Stashed changes
    }

    public void AddCookie(GameObject cookie)
    {
        circCookieList.Add(cookie);
    }
<<<<<<< Updated upstream
=======
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
        circLCookie.CookieReset();
        circRCookie.CookieReset();
        resultText.text = "";
        circCookieList.Clear();
        resultText.gameObject.SetActive(false);
    }
>>>>>>> Stashed changes
}
