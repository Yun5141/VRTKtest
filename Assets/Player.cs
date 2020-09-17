using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

using RootMotion.FinalIK;


///角色控制脚本
///orginal from: https://www.cnblogs.com/qiaogaojian/p/5868561.html

public class Player : MonoBehaviourPun
{

    // public float m_speed = 1;   //这个是定义的玩家的移动速度  之所以Public是因为为了方便对其进行调节  (public的属性和对象会在Unity中物体的脚本选项中显示出来  前提是你把脚本挂在了物体上)
    private Transform Body;
    private Transform Neck;
    private Transform RightHand;
    private Transform LeftHand;

    private GameObject headControl;
    private GameObject rightHandControl;
    private GameObject leftHandControl;

    void Awake () {
        Body = GameObject.Find("[VRSimulator_CameraRig]").transform;
        Neck = GameObject.Find("Neck").transform;
        headControl = GameObject.Find("headControl");
        RightHand = GameObject.Find("RightHand").transform;
        rightHandControl = GameObject.Find("rightHandControl");
        LeftHand = GameObject.Find("LeftHand").transform;
        leftHandControl = GameObject.Find("leftHandControl");
        // LeftHand = GameObject.Find("LeftHand").transform;
        // RightHand = GameObject.Find("RightHand");
    }

    void Update()   //这个是刷新的意思   以帧为单位的大概每刷新一次1/20秒
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        // float movex = 0;   //这个代表的是玩家在x轴上的移动

        // float movez = 0;   //这个代表的是玩家在z轴上的移动



        // if (Input.GetKey(KeyCode.W))   //这个意思是"当按下W键时"
        // {
        //     movez += m_speed * Time.deltaTime;   //物体获得在z轴方向上的增量   也就是向前
        // }

        // if (Input.GetKey(KeyCode.S))   //按下S键时
        // {
        //     movez -= m_speed * Time.deltaTime;   //后
        // }

        // if (Input.GetKey(KeyCode.A))   //A键
        // {
        //     movex -= m_speed * Time.deltaTime;    //左
        // }

        // if (Input.GetKey(KeyCode.D))   //D键
        // {
        //     movex += m_speed * Time.deltaTime;   //右
        // }

        this.transform.position = Body.position;   //这句代码是把得到的偏移量通过translate(平移函数)给玩家  从而使得玩家的位置得到改变
        headControl.transform.position = Neck.position;
        headControl.transform.rotation = Neck.rotation;
        rightHandControl.transform.position = RightHand.position;
        rightHandControl.transform.rotation = RightHand.rotation;
        leftHandControl.transform.position = LeftHand.position;
        leftHandControl.transform.rotation = LeftHand.rotation;
        // VRIK vrik = this.GetComponent<VRIK>();
        
        // vrik.solver.spine.headTarget = GameObject.FindWithTag("Neck").transform;
        // vrik.solver.leftArm.target = GameObject.FindWithTag("LH").transform;
        // vrik.solver.rightArm.target = GameObject.FindWithTag("RH").transform;
    }

}

