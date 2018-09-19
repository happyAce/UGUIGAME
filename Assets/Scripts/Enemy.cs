using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    Animation anim;
	// Use this for initialization
	void Start ()
    {
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        anim = gameObject.GetComponent<Animation>();
    }
    // Update is called once per frame
    void Update () {
        anim.CrossFade("idle");
	}
    
    private void OnMouseOver()
    {
        Shader sd = Shader.Find("Custom/OutLine2");
        this.gameObject.GetComponentInChildren<Renderer>().material.shader = sd;
    }
    private void OnMouseExit()
    {
        Shader sd = Shader.Find("Legacy Shaders/Diffuse");
        this.gameObject.GetComponentInChildren<Renderer>().material.shader = sd;
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            
        }
    }
}
