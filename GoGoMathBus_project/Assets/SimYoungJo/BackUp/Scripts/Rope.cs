using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public LineRenderer line; //���η�����
    public List<Transform> lines = new List<Transform>(); //���� ��ġ
    public float lineZ; //���� Z������
    // Start is called before the first frame update
    void Start()
    {
        //������ ù��° ��ġ�� �ι�° ��ġ Ʈ�������� ���� ��
        if (lines[0] && lines[1])
        {
            //���η������� ���� ��ġ ����
            line.SetPosition(0, new Vector3(lines[0].position.x, lines[0].position.y, lineZ));

            //���η������� ���� ��ġ ����
            line.SetPosition(1, new Vector3(lines[1].position.x, lines[1].position.y, lineZ));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
