using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PUNLauncher : MonoBehaviourPunCallbacks
{

    GameObject Player1;
    GameObject Player2;

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
            Player1 = PhotonNetwork.Instantiate("ybot", new Vector3(3, 0, 0), Quaternion.identity, 0);
            Debug.Log("GameInfo: Instantiate Player 1 Successfully");
        }
        else
        {
            Player2 = PhotonNetwork.Instantiate("ybot", new Vector3(-3, 0, 0), Quaternion.identity, 0);
            Debug.Log("GameInfo: Instantiate Player 2 Successfully");
        }
    }


}
