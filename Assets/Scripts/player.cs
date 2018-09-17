using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    public float playermovespeed;
    public GameObject playermovepoint;
    private Plane playerplane;
    private GameObject pmp;
    private bool move = false;
    // Use this for initialization
    void Start () {

        playerplane = new Plane(Vector3.up, this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdistance = 0f;
        if (Input.GetMouseButtonDown(0))
        {
            if (playerplane.Raycast(ray, out hitdistance))
            {
                Vector3 moveposition = ray.GetPoint(hitdistance);
                if (pmp == null)
                {
                    pmp = Instantiate(playermovepoint, moveposition, Quaternion.identity);
                }
                else
                {

                    Destroy(pmp);
                    pmp = Instantiate(playermovepoint, moveposition, Quaternion.identity);

                }
                move = true;
            }
        }
        if(move)
           moving();
    }
    void moving()
    {
        transform.position = Vector3.MoveTowards(transform.position, pmp.transform.position, playermovespeed);
        transform.LookAt(pmp.transform);
        //if (transform.position == pmp.transform.position)
        //{
        //    if(pmp!=null)
        //        Destroy(pmp);
        //    move = false;
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PMP")
        {
            Destroy(other.gameObject);
            move = false;
        }
    }
}
