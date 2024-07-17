using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject swButton;

    GameObject[] laserWall;

    private void Start()
    {
        laserWall = GameObject.FindGameObjectsWithTag("LaserWall");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Dust"))
        {
            swButton.transform.localPosition = new Vector3(0, -0.3f, 0);
            foreach (GameObject wall in laserWall)
            {
                wall.SetActive(false);
            }
        }
    }
}
