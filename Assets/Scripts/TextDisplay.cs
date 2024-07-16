using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI totalDeath;
    public TMPro.TextMeshProUGUI nowDeath;
    public TMPro.TextMeshProUGUI stage;

    private void Start()
    {
        nowDeath.text = "Dead in Here : " + PlayerPrefs.GetInt("NowDeath", 0).ToString();
        totalDeath.text = "Total Dead : " + PlayerPrefs.GetInt("TotalDeath", 0).ToString();
    }

    public void TotalDeathUpdate()
    {
        int d;
        d = PlayerPrefs.GetInt("TotalDeath", 0);
        d++;
        PlayerPrefs.SetInt("TotalDeath", d);
        totalDeath.text = "Total Dead : " + d.ToString();
    }

    public void NowDeathUpdate()
    {
        int d;
        d = PlayerPrefs.GetInt("NowDeath", 0);
        d++;
        PlayerPrefs.SetInt("NowDeath", d);
        nowDeath.text = "Dead in Here : " + d.ToString();
    }

    public void StageUpdate()
    {
        int chapter = 0;
        int st = 0;
        int d = PlayerPrefs.GetInt("Stage", 1);

        chapter = d / 5;
        st = d % 5;
        if(st == 0)
        {
            st = 5;
        }
        else
        {
            chapter++;
        }
        stage.text = "Stage " + chapter.ToString() + "-" + st.ToString();
    }
}
