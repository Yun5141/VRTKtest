using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    private void Awake() {
        this.transform.localPosition = new Vector3(0,0,0);
    }
}
