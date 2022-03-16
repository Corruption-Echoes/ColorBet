using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Chair : MonoBehaviour
{
    public bool isTaken = false;
    public string property;

    public void TakeChar()
    {
        isTaken = true;
        Hashtable hash = new Hashtable();
        hash.Add(property, isTaken);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
    }
}
