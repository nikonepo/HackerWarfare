using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField inputUsername;
    [SerializeField] private TMP_Text buttonText;

    public void ConnectToServer()
    {
        if (inputUsername.text.Length != 0)
        {
            PhotonNetwork.NickName = inputUsername.text;
            buttonText.text = "Connect...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("LobbyList");
    }
}
