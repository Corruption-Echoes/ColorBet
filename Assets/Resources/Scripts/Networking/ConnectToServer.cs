using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public GameObject ConnectMessage;
    public GameObject WelcomeMessage;
    public GameObject ConnectButton;
    public GameObject ErrorDisplay;

    public void AttemptConnect()
    {
        WelcomeMessage.SetActive(false);
        ConnectButton.SetActive(false);
        ConnectMessage.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
