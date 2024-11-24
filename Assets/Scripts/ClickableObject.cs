using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public delegate void ObjectDestroyedHandler(bool wasClicked);
    public event ObjectDestroyedHandler OnDestroyed;

    private bool wasClicked = false;

    public void SetLifetime(float lifetime)
    {
        // ���� �ð��� ���� �� �ڵ����� �ı�
        Invoke("DestroySelf", lifetime);
    }

    public void OnMouseDown()
    {
        // ��ü�� Ŭ���Ǿ��� �� ó��
        wasClicked = true;
        DestroySelf();
    }

    private void DestroySelf()
    {
        // �ı��� �� Ŭ�� ���θ� �̺�Ʈ�� ����
        OnDestroyed?.Invoke(wasClicked);
        Destroy(gameObject);
    }
}
