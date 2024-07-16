using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject TitleSection, ControlsSection, LevelSection, SpecialGroup;
    public UnityEngine.UI.Button[] buttons;
    public TextAsset[] MapFiles;
    public static TextAsset MapFile;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttons.Length && i < MapFiles.Length; i++)
        {
            int val = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(val));
            buttons[i].interactable = true;
            Resources.Load("Assets/Map/MapBuilder - Map_" + (i + 1));
        }
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

    public void ControlsPressed()
    {
        TitleSection.gameObject.SetActive(false);
        ControlsSection.gameObject.SetActive(true);
    }

    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }

    public void BackPressed()
    {
        LevelSection.gameObject.SetActive(false);
        ControlsSection.gameObject.SetActive(false);
        TitleSection.gameObject.SetActive(true);
    }

    public void OnButtonClick(int value)
    {
        Debug.Log("Button value " + value);
        MapFile = MapFiles[value];
        SceneManager.LoadScene("Scenes/SHScene");
    }
}
