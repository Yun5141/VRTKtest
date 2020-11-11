using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField]
    public static int TotalWeight = 6;
    public int massNum = 3;


    [HideInInspector]
    public enum GameStatus { Start, Success, Fail };
    public static GameStatus status;

    public static Dictionary<int, float> MassWeights = new Dictionary<int, float>();

    public static int InitMassZoneFlag;

    public static int RightCurrentWeightSum = 0;
    public static int LeftCurrentWeightSum = 0;

    private void Awake()
    {
        status = GameStatus.Start;

        generateMassWeight();

        generateInitMassZone();
    }

    private void generateMassWeight()
    {
        int LeftMassNum = Random.Range(1, massNum); //include min, exclude max
        int RightMassNum = massNum - LeftMassNum;
        int halfWeightSum = TotalWeight / 2;

        print("leftMassNum: " + LeftMassNum + "; RightMassNum: " + RightMassNum);
        
        int count = 1;

        int restWeightSum = halfWeightSum - LeftMassNum;
        int w;
        print("LEFT");
        for (int i = 1; i <= LeftMassNum; i += 1)
        {
            count = i;

            int min_w = 1;
            int random_extra = 0;
            if(restWeightSum > 0)
            {
                if(LeftMassNum == i)
                {
                    random_extra = restWeightSum;
                    restWeightSum = 0;
                }
                else
                {
                    if(restWeightSum > 1)
                        random_extra = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * restWeightSum);
                    else
                        random_extra = Random.Range(0.0f, 1.0f) > 0.5f ? 1 : 0;
                }
                restWeightSum -= random_extra;
            }

            MassWeights.Add(count, min_w + random_extra);
        }
        print("RIGHT");
        restWeightSum = halfWeightSum - RightMassNum;
        for(int j = count + 1; j <= massNum; j += 1)
        {
            count = j;
            print("count: "+ count);
            print("restWSum: "+ restWeightSum);

            int min_w = 1;
            int random_extra = 0;
            if(restWeightSum > 0)
            {
                if(massNum == j)
                {
                    random_extra = restWeightSum;
                    restWeightSum = 0;
                }
                else
                {
                    if(restWeightSum > 1)
                        random_extra = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * restWeightSum);
                    else
                        random_extra = Random.Range(0.0f, 1.0f) > 0.5f ? 1 : 0;
                }
                restWeightSum -= random_extra;
            }

            MassWeights.Add(count, min_w + random_extra);
        }
        
        print("FINAL");
        foreach(var x in MassWeights)
        print(x.Key +" "+ x.Value);
    }

    private void generateInitMassZone()
    {
        InitMassZoneFlag = Random.Range(0,2); //either 0 or 1
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public static void updateWeightSum(int mass, string Label)
    {
        if(Label == "Right")
        RightCurrentWeightSum += mass;
        else if(Label == "Left")
        LeftCurrentWeightSum += mass;

        checkSuccess();
    }
    public static void checkSuccess()
    {
        if(RightCurrentWeightSum == LeftCurrentWeightSum && RightCurrentWeightSum + LeftCurrentWeightSum == TotalWeight)
        {
            
        status = GameStatus.Success;
        print("success!!!!");
        }
        else
        {
            print("leftSum:" + LeftCurrentWeightSum + "; RightSum: " + RightCurrentWeightSum);
        }
    }
}
