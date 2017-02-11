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

    //buttons
    public Button[] levelsButtons;
    public static int levelSelected = 1;

    //grafics
    public Image loadingImage;
    public Text wellcomeText;

    public Material normalMat;
    public Material chosenMat;

    // Use this for initialization
    void Start()
    {
        //the game is not loading
        loadingImage.gameObject.SetActive(false);

        // which platform is running
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                GP = GamePlatform.Android;
                wellcomeText.text += " Android"; //debug
                break;
            case RuntimePlatform.WindowsPlayer:
                GP = GamePlatform.Windows;
                wellcomeText.text += " Windows";//debug
                break;
            default:
                GP = GamePlatform.Unknown;
                wellcomeText.text += " Unkown";//debug
                break;

        }
        #region xml
        loadFromXML();
        //##########
        //foreach (int key in Levels.Keys)
        // print(key + ", " + Levels[key]);

        foreach (Button b in levelsButtons)
        {
            b.GetComponent<Image>().material = normalMat;
        }
        levelsButtons[0].GetComponent<Image>().material = chosenMat;
        #endregion
    }

    //touches
    private float buttonMaxScale = 2f;

    private Vector3 fp;
    private Vector3 lp;
    Touch touch;
    float scale;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lp = touch.position;
                foreach (Button button in levelsButtons)
                {
                    button.transform.position += new Vector3((lp.x - fp.x), 0f, 0f);

                    scale = Mathf.Clamp((Mathf.Pow(button.transform.position.x - 400, 2) / -40000 + buttonMaxScale), 0.5f, 2);
                    button.transform.localScale = new Vector3(scale, scale, 1f);
                }
                fp = lp;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                phaseEnded();

            }
        }
    }

    private void phaseEnded()
    {
        //center the closest button
        int min = 0;
        for (int i = 0; i < levelsButtons.Length; i++)
        {
            if (Mathf.Abs(levelsButtons[min].transform.localPosition.x) > Mathf.Abs(levelsButtons[i].transform.localPosition.x))
                min = i;
        }

        LevelSelected(min + 1);

        for (int i = 0; i < levelsButtons.Length; i++)
        {
            levelsButtons[i].transform.position = new Vector3(200 * (i - min) + 437, levelsButtons[i].transform.position.y, 0f);

            scale = Mathf.Clamp((Mathf.Pow(levelsButtons[i].transform.position.x - 400, 2) / -40000 + buttonMaxScale), 0.5f, 2);
            levelsButtons[i].transform.localScale = new Vector3(scale, scale, 1f);
        }
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
