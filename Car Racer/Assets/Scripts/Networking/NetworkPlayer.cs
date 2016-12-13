using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour
{

    public GameObject myCamera;

    private bool disable = true;
    private bool isAlive = true;
    private Vector3 position;
    private Quaternion rotation;
    private float lerpSmoothing = 10f;

    // Use this for initialization
    void Start()
    {
        if (photonView.isMine)
        {
            gameObject.name = "Me";

            myCamera.SetActive(true);
            this.GetComponent<Rigidbody>().useGravity = true;
            this.GetComponent<Motor>().enabled = true; // IF ABLE TO PLAYE LESS THEN MAX PLAYERS

            WheelCollider[] wheelColliders = GetComponentsInChildren<WheelCollider>();
            foreach (WheelCollider wc in wheelColliders)
                wc.enabled = true;

        }
        else
        {
            gameObject.name = "Network Player";

            StartCoroutine("Alive");
        }
    }

    /* ONLY IF YOU CANT PLAY LESS THEN MAX PLAYERS
    void Update()
    {
        if (disable && PhotonNetwork.room.playerCount == NetworkManager.maxPlayers)
        {
            this.GetComponent<Motor>().enabled = true;
            disable = false;
        }
    }
    */

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
            //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * lerpSmoothing);

            yield return null;
        }
    }

}
