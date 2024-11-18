using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public GameObject prefabToPlace;
    private GameObject previewObject;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point;

            if (previewObject == null)
            {
                previewObject = Instantiate(prefabToPlace);
                // �����ϰ� ���� �̸������ ���
                // Material�� �����ϴ� ���� �߰� ����
            }

            previewObject.transform.position = targetPosition;

            if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ��
            {
                Instantiate(prefabToPlace, targetPosition, Quaternion.identity);
            }
        }
    }
}
