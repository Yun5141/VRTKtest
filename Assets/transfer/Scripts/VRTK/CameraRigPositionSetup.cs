using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRigPositionSetup : MonoBehaviour
{
    
    GameObject InitArea;

    private void Start() {
        string PlayArea = "RightPlayArea"; //depending on the player order entering the game
        InitArea = GameObject.Find("Env/"+PlayArea+"/InitCharZone");
        this.transform.parent = InitArea.transform;
        this.transform.localPosition = new Vector3(0,0,0);
    }
}
