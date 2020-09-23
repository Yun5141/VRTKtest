using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetTime : MonoBehaviour {
    int hour;
    int minute;
    int second;
    int millisecond;

    float timeSpend = 0.0f; 
   
    Text timeText;

    // Use this for initialization 
    void Start()
    {
        timeText = GetComponent<Text>();
    }

    // Update is called once per frame 
    void Update()
    {
        if(minute > GameController.TimeLimitation)
        GameController.status = GameController.GameStatus.Fail;

        if(GameController.status == GameController.GameStatus.Success || 
            GameController.status == GameController.GameStatus.Fail)
        return;
        
        timeSpend += Time.deltaTime;

        minute = ((int)timeSpend - hour * 3600) / 60;
        second = (int)timeSpend - hour * 3600 - minute * 60;
        millisecond = (int)((timeSpend - (int)timeSpend) * 1000);

        timeText.text = string.Format("Time: {0:D2}:{1:D2}.{2:D2}", minute, second, millisecond);
    }

}