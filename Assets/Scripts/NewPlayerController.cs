using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour {

    private CharacterController _characterController;
    private float Gravity = 20.0f;
    private Vector3 _moveDirection = Vector3.zero;
    public float moveSpeed = 5.0f;
    private Vector3 _gravity = Vector3.zero;
    public float RotationSpeed = 250.0f;

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
        if(horizontal != 0 || vertical != 0)
        {
            _moveDirection = new Vector3(horizontal,0f, vertical);
            SetMoveRotate(horizontal, vertical);
        }
        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= Gravity * Time.deltaTime;
        }
     

        _characterController.Move(_moveDirection  * moveSpeed * Time.deltaTime);
    }
    void SetMoveRotate(float horizontal, float vertical)
    {
        Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
        // Create a rotation based on this new vector assuming that up is the global y axis.  
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        // Create a rotation that is an increment closer to the target rotation from the player's rotation.  
        Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

        // Change the players rotation to this new rotation.  
        _characterController.transform.rotation = newRotation;
    }
}
