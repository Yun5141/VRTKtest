using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

using RootMotion.FinalIK;


///角色控制脚本
///orginal from: https://www.cnblogs.com/qiaogaojian/p/5868561.html

public class Player : MonoBehaviourPun
{
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
    }

    void Update()   //这个是刷新的意思   以帧为单位的大概每刷新一次1/20秒
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        this.transform.position = Body.position;   //这句代码是把得到的偏移量通过translate(平移函数)给玩家  从而使得玩家的位置得到改变
        headControl.transform.position = Neck.position;
        headControl.transform.rotation = Neck.rotation;
        rightHandControl.transform.position = RightHand.position;
        rightHandControl.transform.rotation = RightHand.rotation;
        leftHandControl.transform.position = LeftHand.position;
        leftHandControl.transform.rotation = LeftHand.rotation;
    }

}

