using UnityEngine;

public class Install : MonoBehaviour
{
    float pick_time; // ���콺 Ŭ�� �ð� ���� ����

    // ���콺 �巡�� �� ������ ������� ���� ó��
    void OnMouseDrag()
    {

        pick_time += Time.deltaTime; // ���콺 Ŭ�� �ð��� ����

        // Ŭ�� �ð��� �ʹ� ª���� �巡�׸� ó������ ����
        if (pick_time < 0.1f) return;

        // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ�Ͽ� ������ ��ġ�� �̵�
        Vector3 mouse_pos = Input.mousePosition;
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(mouse_pos.x, mouse_pos.y, mouse_pos.y));

        transform.position = point;
    }
}
