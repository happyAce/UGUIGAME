using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    Animation anim;
    Color outlinecolor1;
    Color outlinecolor2;
    // Use this for initialization
    void Start ()
    {
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        anim = gameObject.GetComponent<Animation>();
        outlinecolor1 = new Color(255, 0, 0); // red
        outlinecolor2 = new Color(0, 255, 127); // green
    }
    // Update is called once per frame
    void Update () {
        anim.CrossFade("idle");
	}
    
    private void OnMouseOver()
    {
       
        
    }
    private void OnMouseExit()
    {

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
