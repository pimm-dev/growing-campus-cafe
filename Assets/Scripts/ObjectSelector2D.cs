using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectSelector2D : MonoBehaviour
{

    private void OnMouseDown()
    {
        PlacementController.instance.isFollowingMouse = true; // ���콺 ����ٴϱ� Ȱ��ȭ
        Debug.Log("�۵��ϳ�?");
    }
}
