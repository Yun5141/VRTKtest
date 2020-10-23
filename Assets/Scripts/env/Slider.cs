using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    [Tooltip("The left boundary of the slider")]
    public GameObject LeftJoint;

    [Tooltip("The right boundary of the slider")]
    public GameObject RightJoint;
    
    // Update is called once per frame
    void Update()
    {
        var sliderPos = this.transform.position;
        var leftJointPos = LeftJoint.transform.position;
        var rightJointPos = RightJoint.transform.position;

        sliderPos.x = sliderPos.x > leftJointPos.x ? sliderPos.x : leftJointPos.x + 0.5f;
        sliderPos.x = sliderPos.x < rightJointPos.x ? sliderPos.x : rightJointPos.x - 0.5f;

        this.transform.position = sliderPos;
    }
}
