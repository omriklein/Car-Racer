using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour
{

    public GameObject myCamera;

    // Use this for initialization
    void Start()
    {
        if (photonView.isMine)
        {
            myCamera.SetActive(true);
            this.GetComponent<Motor>().enabled = true;
        }
        else { }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
