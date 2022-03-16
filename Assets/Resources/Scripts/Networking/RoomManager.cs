using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject Input;
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(Input.GetComponent<TMP_InputField>().text);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        CreateRoomWithProps();
    }
    public void CreateRoomWithProps()
    {
        RoomOptions RO = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 6,
        };
        Hashtable RoomProperties = new Hashtable();
        RoomProperties.Add("P1", false);
        RoomProperties.Add("P2", false);
        RoomProperties.Add("P3", false);
        RoomProperties.Add("P4", false);
        RoomProperties.Add("P5", false);
        RoomProperties.Add("P6", false); 
        RoomProperties.Add("P1L", false);
        RoomProperties.Add("P2L", false);
        RoomProperties.Add("P3L", false);
        RoomProperties.Add("P4L", false);
        RoomProperties.Add("P5L", false);
        RoomProperties.Add("P6L", false);
        RoomProperties.Add("P1Chips", 10);
        RoomProperties.Add("P2Chips", 10);
        RoomProperties.Add("P3Chips", 10);
        RoomProperties.Add("P4Chips", 10);
        RoomProperties.Add("P5Chips", 10);
        RoomProperties.Add("P6Chips", 10);
        RoomProperties.Add("RedWins", 0);
        RO.CustomRoomProperties = RoomProperties;
        PhotonNetwork.CreateRoom(Input.GetComponent<TMP_InputField>().text,RO);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
