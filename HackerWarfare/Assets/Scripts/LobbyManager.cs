using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField roomNameInputField;
    [SerializeField] private GameObject lobbyUI;
    [SerializeField] private GameObject roomUi;
    [SerializeField] private TMP_Text roomNameText;
    
    [SerializeField] private LobbyPrefab lobbyPrefab;
    private List<LobbyPrefab> lobbyPrefabs = new List<LobbyPrefab>();
    [SerializeField] private Transform contentObject;
    
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

    public override void OnJoinedRoom()
    {
        lobbyUI.SetActive(false);
        roomUi.SetActive(true);
        roomNameText.text = "Room: " + PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnLeftRoom()
    {
        roomUi.SetActive(false);
        lobbyUI.SetActive(true);
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
}
