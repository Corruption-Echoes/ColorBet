using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public List<GameObject> Chairs;
    // Start is called before the first frame update
    void Start()
    {
        updateChairs();
        int playernum=0;
        GameObject chairToUse=null;
        for(int x=0;x<Chairs.Count;x++)
        {
            if (!Chairs[x].GetComponent<Chair>().isTaken)
            {
                chairToUse = Chairs[x];
                playernum = x+1;
                x += Chairs.Count;
            }
        }
        if (chairToUse!=null)
        {
            GameObject g=PhotonNetwork.Instantiate(playerPrefab.name,chairToUse.transform.position,chairToUse.transform.rotation);
            g.GetComponent<BetManager>().load(playernum);
            chairToUse.GetComponent<Chair>().TakeChar();
        }
        else
        {
            PhotonNetwork.LeaveRoom();
        }
    }
    public override void OnRoomPropertiesUpdate(Hashtable newProperties)
    {
        updateChairs();
    }
    public void updateChairs()
    {
        //Ugly and brutal, but it'll function. It's also rarely called, so optimization value is low for a tech demo
        Chairs[0].GetComponent<Chair>().isTaken = (bool)PhotonNetwork.CurrentRoom.CustomProperties["P1"];
        Chairs[1].GetComponent<Chair>().isTaken = (bool)PhotonNetwork.CurrentRoom.CustomProperties["P2"];
        Chairs[2].GetComponent<Chair>().isTaken = (bool)PhotonNetwork.CurrentRoom.CustomProperties["P3"];
        Chairs[3].GetComponent<Chair>().isTaken = (bool)PhotonNetwork.CurrentRoom.CustomProperties["P4"];
        Chairs[4].GetComponent<Chair>().isTaken = (bool)PhotonNetwork.CurrentRoom.CustomProperties["P5"];
        Chairs[5].GetComponent<Chair>().isTaken = (bool)PhotonNetwork.CurrentRoom.CustomProperties["P6"];
    }

}
