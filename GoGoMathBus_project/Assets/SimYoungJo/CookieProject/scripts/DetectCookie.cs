using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCookie : MonoBehaviour
{
    public float detectRange;
    public List<GameObject> cookieList = new List<GameObject>();
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
        
    }

    public void AddCookie(GameObject cookie)
    {
        cookieList.Add(cookie);
    }
}
