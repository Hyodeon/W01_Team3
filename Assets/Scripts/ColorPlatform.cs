using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPlatform : MonoBehaviour
{
    private Color color;
    public string type;

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
            if (pm.PlayerType.CompareTo(type) != 0 && pm.isPlaying)
            {
                pm.Die();
            }
        }
    }
}
