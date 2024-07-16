using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowDeathNum : MonoBehaviour
{
    public static int death = 0;
    public TextMeshProUGUI DisplayText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayText.text = "Deaths: " + death;
    }
}
