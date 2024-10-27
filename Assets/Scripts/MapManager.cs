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
    public GameObject cafe3; // ī�� 3ȣ���� �����ϴ� ������Ʈ
    public GameObject cafe4; // ī�� 4ȣ���� �����ϴ� ������Ʈ
    public GameObject cafe5; // ī�� 5ȣ���� �����ϴ� ������Ʈ

    public bool cafe1_live; // ī�� 1ȣ��
    public bool cafe2_live; // ī�� 2ȣ��
    public bool cafe3_live; // ī�� 3ȣ��
    public bool cafe4_live; // ī�� 4ȣ��
    public bool cafe5_live; // ī�� 5ȣ��

    [Space(10f)]
    [Header("Camera")]
    public Vector3 targetPosition; // ī�� ��ġ�� ��Ÿ��

    private void Awake()
    {
        instance = this; // �̱��� ���� ����
    }

    public void Clickcafe1()
    {
        Checkcafe(0);
    }

    public void Clickcafe2()
    {
        Checkcafe(1);
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

        GameManager.instance.mapunlock_Button.gameObject.SetActive(true);

        cafe = GameManager.instance.map_goldlist[a]; 
        lock_map_gold_text.text = cafe.ToString(); // �� �ʿ� �ʿ��� ��差�� text�� ��Ÿ���� ���� ����
    }

    public void Movecafe1()
    {

        //ī�޶��� ��ġ�� 1ȣ������ �̵�
        targetPosition = new Vector3(0, 0, -10);
        Camera.main.transform.position = targetPosition;
        
    }

    public void Movecafe2()
    {
        //ī�޶��� ��ġ�� 2ȣ������ �̵�
        targetPosition = new Vector3(20, 0, -10);
        Camera.main.transform.position = targetPosition;
    }

    public void Movecafe3()
    {
        //ī�޶��� ��ġ�� 3ȣ������ �̵�
        targetPosition = new Vector3(40, 0, -10);
        Camera.main.transform.position = targetPosition;
    }

    public void Movecafe4()
    {
        //ī�޶��� ��ġ�� 4ȣ������ �̵�
        targetPosition = new Vector3(60, 0, -10);
        Camera.main.transform.position = targetPosition;
    }

    public void Movecafe5()
    {
        //ī�޶��� ��ġ�� 5ȣ������ �̵�
        targetPosition = new Vector3(80, 0, -10);
        Camera.main.transform.position = targetPosition;
    }

}
