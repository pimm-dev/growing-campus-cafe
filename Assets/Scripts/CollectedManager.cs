using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectedManager : MonoBehaviour
{
    public GameObject game_manager_obj; // GameManager ������Ʈ ����
    public GameManager game_manager; // GameManager ��ũ��Ʈ ����

    public bool[] collected_list;
    public Sprite[] customer_spritelist;
    public string[] customer_namelist;

    public Image lockGroupImage0;
    public Image lockGroupImage1;
    public Image lockGroupImage2;
    public Image lockGroupImage3;
    public Image lockGroupImage4;

    public Text pageText;
    int page;

    public Image SpecialSprite0;
    public Image SpecialSprite1;
    public Image SpecialSprite2;
    public Image SpecialSprite3;
    public Image SpecialSprite4;

    public Text SpecialName0;
    public Text SpecialName1;
    public Text SpecialName2;
    public Text SpecialName3;
    public Text SpecialName4;



    // Start is called before the first frame update
    void Awake()
    {
        // GameManager ������Ʈ�� ã��, �ش� ��ũ��Ʈ�� ����
        game_manager_obj = GameObject.Find("GameManager").gameObject;
        game_manager = game_manager_obj.GetComponent<GameManager>();

        collected_list = game_manager.collected_list;
        customer_spritelist = game_manager.special_customer_spritelist;
        customer_namelist = game_manager.special_customer_namelist;

        // ù �������� �ʱ�ȭ
        page = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLockGroupImages()
    {
        // �� �������� �´� �ε����� ������� ��� ���¸� ������Ʈ
        for (int i = 0; i < 5; i++)
        {
            int index = page * 5 + i; // ���� �������� �´� �ε��� ���
            if (index < collected_list.Length) // �ε����� collected_list�� ������ �ʰ����� �ʵ��� Ȯ��
            {
                // ��� ���� ������Ʈ
                switch (i)
                {
                    case 0:
                        lockGroupImage0.gameObject.SetActive(!collected_list[index]);
                        break;
                    case 1:
                        lockGroupImage1.gameObject.SetActive(!collected_list[index]);
                        break;
                    case 2:
                        lockGroupImage2.gameObject.SetActive(!collected_list[index]);
                        break;
                    case 3:
                        lockGroupImage3.gameObject.SetActive(!collected_list[index]);
                        break;
                    case 4:
                        lockGroupImage4.gameObject.SetActive(!collected_list[index]);
                        break;
                }
            }
        }
    }


    public void UpdateCollectedList(int index, bool value)
    { 
       
            collected_list[index] = value; // collected_list �� ������Ʈ
            UpdateLockGroupImages(); // UI ���� ȣ��
        
    }

    // �������� �������� �̵�
    public void PageUp()
    {
        if (page >= 4) // �ִ� �������� ���� �ʵ��� ����
        {
           SoundManager.instance.PlaySound("Fail");
            return;
        }

        ++page;
        ChangePage(); // ������ ����
        SoundManager.instance.PlaySound("Button");
    }

    // �������� �������� �̵�
    public void PageDown()
    {
        if (page <= 0) // �ּ� �������� ���� �ʵ��� ����
        {
            SoundManager.instance.PlaySound("Fail");
            return;
        }

        --page;
        ChangePage(); // ������ ����
        SoundManager.instance.PlaySound("Button");
    }

    void ChangePage()
    {
        int startIndex = page * 5; // �� �������� ���� ���� �ε��� ���
        pageText.text = string.Format("#{0:00}", (page + 1)); // ������ ��ȣ ǥ��

        for (int i = 0; i < 5; i++)
        {
            if (startIndex + i < customer_spritelist.Length) // �ε��� ���� Ȯ��
            {
                // SpecialSprite �迭 ����
                Image[] specialSprites = { SpecialSprite0, SpecialSprite1, SpecialSprite2, SpecialSprite3, SpecialSprite4 };
                specialSprites[i].sprite = customer_spritelist[startIndex + i];
            }
        }


        for (int i = 0; i < 5; i++)
        {
            if (startIndex + i < customer_namelist.Length) // �ε��� ���� Ȯ��
            {
                Text[] specialNames = { SpecialName0, SpecialName1, SpecialName2, SpecialName3, SpecialName4 };
                specialNames[i].text = customer_namelist[startIndex + i].ToString(); // text �Ӽ��� �Ҵ�
            }
        }



        // �������� ����� �� ��� ���� ������Ʈ
        UpdateLockGroupImages(); // UI ���� ȣ��
    }


}
