using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorReveal : MonoBehaviour
{
    // Start is called before the first frame update
    public Material Red;
    public Material Green;
    public Material White;
    float ResetTimer;
    public float ResetSpeed;

    private void Update()
    {
        if (ResetTimer < ResetSpeed)
        {
            ResetTimer += Time.deltaTime;
        }
        else
        {
            setColor("White");
        }
    }
    public void Reveal(int isRed)
    {
        if (isRed==1)
        {
            setColor("Red");
        }
        else
        {
            setColor("Green");
        }
        ResetTimer = 0f;
    }
    public void setColor(string c)
    {
        if (c == "Red")
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Red.color;
        }else if (c == "Green")
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Green.color;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material.color = White.color;
        }
    }
}
