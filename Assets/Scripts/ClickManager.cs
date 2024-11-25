using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
                GameObject clickedObject = objectHit.collider.gameObject;

                if (clickedObject.CompareTag("Trash"))
                {
                    // ClickableObject ��ũ��Ʈ�� ��������
                    ClickableObject clickable = clickedObject.GetComponent<ClickableObject>();
                    
                    clickable.OnMouseDown(); // ClickableObject�� �Լ� ȣ��
                    
                }
                else if (clickedObject.CompareTag("Jelly"))
                {
                    Jelly jelly = clickedObject.GetComponent<Jelly>();
                    jelly.OnMouseDown();
                    Debug.Log($"Clicked object: {clickedObject.name}, Tag: {clickedObject.tag}, Layer: {clickedObject.layer}");

                }
                else if (clickedObject.CompareTag("Special"))
                {
                    SpecialCustomer specialCustomer = clickedObject.GetComponent<SpecialCustomer>();    
                    specialCustomer.OnMouseDown();
                }
               
            }
            // ����� Ŭ���Ǿ����� (������Ʈ�� ���� ��)
            else if (backgroundHit.collider != null)
            {
                
                // ��� Ŭ�� ó��
            }
        }
    }
}