using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

using Photon.Pun;

public class IKTargetConnect : MonoBehaviourPun
{
	private VRIK vrik;
	private bool isLeftArmHandOn = false;
	private bool isRightArmHandOn = false;

	public Vector3 leftTargetRotate = new Vector3(0,90,0);
	public Vector3 rightTargetRotate = new Vector3(0,-90,0);


	private void Awake() {
		vrik = GetComponent<VRIK>();
	}

	private void Start() {
		//if(photonView.IsMine)
		//vrik.solver.spine.headTarget = GameObject.FindGameObjectWithTag("NeckHead").transform;
	}

	private void Update() {

		if(photonView.IsMine == false && PhotonNetwork.IsConnected == true)
		return;
		
		vrik.solver.spine.headTarget = GameObject.FindGameObjectWithTag("NeckHead").transform;
		{
			if(GameObject.FindGameObjectWithTag("LArmHand") != null)
			{
				if(isLeftArmHandOn == false)
				{
					Transform leftTarget = GameObject.FindGameObjectWithTag("LArmHand").transform;
					leftTarget.localEulerAngles = leftTargetRotate;
					vrik.solver.leftArm.target = leftTarget;
					isLeftArmHandOn = true;
				}
			}
			if(GameObject.FindGameObjectWithTag("RArmHand") != null)
			{
				if(isRightArmHandOn == false)
				{
					Transform rightTarget = GameObject.FindGameObjectWithTag("RArmHand").transform;
					rightTarget.localEulerAngles = rightTargetRotate;
					vrik.solver.rightArm.target = rightTarget;
					isRightArmHandOn = true;
				}
			}
		}
		
	}

}
