using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using VRTK;
//using RockVR.Video;

public class GameController : MonoBehaviour
{    
    [HideInInspector]
    public enum GameStatus {Start, Success, Fail}
    public static GameStatus status;

    private bool isStoredVideo;


    [Header("Debug")]
    public string characterName = "";

    // for checking success
    [Header("Success Requirements")]
    //[Tooltip("In minutes")]
    //[Seri]
    public float required_deltaAngle = 5.0f;
    public float required_timeKept_inMilliseconds = 30 * 1000; 

    public static float TimeLimitation = 10.0f; // in minutes, used in GetTime.cs
    private GameObject HorizontalCylinder;
    private Stopwatch stopWatch;
    private GameObject TimeCountDown;

    // for controller events
    private VRTK_ControllerEvents controllerEvents; 

    private void Awake() {

        status = GameStatus.Start;
        isStoredVideo = false;
        
        // player data
        if(characterName != "")
        PlayerPrefs.SetString("CharacterName",characterName);
        UnityEngine.Debug.Log("GameInfo/PlayScene: Character Name: " + PlayerPrefs.GetString("CharacterName"));

        // video capture
        //VideoCaptureCtrl.instance.StartCapture();

        // for checking success
        HorizontalCylinder = GameObject.Find("Env/beam/Horizontal Cylinder");
        stopWatch = new Stopwatch();
        TimeCountDown = GameObject.Find("Canvas/Time Count Down");
        TimeCountDown.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(isStoredVideo)
        return;

        if((status == GameStatus.Success) || (status == GameStatus.Fail))
        {
            //VideoCaptureCtrl.instance.StopCapture();
            isStoredVideo = true;
        }
        
        checkSuccess();

    }

    void checkSuccess()
    {
        float deltaAngle = System.Math.Abs(HorizontalCylinder.transform.localEulerAngles.z - 90);

        if(deltaAngle > required_deltaAngle)
        {
            if(stopWatch.IsRunning)
            {
                stopWatch.Reset();
                TimeCountDown.SetActive(false);
            }
            return;
        }

        if(deltaAngle < required_deltaAngle)
        {
            UnityEngine.Debug.Log("GameInfo/PlayScene: Checking Success");

            if(!stopWatch.IsRunning)
            stopWatch.Start();
            else
            {
                stopWatch.Stop();
                float timePassed = stopWatch.ElapsedMilliseconds;
                if(timePassed < required_timeKept_inMilliseconds)
                {
                    int num = (int)((required_timeKept_inMilliseconds - timePassed) / 1000);
                    if(num < 6)
                    {
                        TimeCountDown.SetActive(true);
                        TimeCountDown.GetComponent<Text>().text = num.ToString();    
                    }
                    stopWatch.Start();
                }
                else
                {
                    UnityEngine.Debug.Log("GameInfo/PlayScene: success");
                    status = GameStatus.Success;

                }
            }
        }
    }
}
