using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public float minY = -2f; // ī�޶��� �ּ� Y ��ǥ
    public float maxY = 4f;
    public float minX = -2f;
    public float maxX = 10f;
    void LateUpdate()
    {
        if (player != null)
        {
            // �÷��̾��� Y ��ǥ�� �������� ī�޶��� Y ��ǥ ����
            float targetY = player.position.y;
            float targetX = player.position.x;

            // Y ��ǥ�� minY���� �۾����� �ʵ��� ����
            if (targetY < minY)
            {
                targetY = minY;
            }
            if (targetY > maxY)
            {
                targetY = maxY;
            }
            if (targetX < minX)
            {
                targetX = minX;
            }
            if (targetX > maxX)
            {
                targetX = maxX;
            }

            // ���� ī�޶��� ��ġ�� ������
            Vector3 currentPosition = transform.position;

            currentPosition.y = targetY;
            currentPosition.x = targetX;

            // ī�޶��� ��ġ�� ������Ʈ
            transform.position = currentPosition;
        }
    }
}
