using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorPlatform : MonoBehaviour
{
    private Color color;
    public string type;

    public UnityEvent SameColorEvents;
    public UnityEvent DiffColorEvents;

    private void Start()
    {
        color = type switch
        {
            "red" => Color.red,
            "blue" => Color.blue,
            "green" => Color.green,
            "purple" => Color.magenta,
            _ => Color.grey
        };

        GetComponent<SpriteRenderer>().color = color;

        // �÷��̾� �ױ⸸ �� ����
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playermove pm = collision.gameObject.GetComponent<playermove>();
            if (pm.PlayerType.CompareTo(type) == 0)
            { // ���� ���� ���� ���
                SameColorEvents.Invoke();
            }
            else // ���� ���� �ٸ� ���
            {
                Debug.Log("�ٸ��ٰ�!");
                DiffColorEvents.Invoke();
                pm.Die();
            }
        }
    }
}
