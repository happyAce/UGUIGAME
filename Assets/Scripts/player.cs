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

    private HPbar m_hpbar;
    private float max_hp = 100.0f;
    private float now_hp = 100.0f;

    private CharacterController _characterController;
    private float Gravity = 20.0f;
    private Vector3 _moveDirection = Vector3.zero;
    public float Speed = 5.0f;
    private Vector3 _gravity = Vector3.zero;
    public float RotationSpeed = 250.0f;

    public float JumpSpeed = 8.0f;


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
        m_hpbar = GetComponent<HPbar>();
        m_hpbar.SetInit(now_hp, max_hp, this.gameObject.transform);
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
    bool attackfinished = true;
    public void SetAttackFinished()
    {
        attackfinished = true;
        SetJumpFinished();
        AttackCondition(0.9f);
    }
    public void SetJumpFinished()
    { 
        if(_animator.GetBool("jump"))
            _animator.SetBool("jump", false);

       
    }
    bool addjumpheight = false;
    public void SetJumpHeight()
    {
        addjumpheight = true;
    }

    private bool mIsControlEnabled = true;

    public void EnableControl()
    {
        mIsControlEnabled = true;
    }

    public void DisableControl()
    {
        mIsControlEnabled = false;
    }
    void Update()
    {
        if (mIsControlEnabled)
        {
            if (Input.GetMouseButtonDown(0) && attackfinished)
            {
                _animator.SetTrigger("attack1");
                attackfinished = false;
            }
            // Get Input for axis
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            // Calculate the forward vector
            Vector3 camForward_Dir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 move = v * Camera.main.transform.forward + h * Camera.main.transform.right;

            if (move.magnitude > 1f) move.Normalize();

            // Calculate the rotation for the player
            move = transform.InverseTransformDirection(move);

            // Get Euler angles
            float turnAmount = Mathf.Atan2(move.x, move.z);

            transform.Rotate(0, turnAmount * RotationSpeed * Time.deltaTime, 0);
            //MoveManager(_characterController);

            if (_characterController.isGrounded)
            {
                _moveDirection = transform.forward * move.magnitude;

                _moveDirection *= Speed;
                
                if (Input.GetButton("Jump")&& !_animator.GetBool("jump"))
                { 
                    _animator.SetBool("jump", true);
                }
                else
                    _animator.SetBool("run", move.magnitude > 0);

                if (addjumpheight)
                {
                   // _moveDirection.y = JumpSpeed;
                    addjumpheight = false;
                }
            }

            _moveDirection.y -= Gravity * Time.deltaTime;
            if (attackfinished)
                _characterController.Move(_moveDirection * Time.deltaTime);
            else
            {
                _gravity.y = _moveDirection.y;
                _characterController.Move(_gravity * Time.deltaTime);
            }
        }
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
    //攻击判定
    private void AttackCondition(float _range)
    {
        // 球形射线检测周围怪物，不用循环所有怪物类列表，无法获取“Enemy”类  
        Collider[] colliderArr = Physics.OverlapSphere(this.gameObject.transform.position, _range, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < colliderArr.Length; i++)
        {
            Vector3 v3 = colliderArr[i].gameObject.transform.position - this.gameObject.transform.position;
            float angle = Vector3.Angle(v3, this.gameObject.transform.forward);
            if (CalculateDistance(colliderArr[i].gameObject))
            {
                // 距离和角度条件都满足了  
                Debug.Log("hit:"+ colliderArr[i].gameObject.name);
                float damagenum = Random.Range(10, 30);
                colliderArr[i].SendMessage("hert", damagenum, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    private bool CalculateDistance(GameObject Target)
    {
        float distance = (this.gameObject.transform.position - Target.transform.position).magnitude;
        Vector3 mfrd = this.gameObject.transform.forward;
        Vector3 tV3 = Target.transform.position - this.gameObject.transform.position;
        // mfrd.normalized（归一化）：方向不变，长度归一，用在只关心方向忽略大小情况下（毕竟以单位1计算比使用float类型数计算方便快速嘛）  
        // Vector3.Dot（点乘）:余弦值；Mathf.Acos()反余弦值(弧度形式表现)  
        // Mathf.Rad2Deg:弧度转度；Mathf.Deg2Rad：度转弧度  
        float deg = Mathf.Acos(Vector3.Dot(mfrd.normalized, tV3.normalized)) * Mathf.Rad2Deg;
        // 一半扇形区域  
        if (distance < 2f && deg < 120 * 0.5)
        {
            return true;
        }
        return false;
    }
    //定义四个方向的枚举值，按照逆时针方向计算
    protected enum DirectionType
    {
        Direction_Forward = 90,
        Direction_Backward = 270,
        Direction_Left = 180,
        Direction_Right = 0
    }
    private DirectionType mType = DirectionType.Direction_Forward;
    void MoveManager(CharacterController mController)
    {
        //移动方向
        Vector3 mDir = Vector3.zero;
        if (mController.isGrounded)
        {
            //将角色旋转到对应的方向
            if (Input.GetAxis("Vertical") == 1)
            {
                SetDirection(DirectionType.Direction_Forward);
               
                 
            }
            if (Input.GetAxis("Vertical") == -1)
            {
                SetDirection(DirectionType.Direction_Backward);
              
               
            }
            if (Input.GetAxis("Horizontal") == -1)
            {
                SetDirection(DirectionType.Direction_Left);
              
            }
            if (Input.GetAxis("Horizontal") == 1)
            {
                SetDirection(DirectionType.Direction_Right);
              
            }

        }
    }

    //设置角色的方向，有问题
    void SetDirection(DirectionType mDir)
    {
        if (mType != mDir)
        {
            transform.Rotate(Vector3.up * (mType - mDir));
            mType = mDir;
        }
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
