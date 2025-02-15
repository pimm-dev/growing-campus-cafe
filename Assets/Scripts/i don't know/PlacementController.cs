using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class PlacementController : MonoBehaviour
{
    public static PlacementController instance;

    public GameObject objectToSpawn; // ������ ������Ʈ
    public Button spawnButton; // ��ư

    private GameObject spawnedObject; // ��ȯ�� ������Ʈ
    public bool isFollowingMouse = false; // ������Ʈ�� ���콺�� ����ٴϴ��� ����

    // ======================= test ===========================
    private bool isShaking = false; // ��鸲 ����
    private Vector3 originalPosition; // ������Ʈ ���� ��ġ
    public float durability = 100f; // �ʱ� ������

    private void Awake()
    {
        instance = this; // �̱��� ���� ����
    }

    void Start()
    {

        // ��ư Ŭ�� �̺�Ʈ ���
        spawnButton.onClick.AddListener(SpawnObject);
    }

    void Update()
    {
        // ��ȯ�� ������Ʈ�� ���콺�� ����ٴϵ��� ��ġ ������Ʈ
        if (spawnedObject != null && isFollowingMouse)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // 2D������ z���� 0���� ����
            spawnedObject.transform.position = mousePosition;

            // ���콺 Ŭ�� �� ������Ʈ ����
            if (Input.GetMouseButtonDown(0)) // ���� ���콺 ��ư
            {
                isFollowingMouse = false; // ���콺 ����ٴϱ� ��Ȱ��ȭ
                Destroy(spawnedObject);
                spawnedObject = Instantiate(objectToSpawn);
                spawnedObject.transform.position = mousePosition;
                // ������Ʈ�� �ʱ� ��ġ ����
                originalPosition = mousePosition;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0)) // ���� ���콺 ��ư
            {
                // Ŭ�� �� ������ ����
                ReduceDurability(10f);
                Debug.Log("Object Broken!");
                // ��鸲 ȿ�� ����
                if (!isShaking)
                    StartCoroutine(Shake());
            }
        }
    }

    void SpawnObject()
    {
        Destroy(spawnedObject);
        // ���ο� ������Ʈ ��ȯ �� ���콺 ����ٴϵ��� ����
        spawnedObject = Instantiate(objectToSpawn);
        isFollowingMouse = true; // ���콺 ����ٴϱ� Ȱ��ȭ
    }

    void ReduceDurability(float amount)
    {
        durability -= amount;
        if (durability < 0)
            durability = 0;

        // �������� 0�̸� �ı� ó��
        if (durability == 0)
        {
            Destroy(spawnedObject, 0.5f); // 0.5�� �� ������Ʈ �ı�
        }
    }

    System.Collections.IEnumerator Shake()
    {
        isShaking = true;

        float elapsedTime = 0f;
        float duration = 0.3f; // ��鸲 ���� �ð�
        float magnitude = 0.1f; // ��鸲 ����

        while (elapsedTime < duration)
        {
            float offsetX = Random.Range(-magnitude, magnitude);
            float offsetY = Random.Range(-magnitude, magnitude);
            spawnedObject.transform.position = originalPosition + new Vector3(offsetX, offsetY, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spawnedObject.transform.position = originalPosition;
        isShaking = false;
    }
}
