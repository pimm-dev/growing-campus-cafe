using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectSelector2D : MonoBehaviour
{
    public Slider durabilityBar; // ������ ��
    public float durability = 100f; // �ʱ� ������
    private bool isShaking = false; // ��鸲 ����
    private Vector3 originalPosition; // ������Ʈ ���� ��ġ

    void Start()
    {
        // ������Ʈ�� �ʱ� ��ġ ����
        originalPosition = transform.position;
        // ������ �� �ʱ�ȭ
        if (durabilityBar != null)
            durabilityBar.value = durability / 100f;
    }

    void OnMouseDown()
    {
        // Ŭ�� �� ������ ����
        ReduceDurability(10f);
        // ��鸲 ȿ�� ����
        if (!isShaking)
            StartCoroutine(Shake());
    }

    void ReduceDurability(float amount)
    {
        durability -= amount;
        if (durability < 0)
            durability = 0;

        // ������ �� ������Ʈ
        if (durabilityBar != null)
            durabilityBar.value = durability / 100f;

        // �������� 0�̸� �ı� ó��
        if (durability == 0)
        {
            Debug.Log("Object Broken!");
            Destroy(gameObject, 0.5f); // 0.5�� �� ������Ʈ �ı�
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
            transform.position = originalPosition + new Vector3(offsetX, offsetY, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
        isShaking = false;
    }
}
