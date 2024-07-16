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

        // 플레이어 죽기만 할 거임
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playermove pm = collision.gameObject.GetComponent<playermove>();
            if (pm.PlayerType.CompareTo(type) == 0)
            { // 둘의 색이 같은 경우
                SameColorEvents.Invoke();
            }
            else // 둘의 색이 다른 경우
            {
                Debug.Log("다르다고!");
                DiffColorEvents.Invoke();
                pm.Die();
            }
        }
    }
}
