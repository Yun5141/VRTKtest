using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetToCanvas : MonoBehaviour
{
    private Canvas canvas;
    private bool targeted;

    private void Start() {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        targeted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeSelf && !targeted)
        {
            canvas.worldCamera = this.GetComponent<Camera>();
            targeted = true;
        }
    }
}
