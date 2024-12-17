using TMPro;
using UnityEngine;

public class LobbyPrefab : MonoBehaviour
{
    [SerializeField] private TMP_Text lobbyName;
    [SerializeField] private TMP_Text size;

    private LobbyManager lobbyManager;
    
    private void Start()
    {
        lobbyManager = FindObjectOfType<LobbyManager>();
    }
    
    public void SetLobbyName(string lobbyName)
    {
        this.lobbyName.text = lobbyName;
        this.size.text = "0/4";
    }

    public void Join()
    {
        lobbyManager.JoinLobby(lobbyName.text);
    }
}
