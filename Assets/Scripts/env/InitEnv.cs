using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitEnv : MonoBehaviour
{
    public GameObject Env;

    public GameObject RightPlayArea;
    private GameObject LeftPlayArea;

    public GameObject TemplateMass;
    public float lerpMin = 0.6f;
    public float lerpMax = 1.1f;
   public float massGap = 1.0f;

    private void Awake()
    {

        //RightPlayArea = transform.Find("playArea/RightPlayArea").gameObject;

        initWalls(getAreaVertices(RightPlayArea));

        initOppositePlayArea();


    }

    private void Start() 
    {
        
        initMasses();
    }


    /* Initialize Block Volumes */
    /*
     return array: Vector3 param[4]
     element 0: top left position
     element 1: bottom left position
     element 2: bottom right position
     element 3: top right position 

     the height of the four vertices is same as the center point of the passed-in go
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
        GameObject WallsParent = RightPlayArea.transform.Find("BlockVolumes").gameObject;
        for (int i = 0; i < 4; i++)
        {

            Walls[i] = new GameObject("Block Volume");
            Walls[i].AddComponent<BoxCollider>();

            Walls[i].transform.localEulerAngles = i == 0 ? new Vector3(0, 0, 0) : Walls[i - 1].transform.localEulerAngles + new Vector3(0, 90, 0);

            Walls[i].transform.position = getCenterOfTwoVertices(areaVertices[i], areaVertices[(i + 1) > 3 ? 0 : (i + 1)]);
            Walls[i].GetComponent<BoxCollider>().size = new Vector3(1, 5, getDistanceOfTwoVertices(areaVertices[i], areaVertices[(i + 1) > 3 ? 0 : (i + 1)]));

            Walls[i].transform.localScale = new Vector3(Walls[i].transform.localScale.x, 2.0f, Walls[i].transform.localScale.z);
            Walls[i].transform.SetParent(WallsParent.transform);
            Walls[i].layer = LayerMask.NameToLayer("BlockVolume");
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
        LeftPlayArea.tag = "LeftPlayArea";

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

    
    /* Initialize Masses*/
    private float getSelfHalfZLength(GameObject obj)
    {
        Vector3 length_raw = obj.GetComponent<MeshFilter>().mesh.bounds.size;
        float z = length_raw.z * obj.transform.lossyScale.z * 0.5f;
        return z;
    }
    private void initMasses()
    {

        GameObject MassParent = Env.transform.Find("Masses").gameObject;

        GameObject initZone = GameController.InitMassZoneFlag == 0? 
                                RightPlayArea.transform.Find("InitMassZone").gameObject : 
                                LeftPlayArea.transform.Find("InitMassZone").gameObject;

        int z_Sign = GameController.InitMassZoneFlag == 0? 1 : -1;

        // 1st mass position

        Vector3[] initZoneAreaVertices = getAreaVertices(initZone);
        Vector3 z_boundaryCenterPoint = GameController.InitMassZoneFlag == 0 ? 
                                        getCenterOfTwoVertices(initZoneAreaVertices[0], initZoneAreaVertices[3]) :
                                        getCenterOfTwoVertices(initZoneAreaVertices[1], initZoneAreaVertices[2]);
        
        Vector3 preInitPos = z_boundaryCenterPoint;
        float selfHalfZLength = 0;
        float preHalfZLength = 0;

        foreach (var x in GameController.MassWeights)
        {
            GameObject newMass = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newMass.AddComponent<Rigidbody>();
            newMass.AddComponent<Mass>();
            newMass.transform.SetParent(MassParent.transform);
            
            newMass.name = x.Key.ToString();
            newMass.GetComponent<Rigidbody>().mass = x.Value;
            newMass.transform.localScale = new Vector3 (1.0f,1.0f,1.0f) * Mathf.Lerp(lerpMin, lerpMax, x.Value / (GameController.TotalWeight / 2.0f));
            
            if(x.Key == 1)
            {
                selfHalfZLength = getSelfHalfZLength(newMass);
                newMass.transform.position = new Vector3(z_boundaryCenterPoint.x, z_boundaryCenterPoint.y, z_boundaryCenterPoint.z + z_Sign * selfHalfZLength);
                Debug.Log("1st mass pos: " + newMass.transform.position);
            }
            else if (x.Key > 1)
            {
                selfHalfZLength = getSelfHalfZLength(newMass);
                newMass.transform.position = new Vector3(z_boundaryCenterPoint.x, z_boundaryCenterPoint.y, preInitPos.z + z_Sign*(preHalfZLength + massGap + selfHalfZLength));
                print(x.Key +"th mass pos: " + newMass.transform.position);
            }
            else
            {
                Debug.Log("ERROR Info: mass name < 1");
            }


            preHalfZLength = selfHalfZLength;
            preInitPos = newMass.transform.position;
            
        }
    }
}
