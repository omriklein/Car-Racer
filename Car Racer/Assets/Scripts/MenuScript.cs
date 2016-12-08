using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public enum Platform
{
    Computer,
    Android,
}


public class MenuScript : MonoBehaviour
{

    public static Platform platform;
    public Image loadingImage;
    // Use this for initialization
    void Start()
    {
        loadingImage.gameObject.SetActive(false);

        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                platform = Platform.Android; break;
            case RuntimePlatform.WindowsPlayer:
                platform = Platform.Computer; break;
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
