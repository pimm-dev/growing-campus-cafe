using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class CollectedManager : MonoBehaviour
{
    public GameObject game_manager_obj; // GameManager ������Ʈ ����
    public GameManager game_manager; // GameManager ��ũ��Ʈ ����

    public bool[] collected_list;
    public Sprite[] customer_spritelist;
    public string[] customer_namelist;
    public int[] customer_favorability;
    public string[] collected_name;
    public Sprite[] collected_sprites;

    public Image lockGroupImage0;
    public Image lockGroupImage1;
    public Image lockGroupImage2;


    public Text pageText;
    int page;

    public Image SpecialSprite0;
    public Image SpecialSprite1;
    public Image SpecialSprite2;


    public Text SpecialName0;
    public Text SpecialName1;
    public Text SpecialName2;


    public Button SpecialButton0;
    public Button SpecialButton1;
    public Button SpecialButton2;


    public Image InformationImage;
    public Text InformationText;
    public Text InformationFavorability;

    public Image information_panel;
    Animator information_anim;

    public Text InformationMenuName;
    public Text InformationMenuDescription;
    public Image InformationMenuImage;

    public Image UnlockPanel;


    // Start is called before the first frame update
    void Awake()
    {
        // GameManager ������Ʈ�� ã��, �ش� ��ũ��Ʈ�� ����
        game_manager_obj = GameObject.Find("GameManager").gameObject;
        game_manager = game_manager_obj.GetComponent<GameManager>();

        collected_list = game_manager.collected_list;
        customer_spritelist = game_manager.special_customer_spritelist;
        customer_namelist = game_manager.special_customer_namelist;
        customer_favorability = game_manager.specialCustomerFavorability;
        collected_name = game_manager.collected_name;
        collected_sprites = game_manager.collected_sprites;

        // ù �������� �ʱ�ȭ
        page = 0;

        information_anim = information_panel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
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
    }*/

    public void UpdateLockGroupImages()
    {
        for (int i = 0; i < 3; i++)
        {
            int index = page * 3 + i;

            if (index < collected_name.Length) // collected_name �迭 ���� ������ Ȯ��
            {
                bool isUnlocked = !string.IsNullOrEmpty(collected_name[index]); // �̸��� �ִ��� Ȯ��
                switch (i)
                {
                    case 0:
                        lockGroupImage0.gameObject.SetActive(!isUnlocked);
                        break;
                    case 1:
                        lockGroupImage1.gameObject.SetActive(!isUnlocked);
                        break;
                    case 2:
                        lockGroupImage2.gameObject.SetActive(!isUnlocked);
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


    public void ChangePage()
    {
        int startIndex = page * 3;
        pageText.text = string.Format("#{0:00}", (page + 1));

        for (int i = 0; i < 3; i++)
        {
            if (startIndex + i < collected_name.Length)
            {
                Image[] specialSprites = { SpecialSprite0, SpecialSprite1, SpecialSprite2 };
                Text[] specialNames = { SpecialName0, SpecialName1, SpecialName2 };

                // �̸��� ��������Ʈ ������Ʈ
                specialSprites[i].sprite = collected_sprites[startIndex + i];
                specialNames[i].text = collected_name[startIndex + i];
            }
        }

        UpdateLockGroupImages();
    }

    public void ClickSpecialBitton0()
    {
        information_anim.SetTrigger("doShow");

        int index = page * 3;

        InformationImage.sprite = collected_sprites[index];
        InformationText.text = collected_name[index];
        InformationFavorability.text = "ȣ���� " + customer_favorability[index];

        InformationMenuName.text = game_manager.collected_menu_name[index];
        InformationMenuDescription.text = game_manager.collected_menu_description[index];
        InformationMenuImage.sprite = game_manager.collected_menu_image[index];

        game_manager.isInformationClick = true;

        if (game_manager.unlockMenu[index] == true)
        {
            UnlockPanel.gameObject.SetActive(false);
        }
        else
        {
            UnlockPanel.gameObject.SetActive(true);
        }

    }

    public void ClickSpecialBitton1()
    {
        information_anim.SetTrigger("doShow");

        int index = page * 3 + 1;

        InformationImage.sprite = collected_sprites[index];
        InformationText.text = collected_name[index];
        InformationFavorability.text = "ȣ���� " + customer_favorability[index];

        InformationMenuName.text = game_manager.collected_menu_name[index];
        InformationMenuDescription.text = game_manager.collected_menu_description[index];
        InformationMenuImage.sprite = game_manager.collected_menu_image[index];

        game_manager.isInformationClick = true;

        if (game_manager.unlockMenu[index] == true)
        {
            UnlockPanel.gameObject.SetActive(false);
        }
        else
        {
            UnlockPanel.gameObject.SetActive(true);
        }
    }

    public void ClickSpecialBitton2()
    {
        information_anim.SetTrigger("doShow");

        int index = page * 3 + 2;

        InformationImage.sprite = collected_sprites[index];
        InformationText.text = collected_name[index];
        InformationFavorability.text = "ȣ���� " + customer_favorability[index];

        InformationMenuName.text = game_manager.collected_menu_name[index];
        InformationMenuDescription.text = game_manager.collected_menu_description[index];
        InformationMenuImage.sprite = game_manager.collected_menu_image[index];

        game_manager.isInformationClick = true;

        if (game_manager.unlockMenu[index] == true)
        {
            UnlockPanel.gameObject.SetActive(false);
        }
        else
        {
            UnlockPanel.gameObject.SetActive(true);
        }
    }


    public void ExitButton()
    {
        information_anim.SetTrigger("doHide");

        game_manager.isInformationClick = false;

    }
}




