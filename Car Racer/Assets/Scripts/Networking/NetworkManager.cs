using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
    [SerializeField]
    private string VERSION = "Level1";

    public static int maxPlayers = 4;

    public string roomName = "room1";
    public string playerPrefubName = "Car";
    public Transform[] spawnPoints; // few spawn points. one for each car (player)

    // Use this for initialization
    void Start()
    {
        VERSION = "Level" + MenuScript.levelSelected;
        PhotonNetwork.ConnectUsingSettings(VERSION);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnJoinedLobby()
    {
        print("lobby");
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, MaxPlayers = 4 };

        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    void OnJoinedRoom()
    {
        int numOfPlayers = PhotonNetwork.room.playerCount;
        PhotonNetwork.Instantiate(playerPrefubName, spawnPoints[numOfPlayers - 1].position, spawnPoints[numOfPlayers - 1].rotation, 0);
        print("room");
    }
}
