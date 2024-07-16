using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandClock : MonoBehaviour
{
    public float timeLeft = 1f;

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime/20;
        transform.localScale = new Vector3(timeLeft, 1, 1);

        transform.localPosition = new Vector3(-(1 - timeLeft) / 2, 0, -2);
    }
}
