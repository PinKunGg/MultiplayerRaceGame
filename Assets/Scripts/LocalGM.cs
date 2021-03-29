using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalGM : MonoBehaviour
{
    public GameObject Canvas,WinText,WinCollider,SpeedFx,JumpBar;

    public static LocalGM LGMinstanse;

    private void Awake() {

        if(LGMinstanse == null){
            LGMinstanse = this;
        }

        Canvas = GameObject.Find("Canvas");
        WinText = GameObject.Find("WinText");
        
    }
    private void Start() {
        WinText.SetActive(false);
        SpeedFx.SetActive(false);
        WinCollider.SetActive(false);
        JumpBar.SetActive(false);
    }

    public void ActiveTxt(string n){
        WinText.SetActive(true);
        WinText.GetComponentInChildren<Text>().text = n + " Win!";
    }
}
