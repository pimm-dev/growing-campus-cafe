using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    void OnMouseDown()
    {
        // ������Ʈ�� Ŭ�� �� ��� ����
        Destroy(gameObject);
    }
}
