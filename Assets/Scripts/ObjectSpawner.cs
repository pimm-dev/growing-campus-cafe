using Unity.VisualScripting;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // ��ȯ�� ������
    public Vector2 spawnRangeX = new Vector2(-5f, 5f); // X�� ����
    public Vector2 spawnRangeY = new Vector2(-5f, 5f); // Y�� ����
    public float objectLifetime = 5f; // ��ü�� ������� �ð�
    public float spawnInterval = 5f; // ��ü ���� ���� (��)

    private int likeabilityScore = 100; // �ʱ� ȣ���� ����

    void Start()
    {
        // ���� �������� ��ü ���� ����
        InvokeRepeating("SpawnRandomObject", 0f, spawnInterval);
    }

    public void SpawnRandomObject()
    {
        // ���� ��ġ ����
        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        float randomY = Random.Range(spawnRangeY.x, spawnRangeY.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        // ��ü ���� �� �ı� �ݹ� ����
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        ClickableObject clickable = spawnedObject.AddComponent<ClickableObject>();
        clickable.SetLifetime(objectLifetime);
        clickable.OnDestroyed += HandleObjectDestroyed;
    }

    private void HandleObjectDestroyed(bool wasClicked)
    {
        if (!wasClicked)
        {
            // ��ü�� Ŭ������ �ʾ��� ��� ȣ���� ����
            likeabilityScore -= 10;
            Debug.Log("��ü�� Ŭ������ �ʾҽ��ϴ�! ���� ȣ����: " + likeabilityScore);
        }
    }
}
