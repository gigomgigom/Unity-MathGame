using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class FishDetect : MonoBehaviour
{
    public float detectRange; //감지 범위
    public int fishCount; //물고기 갯수
    public List<GameObject> fishList = new List<GameObject>();

    public GameObject ropePrefab;
    public List<GameObject> ropeList = new List<GameObject>();

    bool ropeSign = true;

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
        /*
         int layerMask = 1 << 6; //6번 레이어 선정

        //스피어캐스트를 그린다.(그릴 위치, 반지름[크기], 방향, 길이, 레이어마스크)
        var hits = Physics.SphereCastAll(transform.position, detectRange, Vector3.up, 0, layerMask);

        //Debug.Log(hits.Length); //감지되는 물고기의 개수를 출력

        if (fishList.Contains(null))
        {
            fishList.Clear();
        }
        fishCount = hits.Length;
        if (fishList.Count < hits.Length)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (!fishList.Contains(hits[i].transform.gameObject))
                {
                    fishList.Add(hits[i].transform.gameObject);
                }
            }
        }
        if(fishList.Count != hits.Length)
        {
            fishList.Clear();
            ReduceRope();
            for(int i = 0; i<hits.Length; i++)
            {
                fishList.Add(hits[i].transform.gameObject);
            }
        }

        */
    }
    public void FishInDish()
    {
        int layerMask = 1 << 6; //6번 레이어
        //스피어캐스트를 그린다. (그릴 위치, 크기, 방향, 길이, 레이어마스크)
        var hits = Physics.SphereCastAll(transform.position, detectRange, Vector3.up, 0, layerMask);

        if (ropeList.Count < fishList.Count - 1)
        {
            CreateRope(fishList.Count - 2, fishList.Count - 1);
        }
        else if (ropeList.Count > fishList.Count - 1)
        {
            ReduceRope();
        }
    }

    //로프 생성 함수 (첫번째 물고기, 두번째 물고기)
    public void CreateRope(int a, int b)
    {
        //로프 갯수가 0개 일 때
        if (ropeList.Count == 0)
        {
            //로프 오브젝트 생성
            GameObject rope = Instantiate(ropePrefab);
            //로프 이름 지정
            rope.transform.name = "Rope" + a.ToString() + "_" + b.ToString();
            //로프의 첫번째 위치를 첫번째 물고기 매듭의 위치로 지정
            rope.GetComponent<Rope>().lines.Add(fishList[a].GetComponent<DragFish>().fishSprite[2].transform);
            //로프의 두번째 위치를 두번째 물고기 매듭의 위치로 지정
            rope.GetComponent<Rope>().lines.Add(fishList[b].GetComponent<DragFish>().fishSprite[2].transform);
            //로프배열에 로프 추가
            ropeList.Add(rope);
        }
        //로프 갯수가 1개 이상일 때
        else
        {
            //전체 로프 중에서
            for (int i = 0; i < ropeList.Count; i++)
            {
                //로프의 이름이 "Rope[a]_[b]가 없을 때
                if (ropeList[i].transform.name != "Rope" + a.ToString() + "_" + b.ToString())
                {
                    //로프 사인 true
                    if (ropeSign)
                    {
                        GameObject rope = Instantiate(ropePrefab);
                        rope.transform.name = "Rope" + a.ToString() + "_" + b.ToString();
                        rope.GetComponent<Rope>().lines.Add(fishList[a].GetComponent<DragFish>().fishSprite[2].transform);
                        rope.GetComponent<Rope>().lines.Add(fishList[b].GetComponent<DragFish>().fishSprite[2].transform);
                        ropeList.Add(rope);
                        StartCoroutine(RopeSignReset()); //로프 사인 true 지연 호출
                        ropeSign = false; //로프 사인 false
                    }
                }
            }
        }
    }

    IEnumerator RopeSignReset()
    {
        yield return new WaitForSeconds(0.2f);
        ropeSign = true;
    }

    //물고기 추가
    public void AddFish(GameObject fish)
    {
        fishList.Add(fish);
    }
    //물고기 감소
    public void ReduceFish(GameObject fish)
    {
        ReduceRope();
        for(int i = 0; i < fishList.Count;i++)
        {
            if (fishList[i]==fish)
            {
                fishList.RemoveAt(i);
            }
        }
    }
    //물고기 취소(물고기가 접시를 빠져나갔을 때)
    public void CancelFish(GameObject fish)
    {
        for (int i = 0; i < fishList.Count; i++)
        {
            if (fishList[i]==fish)
            {
                fishList.RemoveAt(i);
            }
        }
    }
    //로프 제거 함수
    public void ReduceRope()
    {
        for(int i = 0; i < ropeList.Count; i++)
        {
            Destroy(ropeList[i]);
        }
        ropeList.Clear();
    }
    //물고기 전부 제거
    public void FishReset()
    {
        fishList.Clear();
    }
    //로프 재생성
    public void RopeRebuild()
    {
        StartCoroutine(RopeRebuildStart());
    }
    //로프 재생성 지연 시작
    IEnumerator RopeRebuildStart()
    {
        yield return new WaitForSeconds(0.2f);
        //ropeList.Clear();

        for(int i = 0; i < ropeList.Count - 1; i++)
        {
            //로프 오브젝트 생성
            GameObject rope = Instantiate(ropePrefab);
            //로프 이름 지정
            rope.transform.name = "Rope" + i.ToString() + "_" + (i + 1).ToString();
            //로프의 첫번째 위치를 첫번째 물고기의 매듭의 위치로 지정
            rope.GetComponent<Rope>().lines.Add(fishList[i].GetComponent<DragFish>().fishSprite[2].transform);
            //로프의 두번재 위치를 두번재 물고기의 매듭의 위치로 지정
            rope.GetComponent<Rope>().lines.Add(fishList[i+1].GetComponent<DragFish>().fishSprite[2].transform);
            ropeList.Add(rope);
            //StartCoroutine(RopeSignReset()); //로프 사인 true 지연 호출
            //ropeSign = false; //로프 사인 false
        }
    }
}
