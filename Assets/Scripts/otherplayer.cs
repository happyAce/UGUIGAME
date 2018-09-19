using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class otherplayer : MonoBehaviour {

    Animation anim;
    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        anim = gameObject.GetComponent<Animation>();
    }
    // Update is called once per frame
    void Update()
    {
        anim.CrossFade("idle");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            
        }
    }
}
