using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndicator : MonoBehaviour
{
    public GameObject player;
    public RotateMap rMap;

    void Update()
    {
        if (rMap.IsRotating) return;

        Vector3 playerPosition = player.transform.position;

        Vector3 rotatedPosition = RotatePointAroundOrigin2D(playerPosition, 90);

        // ȸ�� ����
        transform.position = rotatedPosition;
    }

    Vector3 RotatePointAroundOrigin2D(Vector3 point, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        float newX = point.x * cos + point.y * sin;
        float newY = -point.x * sin + point.y * cos;

        return new Vector3(newX, newY, 0);
    }

    public void DoMapRotate(float t)
    {
        float ratio = Mathf.Clamp01(t / 1.0f);

        float angle = 90f - 90f * ratio;
        Vector3 playerPosition = player.transform.position;

        transform.position = RotatePointAroundOrigin2D(playerPosition, angle);
    }
}
