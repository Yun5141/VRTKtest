using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDrawer : MonoBehaviour
{
    public GameObject Father;

    public Transform plateJoint1;
    public Transform plateJoint2;
    public Transform plateJoint3;
    public Transform plateJoint4;

    public Transform slider;

    public string label;

    //private LineRenderer rope1_3;
    //private LineRenderer rope2_4;

    private LineRenderer[] ropes;

    // Start is called before the first frame update
    void Start()
    {
        ropes = new LineRenderer[2];

        for(int i=0; i<2; i++)
        {
            ropes[i] = new GameObject("["+label+"] rope"+(i+1)+"_"+(i+3)).AddComponent<LineRenderer>();
            ropes[i].material = new Material(Shader.Find("Diffuse"));
            ropes[i].positionCount = 3;
            ropes[i].SetWidth(0.1f,0.1f);
            ropes[i].SetColors(Color.black,Color.black);

            ropes[i].transform.parent = Father.transform;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLine();
    }

    public void UpdateLine()
    {
        ropes[0].SetPosition(0,plateJoint1.position);
        ropes[0].SetPosition(2, plateJoint3.position);

        ropes[1].SetPosition(0, plateJoint2.position);
        ropes[1].SetPosition(2, plateJoint4.position);

        ropes[0].SetPosition(1, slider.position);
        ropes[1].SetPosition(1,slider.position);
    }
}
