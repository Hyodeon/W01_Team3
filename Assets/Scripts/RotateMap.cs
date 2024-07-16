
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMap : MonoBehaviour
{
    public float rotationDuration = 1f; // ȸ���� �ɸ��� �ð� (��)
    public float rotationAngle = 90f; // ȸ�� ����
    private bool isRotating = false; // ȸ�� ������ ����

    void Update()
    {
        // ���콺 ��Ŭ�� ����
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
        float startRotation = transform.eulerAngles.z; // ���� ȸ�� ����
        float endRotation = startRotation + angle;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration);
            transform.rotation = Quaternion.Euler(0, 0, zRotation);
            yield return null;
        }

        // ��Ȯ�� ȸ�� ���� ����
        transform.rotation = Quaternion.Euler(0, 0, endRotation);
        isRotating = false;
    }
}
