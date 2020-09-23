using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class AfterResultEventListener : MonoBehaviourPunCallbacks
{
    private VRTK_ControllerEvents controllerEvents;

    private GameObject ProgressText;
    private AsyncOperation ao;
    private bool isLoad =false;
    
    private void Awake() {
        controllerEvents = GetComponent<VRTK_ControllerEvents>();

        GameObject root = GameObject.Find("Canvas");
        ProgressText = root.transform.Find("ProgressText").gameObject;
        ProgressText.SetActive(false);
    }

    private void Update() {
        
        if(GameController.status == GameController.GameStatus.Success ||
           GameController.status == GameController.GameStatus.Fail)
        {
            controllerEvents.StartMenuPressed += BackToStartScene;
            controllerEvents.GripPressed += Exit;
        }
    }

    private void BackToStartScene(object sender, ControllerInteractionEventArgs e)
    {
        //if(!photonView.IsMine)
        //return;

        ProgressText.SetActive(true);
        StartCoroutine("Load");
    }
    IEnumerator Load()
    {
		int displayProgress = -1;
		int toProgress = 100;

		while(displayProgress < toProgress)
		{
			++displayProgress;
			ShowProgress(displayProgress);

			if(!isLoad)
			{
				ao = SceneManager.LoadSceneAsync(0);
				ao.allowSceneActivation = false;
				isLoad = true;
			}

			yield return new WaitForEndOfFrame();
		}

		if(displayProgress == 100)
		{
			ao.allowSceneActivation = true;
			StopCoroutine("Load");
		}
    }
    private void ShowProgress(int progress)
    {
        ProgressText.GetComponent<Text>().text = "Loading: " + progress.ToString() + "%";
    }

    private void Exit(object sender, ControllerInteractionEventArgs e)
    {

        //if(!player.IsMine)
        //return;

        PhotonNetwork.LeaveRoom();
        
    }

    public override void OnLeftRoom()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
        Debug.Log("Quit");


        //UnityEditor.EditorApplication.isPlaying = false;    // delete when build
    }

}
