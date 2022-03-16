using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class BetManager : MonoBehaviourPunCallbacks
{
    public int currentChips;
    public int currentBet;
    public int playerID;
    public bool isLockedIn=false;
    public GameObject ChipRenderer;
    public GameObject BetRenderer;
    public GameObject LockButtonText;
    List<GameObject> Chips;
    List<GameObject> ChipsBet;
    public GameObject ChipPrefab;
    public GameObject ChipSpawnPoint;
    GameObject ChipBetPoint;
    public GameObject RedButton;
    public GameObject GreenButton;
    public bool betRed = true;
    // Start is called before the first frame update
    private void Start()
    {
        Chips = new List<GameObject>();
        ChipsBet = new List<GameObject>();
        ChipBetPoint = GameObject.FindGameObjectWithTag("BetPoint");
        HandleChips();
        UpdateUI();
    }
    public void load(int pnum)
    {
        playerID = pnum;
        currentChips = (int)PhotonNetwork.CurrentRoom.CustomProperties["P"+playerID+"Chips"];
        currentBet = 0;
    }
    public void toggleLock()
    {
        isLockedIn = !isLockedIn;
        Hashtable hash = new Hashtable();
        hash.Add("P"+playerID+"L", isLockedIn);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
        UpdateUI();
    }
    public void increaseBet()
    {
        if (!isLockedIn)
        {
            currentBet += 1;
            if (validateBet())
            {
                moveChipToBet();
            }
            UpdateUI();
        }
    }
    public void decreaseBet()
    {
        if (!isLockedIn)
        {
            currentBet -= 1;
            if (validateBet())
            {
                moveChipToStart();
            }
            UpdateUI();
        }
    }
    public bool validateBet()
    {
        if (currentBet < 0)
        {
            currentBet = 0;
            return false;
        }else if (currentBet > currentChips)
        {
            currentBet = currentChips;
            return false;
        }
        return true;
    }
    public void changeColor()
    {
        betRed = !betRed;
        if (betRed)
        {
            RedButton.SetActive(true);
            GreenButton.SetActive(false);
        }else
        {
            RedButton.SetActive(false);
            GreenButton.SetActive(true);
        }
    }
    public void HandleChips()
    {
        for(int x=0;x<ChipsBet.Count;x=0)
        {
            moveChipToStart();
        }
        while (Chips.Count < currentChips)
        {
            GameObject g = PhotonNetwork.Instantiate(ChipPrefab.name, ChipSpawnPoint.transform.position, ChipSpawnPoint.transform.rotation);
            Chips.Add(g);
        }
        while (Chips.Count > currentChips)
        {
            GameObject g = Chips[Chips.Count - 1];
            Chips.Remove(g);
            PhotonNetwork.Destroy(g);
        }
    }
    public void moveChipToBet()
    {
        Chips[currentBet - 1].transform.position = ChipBetPoint.transform.position;
        ChipsBet.Add(Chips[currentBet - 1]);
    }
    public void moveChipToStart()
    {
        ChipsBet[0].transform.position = ChipSpawnPoint.transform.position;
        ChipsBet.RemoveAt(0);
    }
    public void playResult(int correct)
    {
        GameObject.FindGameObjectWithTag("ColorRevealObject").GetComponent<ColorReveal>().Reveal(correct);
        if (correct==1&&betRed||correct==2&&!betRed)
        {
            currentChips += currentBet;
        }
        else
        {
            currentChips -= currentBet;
        }
        currentBet = 0;
        if (currentChips < 1)
        {
            currentChips = 10;
        }
        HandleChips();
        toggleLock();
    }
    public void UpdateUI()
    {
        ChipRenderer.GetComponent<TMPro.TMP_Text>().text = "" + currentChips + "0";
        BetRenderer.GetComponent<TMPro.TMP_Text>().text = "" + currentBet + "0";
        if (isLockedIn)
        {
            LockButtonText.GetComponent<TMPro.TMP_Text>().text = "Unlock";
        }
        else
        {
            LockButtonText.GetComponent<TMPro.TMP_Text>().text = "Confirm";
        }
    }
    public override void OnRoomPropertiesUpdate(Hashtable newProperties)
    {
        bool AllLocked = true;
        int minimumID = 0;
        if (newProperties.ContainsKey("RedWins"))
        {
            playResult((int)PhotonNetwork.CurrentRoom.CustomProperties["RedWins"]);
        }
        else
        {
            if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["P6"])
            {
                minimumID = 6;
                if (!(bool)PhotonNetwork.CurrentRoom.CustomProperties["P6L"])
                {
                    AllLocked = false;
                }
            }
            if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["P5"])
            {
                minimumID = 5;
                if (!(bool)PhotonNetwork.CurrentRoom.CustomProperties["P5L"])
                {
                    AllLocked = false;
                }
            }
            if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["P4"])
            {
                minimumID = 4;
                if (!(bool)PhotonNetwork.CurrentRoom.CustomProperties["P4L"])
                {
                    AllLocked = false;
                }
            }
            if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["P3"])
            {
                minimumID = 3;
                if (!(bool)PhotonNetwork.CurrentRoom.CustomProperties["P3L"])
                {
                    AllLocked = false;
                }
            }
            if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["P2"])
            {
                minimumID = 2;
                if (!(bool)PhotonNetwork.CurrentRoom.CustomProperties["P2L"])
                {
                    AllLocked = false;
                }
            }
            if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["P1"])
            {
                minimumID = 2;
                if (!(bool)PhotonNetwork.CurrentRoom.CustomProperties["P1L"])
                {
                    AllLocked = false;
                }
            }
            if (AllLocked && playerID == minimumID)
            {
                bool winnerIs = false;//Let's go with a very silly way to flip a coin
                for (int i = 0; i < 5; i += Random.Range(0, 2))
                {
                    winnerIs = !winnerIs;
                }
                Hashtable hash = new Hashtable();
                if (winnerIs)
                {
                    hash.Add("RedWins", 1);
                }
                else
                {
                    hash.Add("RedWins", 2);
                }
                hash.Add("P1L", false);
                hash.Add("P2L", false);
                hash.Add("P3L", false);
                hash.Add("P4L", false);
                hash.Add("P5L", false);
                hash.Add("P6L", false);
                PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
            }
        }
        
    }
}
