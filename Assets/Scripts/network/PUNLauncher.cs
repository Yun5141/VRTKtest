using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PUNLauncher : MonoBehaviourPunCallbacks
{

    GameObject Player1;
    GameObject Player2;

    public GameObject Camera;

    private void Awake()
    {

        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("GameInfo: Connected using settings");

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new Photon.Realtime.RoomOptions() { MaxPlayers = 2 }, default);
        Debug.Log("GameInfo: Joined Room");

    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Transform RightInitAvatarZone = GameObject.FindGameObjectWithTag("RightPlayArea").transform.Find("InitAvatarZone").transform;
            Vector3 pos = new Vector3(RightInitAvatarZone.position.x, 0, RightInitAvatarZone.position.z);
            Camera.transform.position = pos;
            print("GameInfo: Moved Camera to right position successfully");

            Player1 = PhotonNetwork.Instantiate("Rig", new Vector3(0, 0, 0), Quaternion.identity, 0);
            Debug.Log("GameInfo: Instantiate Player 1 Successfully");
        }
        else
        {
            Transform LeftInitAvatarZone = GameObject.FindGameObjectWithTag("LeftPlayArea").transform.Find("InitAvatarZone").transform;
            Vector3 pos = new Vector3(LeftInitAvatarZone.position.x, 0, LeftInitAvatarZone.position.z);
            Camera.transform.position = pos;
            print("GameInfo: Moved Camera to left position successfully");

            Player2 = PhotonNetwork.Instantiate("Rig", new Vector3(0, 0, 0), Quaternion.identity, 0);
            Debug.Log("GameInfo: Instantiate Player 2 Successfully");
        }
    }
}
