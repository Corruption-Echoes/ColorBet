using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public GameObject Swap1;
    public GameObject Swap2;
    public bool isSwapped = false;
    
    public void Swap()
    {
        isSwapped = !isSwapped;
        if (isSwapped)
        {
            Swap2.SetActive(true);
            Swap1.SetActive(false);
        }
        else
        {

            Swap1.SetActive(true);
            Swap2.SetActive(false);
        }
    }
    
}
