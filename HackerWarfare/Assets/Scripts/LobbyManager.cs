using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField roomNameInputField;
    [SerializeField] private GameObject lobbyUI;
    [SerializeField] private GameObject roomUi;
    [SerializeField] private TMP_Text roomNameText;
    [SerializeField] private TMP_Text playersCountText;

    [SerializeField] private LobbyPrefab lobbyPrefab;
    private List<LobbyPrefab> lobbyPrefabs = new List<LobbyPrefab>();
    [SerializeField] private Transform contentObject;
    [SerializeField] private Transform contentOfPlayers;

    [SerializeField] private float updateInterval = 0.2f;
    private float nextUpdateTime;
    
    private void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    public void CreateRoom()
    {
        if (roomNameInputField.text != "")
        {
            PhotonNetwork.CreateRoom(roomNameInputField.text);
        }
    }

    public void JoinLobby(string lobbyName)
    {
        PhotonNetwork.JoinRoom(lobbyName);
    }

    public void LeftLobby(string lobbyName)
    {
        PhotonNetwork.LeaveRoom();
    }

    public void GoBackToMenu()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }

    public override void OnJoinedRoom()
    {
        lobbyUI.SetActive(false);
        roomUi.SetActive(true);
        roomNameText.text = "Room: " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayersList();
    }

    public override void OnLeftRoom()
    {
        roomUi.SetActive(false);
        lobbyUI.SetActive(true);
        UpdatePlayersList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time > nextUpdateTime)
        {
            UpdateLobbyList(roomList);
            nextUpdateTime = Time.time + updateInterval;
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    private void UpdateLobbyList(List<RoomInfo> roomList)
    {
        foreach (var lobbyPrefab in lobbyPrefabs)
        {
            Destroy(lobbyPrefab.gameObject);
        }
        
        lobbyPrefabs.Clear();

        foreach (RoomInfo roomInfo in roomList)
        {
            LobbyPrefab newLobby = Instantiate(lobbyPrefab, contentObject);
            newLobby.SetLobbyName(roomInfo.Name);
            lobbyPrefabs.Add(newLobby);
        }
    }

    public void UpdatePlayersList()
    {
        //foreach (var playersPrefab in playersPrefabs)
        //{
        //    Destroy(playersPrefab.gameObject);
        //}

        //playerPrefabs.Clear();

        //foreach (var player in PhotonNetwork.CurrentRoom.Players)
        //{
        //    LobbyPrefab newLobby = Instantiate(lobbyPrefab, contentObject);
        //    newLobby.SetLobbyName(player);
        //    lobbyPrefabs.Add(newLobby);
        //}
        playersCountText.text = "Players " + PhotonNetwork.CurrentRoom.PlayerCount + " of 4";
    }
}
