using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTrigger : MonoBehaviour
{
    private float currentWeightSum = 0.0f;
    public float TargetWeightSum = 10.0f;    //can obtained from the game controller

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Mass")
        {
            Rigidbody r = other.transform.gameObject.GetComponent<Rigidbody>();
            currentWeightSum = currentWeightSum + r.mass;
            Debug.Log("GameInfo: mass enter trigger, curent weight sum:  " + currentWeightSum);

            if (currentWeightSum == TargetWeightSum)
                GameController.status = GameController.GameStatus.Success;

            Debug.Log("GameInfo: " + GameController.status);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Mass")
        {
            Rigidbody r = other.transform.gameObject.GetComponent<Rigidbody>();
            currentWeightSum = currentWeightSum - r.mass;
            Debug.Log("GameInfo: mass exit trigger, current weight sum: " + currentWeightSum);

            if (currentWeightSum == TargetWeightSum)
                GameController.status = GameController.GameStatus.Success;

            Debug.Log("GameInfo: " + GameController.status);
        }
    }
}
