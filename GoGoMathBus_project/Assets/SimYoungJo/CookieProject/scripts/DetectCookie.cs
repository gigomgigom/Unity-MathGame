using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCookie : MonoBehaviour
{
    public float detectRange;
    public List<GameObject> cookieList = new List<GameObject>();
    //����� �׸��� �Լ�
    public void OnDrawGizmos()
    {
        //���̾� ���Ǿ �׸���(�׸� ��ġ, ������)
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
