using UnityEngine;

public class HeartEffect : MonoBehaviour
{
    public float fadeDuration = 0.5f;
    public float riseHeight = 0.5f;

    private SpriteRenderer spriteRenderer;
    private float timer;

    private Vector3 startPosition;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float progress = timer / fadeDuration;

        // ��Ʈ �ö󰡴� ����
        transform.position = startPosition + Vector3.up * riseHeight * progress;

        // ���İ� �����ؼ� ���̵�ƿ�
        Color color = spriteRenderer.color;
        color.a = Mathf.Lerp(1f, 0f, progress);
        spriteRenderer.color = color;

        // �� ������� ����
        if (progress >= 1f)
        {
            Destroy(gameObject);
        }
    }
}
