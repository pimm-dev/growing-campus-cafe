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
}
