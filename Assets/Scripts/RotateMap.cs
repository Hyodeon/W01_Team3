
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMap : MonoBehaviour
{
    public float rotationDuration = 1f; // ȸ���� �ɸ��� �ð� (��)
    public float rotationAngle = 90f; // ȸ�� ����
    private bool isRotating = false; // ȸ�� ������ ����
    //private bool isAttached = false;

    // ���� ��Ȳ ������
    public bool IsRotating { get { return isRotating; } }

    public GameObject[] Indicators;
    public GameObject player;

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

        Vector3 playerStartPos = player.transform.position;

        while (t < duration)
        {
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration);
            transform.rotation = Quaternion.Euler(0, 0, zRotation);

            foreach (GameObject ind in Indicators)
            {
                ind.gameObject.SetActive(false);
            }

            yield return null;
        }

        foreach (GameObject ind in Indicators)
        {
            ind.gameObject.SetActive(true);
        }

        DetachPlayer();

        // ��Ȯ�� ȸ�� ���� ����
        transform.rotation = Quaternion.Euler(0, 0, endRotation);
        isRotating = false;
    }

    public void AttachPlayer()
    {
        player.transform.SetParent(this.transform);
    }

    public void DetachPlayer()
    {
        player.transform.SetParent(null);
    }
}
