using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandClock : MonoBehaviour
{
    public float timeLeft = 1f;
    public playermove pm;

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime/20;
        transform.localScale = new Vector3(timeLeft, 1, 1);

        transform.localPosition = new Vector3(-(1 - timeLeft) / 2, 0, -2);
        if(timeLeft < 0 )
        {
            pm.isNoTime = true;
            pm.isStoped = false;

            foreach (GameObject ind in pm.Indicators)
            {
                ind.SetActive(false);
            }

            pm.sandClockUI.SetActive(false);
            pm.rb.gravityScale = 1;
            pm.rb.velocity = pm.postForce;
        }
    }
}
