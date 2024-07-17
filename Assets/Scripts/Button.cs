using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject swButton;
    public int id;

    public List<Laser> LaserList;

    private bool isUsed = false;

    private void Start()
    {
        
    }
    
    public void RegisterLaser(List<GameObject> list)
    {
        // TODO : Implement

        foreach (GameObject laser in list)
        {
            LaserList.Add(laser.GetComponent<Laser>());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Dust"))
            && !isUsed)
        { // 플에이어 접촉시
            isUsed = true;
            Debug.Log("작동!!");

            foreach (Laser laser in LaserList)
            {
                Debug.Log(laser.gameObject.name);
                laser.State = !laser.State;
                laser.ModifyLaser(laser.State);
            }
        }
    }
}
