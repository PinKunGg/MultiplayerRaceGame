using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class ServerGM : NetworkManager
{
    public static ServerGM SGMinstanse;
    private void Awake() {
        if(SGMinstanse == null){
            SGMinstanse = this;
        }
    }

    public override void OnStopClient()
    {
        base.OnStopClient();

        LocalGM.LGMinstanse.WinCollider.SetActive(false);
        LocalGM.LGMinstanse.WinText.SetActive(false);
        LocalGM.LGMinstanse.JumpBar.SetActive(false);
        Camera.main.GetComponent<CameraFollow360>().player = null;
        Camera.main.GetComponent<CameraFollow360>().gotostart = true;
    }
}
