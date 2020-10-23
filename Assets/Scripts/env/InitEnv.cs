using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitEnv : MonoBehaviour
{
    private GameObject Masses;
    private GameObject RightPlayArea;
    private GameObject LeftPlayArea;

    private void Awake()
    {

        RightPlayArea = transform.Find("playArea/RightPlayArea").gameObject;

        initWalls(getAreaVertices(RightPlayArea));

        initOppositePlayArea();

    }
    /* Initialize Invisible Walls */
    /*
     return array: Vector3 param[4]
     element 0: top left position
     element 1: bottom left position
     element 2: bottom right position
     element 3: top right position 
    */
    private Vector3[] getAreaVertices(GameObject Area)
    {
        Vector3 length_raw = Area.GetComponent<MeshFilter>().mesh.bounds.size;
        float x = length_raw.x * Area.transform.lossyScale.x;
        float y = length_raw.y * Area.transform.lossyScale.y;
        float z = length_raw.z * Area.transform.lossyScale.z;
        Vector3 length = new Vector3(x, y, z);

        Vector3 centerPos = Area.transform.position; //position in world space

        Vector3 top_left = centerPos + new Vector3(-length.x, 0, length.z) * 0.5f;
        Vector3 bottom_left = centerPos + new Vector3(-length.x, 0, -length.z) * 0.5f;
        Vector3 bottom_right = centerPos + new Vector3(length.x, 0, -length.z) * 0.5f;
        Vector3 top_right = centerPos + new Vector3(length.x, 0, length.z) * 0.5f;

        var param = new Vector3[4];
        param[0] = top_left;
        param[1] = bottom_left;
        param[2] = bottom_right;
        param[3] = top_right;

        return param;
    }

    private Vector3 getCenterOfTwoVertices(Vector3 A, Vector3 B)
    {
        return new Vector3((A.x + B.x) * 0.5f, (A.y + B.y) * 0.5f, (A.z + B.z) * 0.5f);
    }

    private float getDistanceOfTwoVertices(Vector3 A, Vector3 B)
    {
        return Mathf.Sqrt(Mathf.Pow(A.x - B.x, 2) + Mathf.Pow(A.y - B.y, 2) + Mathf.Pow(A.z - B.z, 2));
    }
    private void initWalls(Vector3[] areaVertices)
    {

        GameObject[] Walls = new GameObject[4];
        GameObject WallsParent = transform.Find("playArea/RightPlayArea/Walls").gameObject;
        for (int i = 0; i < 4; i++)
        {

            Walls[i] = new GameObject("Fence Wall");
            Walls[i].AddComponent<BoxCollider>();

            Walls[i].transform.localEulerAngles = i == 0 ? new Vector3(0, 0, 0) : Walls[i - 1].transform.localEulerAngles + new Vector3(0, 90, 0);

            Walls[i].transform.position = getCenterOfTwoVertices(areaVertices[i], areaVertices[(i + 1) > 3 ? 0 : (i + 1)]);
            Walls[i].GetComponent<BoxCollider>().size = new Vector3(1, 5, getDistanceOfTwoVertices(areaVertices[i], areaVertices[(i + 1) > 3 ? 0 : (i + 1)]));

            Walls[i].transform.SetParent(WallsParent.transform);
            Walls[i].layer = LayerMask.NameToLayer("Ignore Raycast");
            //Walls[i].AddComponent<IgnoreRightPointer>();
            //Walls[i].AddComponent<IgnoreLeftPointer>();

        }

        StaticBatchingUtility.Combine(WallsParent);
    }


    /* Initialize Left Play Area */
    private void initOppositePlayArea()
    {
        Vector3 leftPos = RightPlayArea.transform.position;
        leftPos.z = 0 - leftPos.z;
        LeftPlayArea = Instantiate(RightPlayArea, leftPos, Quaternion.identity);
        LeftPlayArea.name = "LeftPlayArea";

        LeftPlayArea.transform.parent = RightPlayArea.transform.parent;

        Transform[] RightChilds = RightPlayArea.GetComponentsInChildren<Transform>();
        Transform[] LeftChilds = LeftPlayArea.GetComponentsInChildren<Transform>();
        for (int i = 1; i < RightChilds.Length; i++)
        {
            leftPos = RightChilds[i].position;
            leftPos.z = 0 - leftPos.z;
            LeftChilds[i].position = leftPos;
        }
    }
}
