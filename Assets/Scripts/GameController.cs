using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

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

        int restWeightSum = halfWeightSum;
        int w;
        print("LEFT");
        for (int i = 1; i <= LeftMassNum; i += 1)
        {
            count = i;
            print("count: "+ count);
            print("restWSum: "+ restWeightSum);
            if(i == LeftMassNum)
            {

                MassWeights.Add(count, restWeightSum);
            }
            else
            {
                w = restWeightSum == LeftMassNum - i + 1 ? 1 : Random.Range(1, restWeightSum);
                print("w: "+w);
                MassWeights.Add(count,w);
                restWeightSum = restWeightSum - w;
            }
        }
        print("RIGHT");
        restWeightSum = halfWeightSum;
        for(int j = count + 1; j <= massNum; j += 1)
        {
            count = j;
            print("count: "+ count);
            print("restWSum: "+ restWeightSum);
            if(j == massNum)
            {
                MassWeights.Add(count, restWeightSum);
            }
            else
            {
                w = restWeightSum == massNum - j + 1 ? 1 : Random.Range(1, restWeightSum);
                print("w: "+w);
                MassWeights.Add(count,w);
                restWeightSum = restWeightSum - w;
            }
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
