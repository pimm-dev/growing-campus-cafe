using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;// // �̱��� �������� MapManager �ν��Ͻ��� �������� ������ �� �ְ� ����

    public GameObject cafe1; // ī�� 1ȣ���� �����ϴ� ������Ʈ
    public GameObject cafe2; // ī�� 2ȣ���� �����ϴ� ������Ʈ
    public bool cafe1_live; // ī�� 1ȣ��
    public bool cafe2_live; // ī�� 2ȣ��

    public Text cafe_text;

    private void Awake()
    {
        instance = this; // �̱��� ���� ����
        cafe1_live = false;
        cafe2_live = true;
    }

    public void Clickcafe2()
    {
        if (cafe2_live)
        {
            cafe1.gameObject.SetActive(true); //cafe1 ����
            cafe2.gameObject.SetActive(false); //cafe2 �ݱ�
            cafe1_live = true;
            cafe2_live = false;
            cafe_text.text = "1ȣ��";
        }
        else
        {
            cafe2.gameObject.SetActive(true); //cafe2 ����
            cafe1.gameObject.SetActive(false); //cafe1 �ݱ�
            cafe1_live = false;
            cafe2_live = true;
            cafe_text.text = "2ȣ��";
        }
        
    }

}
