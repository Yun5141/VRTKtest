using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInteractionEvents : MonoBehaviour
{
    //Start Panel
    public GameObject StartPanel;
    private Button btn_Start;
    private Button btn_ChooseChar;
    private Button btn_Help;

    //Character Choose Panel
    public GameObject CharacterChoosePanel;
    private Button btn_Back_Chara;
    private GameObject CharacterGroup;

    //Help Panel
    public GameObject HelpPanel;
    private Button btn_Back_Help;

    private void Awake() {
        //Start Panel
        btn_Start = transform.Find("StartPanel/btn_Start").GetComponent<Button>();
        btn_ChooseChar = transform.Find("StartPanel/btn_ChooseChar").GetComponent<Button>();
        btn_Help = transform.Find("StartPanel/btn_Help").GetComponent<Button>();

        //character choose panel
        btn_Back_Chara = transform.Find("CharacterChoosePanel/btn_Back_Chara").GetComponent<Button>();
        CharacterGroup = transform.Find("CharacterChoosePanel/Characters").gameObject;

        //Help Panel
        btn_Back_Help = transform.Find("HelpPanel/btn_Back_Help").GetComponent<Button>();

        Init();
    }

    private void Init()
    {
        StartPanel.SetActive(true);
        CharacterChoosePanel.SetActive(false);
        HelpPanel.SetActive(false);

        foreach(Transform character in CharacterGroup.transform)
        {
            character.GetChild(0).gameObject.SetActive(character.GetComponent<Toggle>().isOn);
        }
        PlayerPrefs.SetString("CharacterName",CharacterGroup.transform.GetChild(0).gameObject.name);
    }

    /* Start Panel Events*/
    public void OnClickStart()
    {
        StartPanel.SetActive(false);
        GameController.status = GameController.GameStatus.Start;
    }

    public void OnClickCharacterChoose()
    {
        StartPanel.SetActive(false);
        CharacterChoosePanel.SetActive(true);
        HelpPanel.SetActive(false);
    }

    public void OnClickHelp()
    {
        StartPanel.SetActive(false);
        CharacterChoosePanel.SetActive(false);
        HelpPanel.SetActive(true);
    }
    
    /* Back Button Event */
    public void OnClickBack(GameObject CurrentPanel)
    {
        CurrentPanel.SetActive(false);
        StartPanel.SetActive(true);
    }

    /* Character Choose Panel Events */
    public void OnToggleValueChanged()
    {
        foreach(Transform character in CharacterGroup.transform)
        {
            character.GetChild(0).gameObject.SetActive(character.GetComponent<Toggle>().isOn);
            if(character.GetComponent<Toggle>().isOn)
            {
                PlayerPrefs.SetString("CharacterName",character.gameObject.name);
                Debug.Log(PlayerPrefs.GetString("CharacterName"));
            }
        }
    }
}
