using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class FishDetect : MonoBehaviour
{
    public float detectRange; //���� ����
    public int fishCount; //����� ����
    public List<GameObject> fishList = new List<GameObject>();

    public GameObject ropePrefab;
    public List<GameObject> ropeList = new List<GameObject>();

    bool ropeSign = true;

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
        /*
         int layerMask = 1 << 6; //6�� ���̾� ����

        //���Ǿ�ĳ��Ʈ�� �׸���.(�׸� ��ġ, ������[ũ��], ����, ����, ���̾��ũ)
        var hits = Physics.SphereCastAll(transform.position, detectRange, Vector3.up, 0, layerMask);

        //Debug.Log(hits.Length); //�����Ǵ� ������� ������ ���

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
        int layerMask = 1 << 6; //6�� ���̾�
        //���Ǿ�ĳ��Ʈ�� �׸���. (�׸� ��ġ, ũ��, ����, ����, ���̾��ũ)
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

    //���� ���� �Լ� (ù��° �����, �ι�° �����)
    public void CreateRope(int a, int b)
    {
        //���� ������ 0�� �� ��
        if (ropeList.Count == 0)
        {
            //���� ������Ʈ ����
            GameObject rope = Instantiate(ropePrefab);
            //���� �̸� ����
            rope.transform.name = "Rope" + a.ToString() + "_" + b.ToString();
            //������ ù��° ��ġ�� ù��° ����� �ŵ��� ��ġ�� ����
            rope.GetComponent<Rope>().lines.Add(fishList[a].GetComponent<DragFish>().fishSprite[2].transform);
            //������ �ι�° ��ġ�� �ι�° ����� �ŵ��� ��ġ�� ����
            rope.GetComponent<Rope>().lines.Add(fishList[b].GetComponent<DragFish>().fishSprite[2].transform);
            //�����迭�� ���� �߰�
            ropeList.Add(rope);
        }
        //���� ������ 1�� �̻��� ��
        else
        {
            //��ü ���� �߿���
            for (int i = 0; i < ropeList.Count; i++)
            {
                //������ �̸��� "Rope[a]_[b]�� ���� ��
                if (ropeList[i].transform.name != "Rope" + a.ToString() + "_" + b.ToString())
                {
                    //���� ���� true
                    if (ropeSign)
                    {
                        GameObject rope = Instantiate(ropePrefab);
                        rope.transform.name = "Rope" + a.ToString() + "_" + b.ToString();
                        rope.GetComponent<Rope>().lines.Add(fishList[a].GetComponent<DragFish>().fishSprite[2].transform);
                        rope.GetComponent<Rope>().lines.Add(fishList[b].GetComponent<DragFish>().fishSprite[2].transform);
                        ropeList.Add(rope);
                        StartCoroutine(RopeSignReset()); //���� ���� true ���� ȣ��
                        ropeSign = false; //���� ���� false
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

    //����� �߰�
    public void AddFish(GameObject fish)
    {
        fishList.Add(fish);
    }
    //����� ����
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
    //����� ���(����Ⱑ ���ø� ���������� ��)
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
    //���� ���� �Լ�
    public void ReduceRope()
    {
        for(int i = 0; i < ropeList.Count; i++)
        {
            Destroy(ropeList[i]);
        }
        ropeList.Clear();
    }
    //����� ���� ����
    public void FishReset()
    {
        fishList.Clear();
    }
    //���� �����
    public void RopeRebuild()
    {
        StartCoroutine(RopeRebuildStart());
    }
    //���� ����� ���� ����
    IEnumerator RopeRebuildStart()
    {
        yield return new WaitForSeconds(0.2f);
        //ropeList.Clear();

        for(int i = 0; i < ropeList.Count - 1; i++)
        {
            //���� ������Ʈ ����
            GameObject rope = Instantiate(ropePrefab);
            //���� �̸� ����
            rope.transform.name = "Rope" + i.ToString() + "_" + (i + 1).ToString();
            //������ ù��° ��ġ�� ù��° ������� �ŵ��� ��ġ�� ����
            rope.GetComponent<Rope>().lines.Add(fishList[i].GetComponent<DragFish>().fishSprite[2].transform);
            //������ �ι��� ��ġ�� �ι��� ������� �ŵ��� ��ġ�� ����
            rope.GetComponent<Rope>().lines.Add(fishList[i+1].GetComponent<DragFish>().fishSprite[2].transform);
            ropeList.Add(rope);
            //StartCoroutine(RopeSignReset()); //���� ���� true ���� ȣ��
            //ropeSign = false; //���� ���� false
        }
    }
}
