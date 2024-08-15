using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public float minY = -2f; // 카메라의 최소 Y 좌표
    public float maxY = 4f;
    public float minX = -2f;
    public float maxX = 10f;
    void LateUpdate()
    {
        if (player != null)
        {
            // 플레이어의 Y 좌표를 기준으로 카메라의 Y 좌표 설정
            float targetY = player.position.y;
            float targetX = player.position.x;

            // Y 좌표가 minY보다 작아지지 않도록 제한
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

            // 현재 카메라의 위치를 가져옴
            Vector3 currentPosition = transform.position;

            currentPosition.y = targetY;
            currentPosition.x = targetX;

            // 카메라의 위치를 업데이트
            transform.position = currentPosition;
        }
    }
}
