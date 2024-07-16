using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject TitleSection, CreditsSection, LevelSection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPressed()
    {
        TitleSection.gameObject.SetActive(false);
        LevelSection.gameObject.SetActive(true);
    }

    public void CreditsPressed()
    {
        TitleSection.gameObject.SetActive(false);
        CreditsSection.gameObject.SetActive(true);
    }

    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }

    public void BackPressed()
    {
        LevelSection.gameObject.SetActive(false);
        CreditsSection.gameObject.SetActive(false);
        TitleSection.gameObject.SetActive(true);
    }
}
