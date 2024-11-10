using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // ��ȯ�� ��ü ������
    public Vector2 spawnRangeX = new Vector2(-5f, 5f); // X�� ����
    public Vector2 spawnRangeY = new Vector2(-5f, 5f); // Y�� ����
    public float objectLifetime = 5f; // ��ü�� ������� �ð�
    public float spawnInterval = 5f; // ��ü ���� ���� (��)

    void Start()
    {
        // ���� �������� ��ü ���� ����
        InvokeRepeating("SpawnRandomObject", 0f, spawnInterval);
    }

    public void SpawnRandomObject()
    {
        // ���� ��ġ ���� (ī�޶� �� ����, �ʿ�� 3D�� Z�� �߰� ����)
        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        float randomY = Random.Range(spawnRangeY.x, spawnRangeY.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        // ��ü ����
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        // ���� �ð� �� �Ҹ�
        Destroy(spawnedObject, objectLifetime);
    }
}
