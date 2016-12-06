using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour
{

    public GameObject myCamera;

    private bool isAlive = true;
    private Vector3 position;
    private Quaternion rotation;
    private float lerpSmoothing = 5f;

    // Use this for initialization
    void Start()
    {
        if (photonView.isMine)
        {
            myCamera.SetActive(true);
            this.GetComponent<Motor>().enabled = true;
            this.GetComponent<Rigidbody>().useGravity = true;
        }
        else
        {
            StartCoroutine("Alive");
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else // stream.isReading
        {
            position = (Vector3)stream.ReceiveNext();
            rotation = (Quaternion)stream.ReceiveNext();
        }
    }

    IEnumerator Alive()
    {
        while (isAlive)
        {
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * lerpSmoothing);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime *  lerpSmoothing);

            yield return null;
        }
    }

}
