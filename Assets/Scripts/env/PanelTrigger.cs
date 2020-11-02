using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTrigger : MonoBehaviour
{
    private float currentWeightSum = 0.0f;
    public float TargetWeightSum = 10.0f;    //can obtained from the game controller

    public string Label;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Mass")
        {
            Rigidbody r = other.transform.gameObject.GetComponent<Rigidbody>();

            
            GameController.updateWeightSum((int)r.mass, Label);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Mass")
        {
            Rigidbody r = other.transform.gameObject.GetComponent<Rigidbody>();

            GameController.updateWeightSum(-1 * (int)r.mass, Label);
        }
    }
}
