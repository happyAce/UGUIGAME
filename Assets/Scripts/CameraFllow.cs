using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFllow : MonoBehaviour
{
    //目标
    public Transform target;

    //距离
    public float distance = 8f;

    //右键旋转控制部分
    //旋转速度
    private float speedX = 240;
    private float speedY = 120;
    //Y轴旋转的角度限制
    private float minLimitY = 5;
    private float maxLimitY = 45;
    //旋转角度
    private float mX = 0.0f;
    private float mY = 0.0f;

    //鼠标缩放控制部分
    //鼠标缩放距离最值
    private float maxDistance = 10;
    private float minDistance = 1.5f;
    //鼠标缩放速度
    private float zoomSpeed = 2f;

    //差值控制部分
    //是否启用差值计算
    //public bool isNeedDamping = false;
    //差值速度
    //public float dampingSpeed = 10f;

    private Quaternion mRotation;
    private Vector3 mPosition;
    void Start()
    {
        //初始化旋转角度
        mX = 0;
        mY = 30;
        mRotation = Quaternion.Euler(30, 0, 0);
        transform.rotation = mRotation;
    }

    void LateUpdate()
    {
        //第一步（功能一）：右键控制相机旋转部分
        if (target != null && Input.GetMouseButton(1))
        {
            //1.获取鼠标输入
            mX += Input.GetAxis("Mouse X") * speedX * 0.02f;
            mY -= Input.GetAxis("Mouse Y") * speedY * 0.02f;

            //1.1Y轴角度范围限制
            //mY = ClampAngle(mY,minLimitY,maxLimitY);
            mY = Mathf.Clamp(mY, minLimitY, maxLimitY);

            //1.2计算旋转，转化成欧拉角
            mRotation = Quaternion.Euler(mY, mX, 0);

            //2.旋转相机
            //根据是否差值采取不同的角度计算方式,角度旋转的时候不用差值更自然
            //if(isNeedDamping)
            //transform.rotation = Quaternion.Lerp(transform.rotation,mRotation,Time.deltaTime*dampingSpeed);
            //else 
            transform.rotation = mRotation;
            //3.对目标物体的状态限定。不是所有的状态都可以旋转的，比如轻功时、战斗时就不可以旋转（去除物体本身旋转的可能）
            //target.rotation = Quaternion.Euler(new Vector3(0, mX, 0));  //注意，这里mx中的X是针对鼠标的，实际上，在X平面上是绕着Y轴旋转
        }

        //第二步（功能二）：鼠标滚轮缩放部分控制
        distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        //第三步（功能三）：计算相机位置并进行设定
        mPosition = mRotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
        //if (isNeedDamping)
        //{
        //transform.position = Vector3.MoveTowards(transform.position, mPosition, Time.deltaTime * 10.0f);
        //}
        //else
        //{
            transform.position = mPosition;
        //}
    }
   
}