using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.GrabAttachMechanics;
using VRTK.SecondaryControllerGrabActions;
using Photon.Pun;


[RequireComponent(typeof(VRTK_InteractableObject))]
[RequireComponent(typeof(VRTK_TrackObjectGrabAttach))]
[RequireComponent(typeof(VRTK_SwapControllerGrabAction))]
[RequireComponent(typeof(VRTK_InteractHaptics))]
[RequireComponent(typeof(VRTK_InteractObjectHighlighter))]


/* 
- When it falls outside the playarea, re-init itself at the last playarea it belongs to
- Interactable: grabbable, {& 射线, 待加入?}
- {can be interacted by two players at the same time, 待加入}
- 
*/

public class Mass : MonoBehaviourPun
{
    /* Initial Setup */
    private VRTK_InteractableObject io;
    private VRTK_TrackObjectGrabAttach grabAttach;
    private VRTK_SwapControllerGrabAction secGrabAction;
    private VRTK_InteractObjectHighlighter highlighter;
    private string colorCode = "#82DFE0";

    /* Properties */
    private enum Status { Normal, ChangedPlayArea, ExceedLimit };
    private float DroppedDisLimit = 8.0f;
    private float reinitStartHeight = 5.0f;

    /* Behaviours */
    private GameObject LeftPlayArea;
    private GameObject RightPlayArea;
    private float[] LeftAreaRange;
    private float[] RightAreaRange;
    private string currentPlayArea;

    /* Initial Setup */
    private void Awake()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Mass");
        this.gameObject.tag = "Mass";

        io = this.GetComponent<VRTK_InteractableObject>();
        grabAttach = this.GetComponent<VRTK_TrackObjectGrabAttach>();
        secGrabAction = this.GetComponent<VRTK_SwapControllerGrabAction>();
        highlighter = this.GetComponent<VRTK_InteractObjectHighlighter>();

        setupIO();
        setupGrabAttach();
        setupHighlighter();

        setupNetworking();

    }

    private void setupIO()
    {
        io.isGrabbable = true;
        io.grabAttachMechanicScript = grabAttach;
        io.secondaryGrabActionScript = secGrabAction;
    }

    private void setupGrabAttach()
    {
        grabAttach.precisionGrab = true;
        float massValue = this.GetComponent<Rigidbody>().mass;
        grabAttach.velocityLimit = (float)(200 / massValue);
        grabAttach.maxDistanceDelta = Mathf.Infinity;
    }

    private void setupHighlighter()
    {
        Color color;
        ColorUtility.TryParseHtmlString(colorCode, out color);
        highlighter.touchHighlight = color;
    }

    private void setupNetworking()
    {
        /*
        this.gameObject.AddComponent<PhotonView>();
        this.gameObject.AddComponent<PhotonTransformView>();
        
        PhotonTransformView c = this.gameObject.GetComponent<PhotonTransformView>();
        c.m_SynchronizePosition = true;
        c.m_SynchronizeRotation = true;

        PhotonView v = this.gameObject.GetPhotonView();
        if(v.ObservedComponents == null)
        v.ObservedComponents = new List<Component>();
        v.ObservedComponents.Add(c);
        */
    }

    /* Behaviours */
    private void Start()
    {
        RightPlayArea = GameObject.FindGameObjectWithTag("RightPlayArea");
        LeftPlayArea = GameObject.FindGameObjectWithTag("LeftPlayArea");
        RightAreaRange = getBoundary(RightPlayArea);
        LeftAreaRange = getBoundary(LeftPlayArea);

        currentPlayArea = GameController.InitMassZoneFlag == 0 ? "RightPlayArea" : "LeftPlayArea";
    }

    private void FixedUpdate()
    {
        switch (getStatus())
        {
            case Status.ExceedLimit:
                Transform CurrentPlayArea = currentPlayArea == "RightPlayArea" ? RightPlayArea.transform : LeftPlayArea.transform;
                reinit(getBoundary(CurrentPlayArea.GetChild(0).gameObject));
                break;

            case Status.ChangedPlayArea:
                changeParent();
                break;

            default:
                break;
        }

    }

    private Status getStatus()
    {
        bool isInRightPlayArea = withinBoundary(RightAreaRange, this.gameObject);
        bool isInLeftPlayArea = withinBoundary(LeftAreaRange, this.gameObject);

        if ((isInRightPlayArea && (currentPlayArea == "LeftPlayArea")) ||
            (isInLeftPlayArea && (currentPlayArea == "RightPlayArea")))
            return Status.ChangedPlayArea;

        else if (this.transform.position.y < RightPlayArea.transform.position.y - DroppedDisLimit)
            return Status.ExceedLimit;

        else
            return Status.Normal;
    }

    private void reinit(float[] areaRange)
    {
        GameObject newMass = Instantiate(this.gameObject) as GameObject;

        // set position of the new mass
        float random_x = Random.Range(areaRange[0], areaRange[1]);//x_min, x_max
        float random_z = Random.Range(areaRange[2], areaRange[3]);//z_min, z_max
        newMass.transform.position = new Vector3(random_x, reinitStartHeight, random_z);
        newMass.transform.SetParent(this.transform.parent);

        Destroy(this.gameObject);
    }

    private float[] getBoundary(GameObject Area)
    {
        Vector3 length_raw = Area.GetComponent<MeshFilter>().mesh.bounds.size;
        float x = length_raw.x * Area.transform.lossyScale.x;
        float y = length_raw.y * Area.transform.lossyScale.y;
        float z = length_raw.z * Area.transform.lossyScale.z;
        Vector3 length = new Vector3(x, y, z);

        Vector3 centerPos = Area.transform.position; //position in world space

        Vector3 top_left = centerPos + new Vector3(-length.x, 0, length.z) * 0.5f;
        Vector3 top_right = centerPos + new Vector3(length.x, 0, length.z) * 0.5f;
        Vector3 bottom_left = centerPos + new Vector3(-length.x, 0, -length.z) * 0.5f;

        var param = new float[4];
        param[0] = top_left.x;  //X_min
        param[1] = top_right.x; //X_max
        param[2] = bottom_left.z;  //Z_min
        param[3] = top_left.z;   //Z_max

        return param;
    }

    private bool withinBoundary(float[] areaRange, GameObject go)
    {
        return go.transform.position.x > areaRange[0] && go.transform.position.x < areaRange[1] &&
            go.transform.position.z > areaRange[2] && go.transform.position.z < areaRange[3];
    }

    private void changeParent()
    {
        currentPlayArea = currentPlayArea == "RightPlayArea" ? "LeftPlayArea" : "RightPlayArea";
    }
}
