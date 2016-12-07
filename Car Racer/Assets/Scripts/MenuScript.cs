using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Image loadingImage;
    // Use this for initialization
    void Start()
    {
        loadingImage.gameObject.SetActive(false);
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
