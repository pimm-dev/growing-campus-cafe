using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public LayerMask backgroundLayer;  // ��� ���̾� (��: 8�� ���̾�)
    public LayerMask objectLayer;      // ������Ʈ ���̾� (��: 0�� ���̾�)

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� Ŭ��
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ���� ������Ʈ ���̾ ���� �� ���� Raycast�� ����Ͽ� �켱���� ó��
            RaycastHit2D backgroundHit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, backgroundLayer);
            RaycastHit2D objectHit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, objectLayer);

            // ������Ʈ�� Ŭ���Ǿ�����
            if (objectHit.collider != null)
            {
                Debug.Log($"{objectHit.collider.gameObject.name} Ŭ����!");
                // ������Ʈ Ŭ�� ó��
            }
            // ����� Ŭ���Ǿ����� (������Ʈ�� ���� ��)
            else if (backgroundHit.collider != null)
            {
                Debug.Log("��� Ŭ����!");
                // ��� Ŭ�� ó��
            }
        }
    }
}