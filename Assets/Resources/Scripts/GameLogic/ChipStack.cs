using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipStack : MonoBehaviour
{
    public List<MeshRenderer> Chips;
    Material mat;
    private void Start()
    {
        mat = new Material(Shader.Find("Standard (Specular setup)"));
        mat.color = new Color(0,0,0);
        randomizeColor();
    }
    public void randomizeColor()
    {
        updateChipColors(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
    public void updateChipColors(float r,float g,float b)
    {
        mat.color = new Color(r,g,b);
        mat.SetColor("_Color", new Color(r, g, b));
        foreach (MeshRenderer x in Chips)
        {
            x.material = mat;
        }
    }
}
