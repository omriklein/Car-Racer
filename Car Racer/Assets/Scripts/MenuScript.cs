using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public enum GamePlatform
{
    Android,
    Windows,
    Unknown,
}


public class MenuScript : MonoBehaviour
{
    public static GamePlatform GP;

    public Image loadingImage;

    public Text wellcomeText;

    // Use this for initialization
    void Start()
    {
        loadingImage.gameObject.SetActive(false);

        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                GP = GamePlatform.Android;
                wellcomeText.text += " Android";  break;
            case RuntimePlatform.WindowsPlayer:
                GP = GamePlatform.Windows;
                wellcomeText.text += " Windows"; break;
            default: GP = GamePlatform.Unknown;
                wellcomeText.text += " Unkown"; break;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadGame(string sceneName)
    {
        loadingImage.gameObject.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }

}
