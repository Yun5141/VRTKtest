using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRTKScriptsManager : MonoBehaviour
{
    public GameObject leftHandScripts;
    public GameObject rightHandScripts;

    //private GameObject cameraRig;
    public GameObject leftHand;
    public GameObject rightHand;

    // Start is called before the first frame update
    void Start()
    {
        leftHandScripts.transform.parent = leftHand.transform;
        rightHandScripts.transform.parent = rightHand.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
