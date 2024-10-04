using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;// // �̱��� �������� MapManager �ν��Ͻ��� �������� ������ �� �ְ� ����

    [Space(10f)]
    [Header("Map Lock")]
    public Text lock_map_gold_text; // cafe �رݿ� �ʿ��� ��带 ��Ÿ��
    public GameObject[] maplock_group; // �� ��� �� ������Ʈ���� ��Ÿ��
    public GameObject[] maplock_button; // �� ��� �� ��ư�� ��Ÿ��
    int cafe; // �� ��� ���� ȣ���� ��Ÿ���� ����

    [Space(10f)]
    [Header("Cafe Management")]
    public GameObject cafe1; // ī�� 1ȣ���� �����ϴ� ������Ʈ
    public GameObject cafe2; // ī�� 2ȣ���� �����ϴ� ������Ʈ
    public bool cafe1_live; // ī�� 1ȣ��
    public bool cafe2_live; // ī�� 2ȣ��

    [Space(10f)]
    [Header("Camera")]
    public Vector3 targetPosition; // ī�� ��ġ�� ��Ÿ��

    private void Awake()
    {
        instance = this; // �̱��� ���� ����
        cafe1_live = false;
        cafe2_live = true;
    }

    public void Clickcafe1()
    {
        Checkcafe(0);

        //if (!cafe1_live)
        //{

        //    cafe1.gameObject.SetActive(true); //cafe1 ����
        //    cafe2.gameObject.SetActive(false); //cafe2 �ݱ�
        //    cafe2_live = false;
        //    cafe1_live = true;

        //    //ī�޶��� ��ġ�� 1ȣ������ �̵�
        //    targetPosition = new Vector3(20, 0, -10);
        //    Camera.main.transform.position = targetPosition;
        //}

    }

    public void Clickcafe2()
    {
        Checkcafe(1);
        //if (!cafe2_live)
        //{

        //    cafe2.gameObject.SetActive(true); //cafe2 ����
        //    cafe1.gameObject.SetActive(false); //cafe1 �ݱ�
        //    cafe1_live = false;
        //    cafe2_live = true;

        //    //ī�޶��� ��ġ�� 2ȣ������ �̵�
        //    targetPosition = new Vector3(0, 0, -10);
        //    Camera.main.transform.position = targetPosition;
        //}

    }

    public void Clickcafe3()
    {
        Checkcafe(2);
    }

    public void Clickcafe4()
    {
        Checkcafe(3);

    }

    public void Clickcafe5()
    {
        Checkcafe(4);
    }

    public void Checkcafe(int a)
    {
        GameManager.instance.lock_cafe_list = a; // ��ȣ���� �����ߴ��� gamemanager�� ����

        cafe = GameManager.instance.map_goldlist[a]; 
        lock_map_gold_text.text = cafe.ToString(); // �� �ʿ� �ʿ��� ��差�� text�� ��Ÿ���� ���� ����
    }
}
