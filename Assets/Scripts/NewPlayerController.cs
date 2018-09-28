using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour {

    private CharacterController _characterController;
    private float Gravity = 20.0f;
    private Vector3 _moveDirection = Vector3.zero;
    public float moveSpeed = 5.0f;
    private Vector3 _gravity = Vector3.zero;
    public float RotationSpeed = 350.0f;

    public float JumpSpeed = 8.0f;
    // Use this for initialization
    void Start () {
        _characterController = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        //get input axis
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        SetMoveMent(h, v);

    }
    void SetMoveMent(float horizontal,float vertical)
    {
        Vector3 camForward_Dir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight_Dir = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)).normalized;
        Vector3 move = vertical * camForward_Dir + horizontal * camRight_Dir;
        if (move.magnitude > 1f)
            move.Normalize();
        move = transform.InverseTransformDirection(move);

        float turnamout = Mathf.Atan2(move.x, move.z);

        transform.Rotate(0, turnamout * RotationSpeed * Time.deltaTime , 0);
        
        if (_characterController.isGrounded)
        {
            _moveDirection = transform.forward * move.magnitude;
            _moveDirection *= moveSpeed;
        }
        _moveDirection.y -= Gravity * Time.deltaTime;
        
        _characterController.Move(_moveDirection * Time.deltaTime);
    }
     
}
