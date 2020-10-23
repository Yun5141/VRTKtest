using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public enum GameStatus { Start, Success, Fail }
    public static GameStatus status;


    private void Awake()
    {
        status = GameStatus.Start;
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
