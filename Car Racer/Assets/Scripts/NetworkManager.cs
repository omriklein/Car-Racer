using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
    [SerializeField]
    private string VERSION = "cc";

    public string roomName = "room1";
    public string playerPrefubName = "Car";
    public Transform spawnPoint;

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(VERSION);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnJoinedLobby()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, MaxPlayers = 4 };

        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(playerPrefubName, spawnPoint.position, spawnPoint.rotation, 0);
    }
}
