using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class WinpointCheck : NetworkBehaviour
{
    public GameObject Canvas;

    private void Awake()
    {
        Canvas = GameObject.Find("Canvas");
    }
    public string PlayerName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Winpoint") && !GetComponent<SetUpLocalPlayer>().isWin)
        {
            Debug.Log("why");
            CmdOnWin(this.gameObject.name);
            GetComponent<SetUpLocalPlayer>().CmdOnWin(true);
        }
    }

    [Command]
    void CmdOnWin(string n)
    {
        RpcOnWin(n);
    }

    [ClientRpc]
    void RpcOnWin(string n)
    {
        PlayerName = n;
        LocalGM.LGMinstanse.ActiveTxt(n);
    }
}