using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AndroidCheck : MonoBehaviour
{

    public Text text;
    private Gyroscope gyro;

    public float start;

    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        gyro = Input.gyro;
        if (!gyro.enabled)
        {
            gyro.enabled = true;
        }

        start = gyro.attitude.y;
    }

    // Update is called once per frame
    void Update()
    {

        text.transform.localPosition += new Vector3((start - gyro.attitude.y) * 10, 0f, 0f);

        if(Input.touchCount == 1)
        {
            text.transform.Rotate(0f, 0f, 10f);
        }

        /*
        text.text = (int)(gyro.attitude.x / 0.0001) + ", " 
            + (int)(gyro.attitude.y / 0.0001) + ", " 
            + (int)(gyro.attitude.z / 0.0001) + ", " 
            + (int)(gyro.attitude.w / 0.0001);
        */
    }
}
