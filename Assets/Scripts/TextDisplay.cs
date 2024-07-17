using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TextDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI totalDeath;
    public TMPro.TextMeshProUGUI nowDeath;
    public TMPro.TextMeshProUGUI stage;

    public GameObject[] star;

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

    public void MakeStar()
    {
        if(PlayerPrefs.GetInt("NowDeath", 0) < 30)
        {
            StartCoroutine(PopStar(1));
        }
        if (PlayerPrefs.GetInt("NowDeath", 0) < 20)
        {
            StartCoroutine(PopStar(2));
        }
        if (PlayerPrefs.GetInt("NowDeath", 0) < 10)
        {
            StartCoroutine(PopStar(3));
        }
    }

    IEnumerator PopStar(int count)
    {
        for(int i = 0; i < count; i++)
        {
            float elapsedTime = 0f;
            star[i].SetActive(true);
            while (elapsedTime < 0.5f)
            {
                star[i].GetComponent<RectTransform>().localScale = Vector3.Lerp(Vector3.zero, Vector3.one, elapsedTime / 0.5f);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            star[i].GetComponent<RectTransform>().localScale = Vector3.one;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
