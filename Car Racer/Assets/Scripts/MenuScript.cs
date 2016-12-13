using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

using System.Xml;

public enum GamePlatform
{
    Android,
    Windows,
    Unknown,
}

public class MenuScript : MonoBehaviour
{
    public static GamePlatform GP;

    public TextAsset XMLFile;
    public static Dictionary<int, string> Levels = new Dictionary<int, string>();
    public static int levelSelected = 1;

    public Button[] levelsButtons;
    public Image loadingImage;
    public Text wellcomeText;

    public Material normalMat;
    public Material chosenMat;

    // Use this for initialization
    void Start()
    {

        loadingImage.gameObject.SetActive(false);

        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                GP = GamePlatform.Android;
                wellcomeText.text += " Android"; break;
            case RuntimePlatform.WindowsPlayer:
                GP = GamePlatform.Windows;
                wellcomeText.text += " Windows"; break;
            default:
                GP = GamePlatform.Unknown;
                wellcomeText.text += " Unkown"; break;

        }

        loadFromXML();
        //##########
        foreach (int key in Levels.Keys)
            print(key + ", " + Levels[key]);

        foreach (Button b in levelsButtons)
        {
            b.GetComponent<Image>().material = normalMat;
        }
        levelsButtons[0].GetComponent<Image>().material = chosenMat;
    }

    public void LoadGame(string sceneName)
    {
        loadingImage.gameObject.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }

    public void LevelSelected(int num)
    {
        levelSelected = num;
        foreach (Button b in levelsButtons)
        {
            b.GetComponent<Image>().material = normalMat;
        }

        levelsButtons[num - 1].GetComponent<Image>().material = chosenMat;
        print(levelsButtons[num - 1].name);
    }

    public void loadFromXML()
    {
        XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
        xmlDoc.LoadXml(XMLFile.text); // load the file.
        XmlNodeList levelsList = xmlDoc.GetElementsByTagName("arena");

        foreach (XmlNode level in levelsList)
        {
            Levels.Add(int.Parse(level.Attributes["LevelNumber"].Value), level.FirstChild.InnerText);
        }
    }
}
