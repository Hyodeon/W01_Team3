using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowStageNum : MonoBehaviour
{
    public int stage = 1;
    public TextMeshProUGUI DisplayText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayText.text = "Stage " + stage;
    }
}
