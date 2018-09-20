using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectoutline : MonoBehaviour {

    Color outiinecolor;
    // Use this for initialization
    void Start () {
        Color outlinecolor1 = new Color(255, 0, 0); // red
        Color outlinecolor2 = new Color(0, 255, 127); // green
        outiinecolor = this.gameObject.tag == "Enemy"? outlinecolor1: outlinecolor2;
    }
    private void OnMouseOver()
    {
        Shader sd = Shader.Find("Custom/OutLine2");
        for (int x = 0; x < this.gameObject.GetComponentInChildren<Renderer>().materials.Length; ++x)
        {
            this.gameObject.GetComponentInChildren<Renderer>().materials[x].shader = sd;
            this.gameObject.GetComponentInChildren<Renderer>().materials[x].SetColor("_OutLineColor", outiinecolor);
        }
    }
    private void OnMouseExit()
    {
        Shader sd = Shader.Find("Legacy Shaders/Diffuse");
        for (int x = 0; x < this.gameObject.GetComponentInChildren<Renderer>().materials.Length; ++x)
        {
            this.gameObject.GetComponentInChildren<Renderer>().materials[x].shader = sd;
        }

    }
}
