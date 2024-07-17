using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorZone : MonoBehaviour
{
    private Color color;
    public string type;

    public void Initialize()
    {
        color = type switch
        {
            "red" => Color.red,
            "blue" => Color.blue,
            "green" => Color.green,
            "purple" => Color.magenta,
            _ => Color.grey
        };

        Color aColor = new Color(color.r, color.g, color.b, 0.4f);
        GetComponent<SpriteRenderer>().color = aColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<playermove>()
                .ChangeColor(type);
        }
    }
}
