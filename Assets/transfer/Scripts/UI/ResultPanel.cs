using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    private GameObject ResultText;

    private void Awake() {
        ResultText = transform.Find("ResultText").gameObject;
        ResultText.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if(GameController.status == GameController.GameStatus.Success ||
            GameController.status == GameController.GameStatus.Fail)
        {
            show(GameController.status == GameController.GameStatus.Success ? 1 : 0);
        }

    }

    private void show(int status)
    {
        switch(status)
        {
            case 0:
                ResultText.GetComponent<Text>().text = 
                        "SORRY YOU FAILED!\nTHANK YOU FOR PLAYING!";
                break;
            
            case 1:
                ResultText.GetComponent<Text>().text = 
                        "SUCCESS!\nTHANK YOU FOR PLAYING!";
                break;
        }

        ResultText.SetActive(true);
    }
}
