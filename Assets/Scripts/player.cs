using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    //player
    public float playermovespeed;
    public GameObject playermovepoint;
    private Plane playerplane;
    private GameObject pmp;
    private bool move = false;
    Animation anim;
    
    //
    private bool canattack = false;
    private GameObject m_enemy;
    // Use this for initialization
    void Start () {

        playerplane = new Plane(Vector3.up, this.transform.position);
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdistance = 0f;
        if (Input.GetMouseButtonDown(0)|| Input.GetMouseButton(0))
        {
            if (playerplane.Raycast(ray, out hitdistance))
            {
                Vector3 moveposition = ray.GetPoint(hitdistance);
                if (pmp != null)
                    Destroy(pmp);
                pmp = Instantiate(playermovepoint, moveposition, Quaternion.identity);
                move = true;
            }
           
        }
         
        if (move)
            moving();
        else
        {
            if (canattack)
                attacking();
            else
                anim.CrossFade("idle");
        }
          
    }
    void moving()
    {
        transform.position = Vector3.MoveTowards(transform.position, pmp.transform.position, playermovespeed);
        transform.LookAt(pmp.transform);
        anim.CrossFade("walk");
    }
    void attacking()
    {
        transform.LookAt(m_enemy.transform);
        anim.CrossFade("attack");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PMP")
        {
            Destroy(other.gameObject);
            move = false;
        }
        if(other.tag == "Enemy")
        {
            canattack = true;
            m_enemy = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            canattack = false;
        }
    }
}
