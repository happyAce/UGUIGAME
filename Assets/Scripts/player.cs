using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    //player
    public float playermovespeed;
    public GameObject playermovepoint;
    private Plane playerplane;
    private GameObject pmp;
    private bool m_move = false;
    private Animator _animator;
     

    private CharacterController _characterController;
    private float Gravity = 20.0f;
    private Vector3 _moveDirection = Vector3.zero;
    public float Speed = 5.0f;

    public float RotationSpeed = 250.0f;

    public float JumpSpeed = 7.0f;


    //
    private bool canattack = false;
    private GameObject m_enemy;
    // Use this for initialization
    void Start ()
    {
        //gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        playerplane = new Plane(Vector3.up, this.transform.position);
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    
    void rotate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 e_rot = transform.eulerAngles;
            e_rot.x = -90;
            e_rot.y = 0;
            e_rot.z = 0;
            transform.eulerAngles = e_rot;
        }
        // 按S键，向下移动
        else if (Input.GetKey(KeyCode.S))
        {
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.x = 90;
            eulerAngles.y = 0;
            eulerAngles.z = 180;
            transform.eulerAngles = eulerAngles;
             
        }
        // 按A键，向左移动
        else if (Input.GetKey(KeyCode.A))
        {
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.x = 0;
            eulerAngles.y = -90;
            eulerAngles.z = 90;
            transform.eulerAngles = eulerAngles;
             
        }
        // 按D键，向右移动
        else if (Input.GetKey(KeyCode.D))
        {
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.x = 0;
            eulerAngles.y = 90;
            eulerAngles.z = -90;
            transform.eulerAngles = eulerAngles;
             
        }

    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            _animator.SetTrigger("attack1");
        }
        // Get Input for axis
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Calculate the forward vector
        Vector3 camForward_Dir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = v * camForward_Dir + h * Camera.main.transform.right;

        if (move.magnitude > 1f) move.Normalize();

        // Calculate the rotation for the player
        move = transform.InverseTransformDirection(move);

        // Get Euler angles
        float turnAmount = Mathf.Atan2(move.x, move.z);

        transform.Rotate(0, turnAmount * RotationSpeed * Time.deltaTime, 0);
        //rotate();

        if (_characterController.isGrounded)
        {
            _moveDirection = transform.forward * move.magnitude;

            _moveDirection *= Speed;

            if (Input.GetButton("Jump"))
            {
                _animator.SetBool("jump",true);
                _moveDirection.y = JumpSpeed;

            }
            else
            {
                _animator.SetBool("jump", false);
                _animator.SetBool("run", move.magnitude > 0);
            }
        }

        _moveDirection.y -= Gravity * Time.deltaTime;
        _characterController.Move(_moveDirection * Time.deltaTime);

        #region clickmove
        //////
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //float hitdistance = 0f;
        //if (Input.GetMouseButtonDown(0)|| Input.GetMouseButton(0))
        //{
        //    if (playerplane.Raycast(ray, out hitdistance))
        //    {
        //        Vector3 moveposition = ray.GetPoint(hitdistance);
        //        if (pmp != null)
        //            Destroy(pmp);
        //        pmp = Instantiate(playermovepoint, moveposition, Quaternion.identity);
        //        m_move = true;
        //    }

        //}

        //if (m_move)
        //    moving();
        //else
        //{
        //    if (canattack)
        //        attacking();
        //    else
        //        _animator.SetBool("param_toidle", true);
        //}
        #endregion

    }
    void moving()
    {
        transform.position = Vector3.MoveTowards(transform.position, pmp.transform.position, playermovespeed);
        transform.LookAt(pmp.transform);
        _animator.SetBool("param_idletorunning", true);
    }
    void attacking()
    {
        transform.LookAt(m_enemy.transform);
        _animator.SetBool("param_idletohit01", true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PMP")
        {
            Destroy(other.gameObject);
            m_move = false;
        }

    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "PMP")
        {
            Destroy(other.gameObject);
            m_move = false;
        }
        if(other.transform.tag == "Enemy")
        {
            canattack = true;
            m_enemy = other.gameObject;
            m_move = false;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.transform.tag == "Enemy")
        {
            canattack = false;
        }
    }
}
