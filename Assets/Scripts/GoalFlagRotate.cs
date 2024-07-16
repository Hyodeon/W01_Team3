using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalFlagRotate : MonoBehaviour
{
    public GameObject map;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0,0,-map.transform.rotation.z);
    }
}
