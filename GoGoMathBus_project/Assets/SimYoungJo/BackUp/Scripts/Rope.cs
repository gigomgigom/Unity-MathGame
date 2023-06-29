using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public LineRenderer line; //라인렌더러
    public List<Transform> lines = new List<Transform>(); //라인 위치
    public float lineZ; //라인 Z포지션
    // Start is called before the first frame update
    void Start()
    {
        //라인의 첫번째 위치와 두번째 위치 트랜스폼이 있을 때
        if (lines[0] && lines[1])
        {
            //라인렌더러의 시작 위치 지정
            line.SetPosition(0, new Vector3(lines[0].position.x, lines[0].position.y, lineZ));

            //라인렌더러의 종료 위치 지정
            line.SetPosition(1, new Vector3(lines[1].position.x, lines[1].position.y, lineZ));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
