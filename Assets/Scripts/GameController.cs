using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public float TotalWeight = 6.0f;
    public int massNum = 3;


    [HideInInspector]
    public enum GameStatus { Start, Success, Fail }
    public static GameStatus status;

    public static Dictionary<int, float> MassWeights = new Dictionary<int, float>();


    private void Awake()
    {
        status = GameStatus.Start;

        generateMassWeight();
    }

    private void generateMassWeight()
    {
        for (int i = 1; i <= massNum; i += 1)
        {
            //MassWeights.Add(i,)
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void checkSuccess()
    {

    }
}
