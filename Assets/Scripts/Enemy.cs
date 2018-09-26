using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    Animator _animator;
    Color outlinecolor1;
    Color outlinecolor2;

    float max_hp = 100.0f;
    float now_hp = 100.0f;
    HPbar m_hpbar;
    public bool IsDead
    {
        get
        {
            return now_hp == 0 ;
        }
    }
    public void setInit(float maxhp,float nowhp)
    {
        max_hp = maxhp;
        now_hp = nowhp;
        if(m_hpbar == null)
            m_hpbar = GetComponent<HPbar>();
        m_hpbar.SetInit(now_hp, max_hp, this.gameObject.transform);
    }
    // Use this for initialization
    void Start ()
    {
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        _animator = GetComponent<Animator>();
        outlinecolor1 = new Color(255, 0, 0); // red
        outlinecolor2 = new Color(0, 255, 127); // green
        m_hpbar = GetComponent<HPbar>();
        m_hpbar.SetInit(now_hp, max_hp, this.gameObject.transform);
    }
    // Update is called once per frame
    void Update () {
         
	}
    
    public void hert(float amount)
    {
        now_hp -= amount;
        if (now_hp < 0)
            now_hp = 0;

        m_hpbar.SetBarValue(now_hp);
        if(IsDead)
            Die();
        else
            _animator.SetTrigger("damage");
    }
    public void Die()
    {
        _animator.SetTrigger("dead");
        Invoke("dieDestory", 2.0f);
    }
    void dieDestory()
    {
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
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
