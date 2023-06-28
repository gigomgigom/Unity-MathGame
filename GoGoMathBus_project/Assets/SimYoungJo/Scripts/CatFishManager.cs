using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //����Ƽ UI ��Ű�� ��� ����

public class CatFishManager : MonoBehaviour
{

    public enum CatFishState
    {
        Standby, Play, Result
    }

    public CatFishState catFishState = CatFishState.Standby;
    //����⸮��Ʈ
    public List<GameObject> fishList = new List<GameObject>();
    //��ü ������� ����
    private int totalFishCount = 10;
    //��ũ����, �Ķ����ÿ� �ʿ��� ����� ����
    [SerializeField] //Private�����ڸ� ���� ��ü�� ����Ƽ���� �ٷ���ְ� �ϴ� ���
    private int redFishCount;
    [SerializeField]
    private int blueFishCount;

    public FishDetect redDish; //��ũ ����
    public FishDetect blueDish; //�Ķ� ����

    //��ũ, �Ķ� ���� ����� �ؽ�Ʈ
    public Text redFishText;
    public Text blueFishText;

    //��� �ؽ�Ʈ [����!, ����!]
    public Text resultText;

    // Start is called before the first frame update
    void Start()
    {
        RandomFishCount();
    }

    // Update is called once per frame
    void Update()
    {
        switch(catFishState)
        {
           /* case CatFishState.Play:
                {
                    //�������ÿ� �Ķ������� ������� ���� ��ü ����� ���� ������
                    if (redDish.fishCount + blueDish.fishCount == totalFishCount)
                    {
                        //Debug.Log("���ÿ� �� ���� ��");
                        FishResult();
                        catFishState = CatFishState.Result;
                    }
                    break;
                } */
        }
    }
    //����� ���
    void FishResult()
    {
        resultText.gameObject.SetActive(true);
        //�������� ����� ���� �������� �䱸���� ����, �Ķ����� ����� ���� �Ķ����� �䱸���� ���� ��
        if(redDish.fishCount == redFishCount && blueDish.fishCount == blueFishCount)
        {
            resultText.text = "����! :D";
        }
        else
        {
            resultText.text = "����... T.T";
        }
        //�׳� �ʱ�ȭ �޼��带 ȣ���� �ϰ� �Ǹ� ����� Ȯ���ϱ⵵ ���� �ʱ�ȭ�� �ǹ����Ƿ� �ð� ���� �ΰ� �޼��带 �����ϵ��� ��.
        StartCoroutine(FishReset());
    }

    //���� �ʱ�ȭ
    IEnumerator FishReset()
    {
        yield return new WaitForSeconds(3.0f);
        RandomFishCount();
        for(int i = 0; i < fishList.Count; i++)
        {
            fishList[i].SendMessage("FishReset");
        }
        resultText.text = "";
        resultText.gameObject.SetActive(false);
        catFishState = CatFishState.Play;
    }

    //�������ÿ� �Ķ����ÿ� ������ ������ �����ϴ� �Լ�
    void RandomFishCount()
    {
        //��ũ ���ÿ� �ʿ��� ������� ������ ������ ����
        redFishCount = Random.Range(1, totalFishCount);
        //�Ķ� ���ÿ� �ʿ��� ����� ���� ( ��ü���� - redFishCount )
        blueFishCount = totalFishCount - redFishCount;

        redFishText.text = redFishCount.ToString();
        blueFishText.text = blueFishCount.ToString();
    }
    //����⸦ �巡�� ������ �� ������ ����⸸ �����̰� �ϴ� �Լ�
    public void FishDragStart(GameObject fish)
    {
        //��ü ����� ����Ʈ �߿���
        for(int i = 0; i < fishList.Count; i++)
        {
            //������ ����Ⱑ �ƴ϶��
            if (fishList[i] != fish)
            {
                //�ش� ������� Image ������Ʈ�� ��� �����Ѵ�.
                fishList[i].GetComponent<Image>().enabled = false;
            }
        }
    }
    //����� �巡�װ� ������ ���� �Լ�
    public void FishDragEnd()
    {
        //��ü ����� ����Ʈ �߿���
        for(int i=0; i< fishList.Count;i++)
        {   
            //��� ������� Image ������Ʈ�� ����Ѵ�.
            fishList[i].GetComponent<Image>().enabled = true;
        }

        redDish.FishInDish();
        blueDish.FishInDish();

        if (redDish.fishCount + blueDish.fishCount == totalFishCount)
        {
            FishResult();
        }
    }
}
