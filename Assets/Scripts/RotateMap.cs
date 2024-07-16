
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMap : MonoBehaviour
{
    public float rotationDuration = 1f; // 회전에 걸리는 시간 (초)
    public float rotationAngle = 90f; // 회전 각도
    private bool isRotating = false; // 회전 중인지 여부

    void Update()
    {
        // 마우스 우클릭 감지
        if (Input.GetMouseButtonDown(0) && !isRotating)
        {
            StartCoroutine(RotateOverTime(rotationAngle, rotationDuration));
        }
        if (Input.GetMouseButtonDown(1) && !isRotating)
        {
            StartCoroutine(RotateOverTime(-rotationAngle, rotationDuration));
        }
    }

    private IEnumerator RotateOverTime(float angle, float duration)
    {
        isRotating = true;
        float startRotation = transform.eulerAngles.z; // 현재 회전 각도
        float endRotation = startRotation + angle;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration);
            transform.rotation = Quaternion.Euler(0, 0, zRotation);
            yield return null;
        }

        // 정확한 회전 각도 설정
        transform.rotation = Quaternion.Euler(0, 0, endRotation);
        isRotating = false;
    }
}
