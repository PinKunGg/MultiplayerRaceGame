using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SetUpLocalPlayer : NetworkBehaviour
{

    [SyncVar(hook = "OnPlayerNameChange")]
    public string playerName;
    [SyncVar(hook = "OnTimeChange")]
    public string timer;
    public static int GMNum;
    public static int TimeCount = 3;
    public static bool TimeCountDownStart;
    public Text PlayerName;
    public Text TimeTxt;
    public bool isWin;
    public bool isTimeisZero;

    private void Awake()
    {
        TimeTxt = GameObject.Find("TimeTxt").GetComponent<Text>();
    }
    private void Start()
    {
        if (isLocalPlayer)
        {
            this.GetComponent<MyPlayerController>().enabled = false;

            Camera.main.GetComponent<CameraFollow360>().player = this.gameObject.transform;
            LocalGM.LGMinstanse.JumpBar.SetActive(true);
            LocalGM.LGMinstanse.JumpBar.GetComponent<Slider>().value = 0f;
            StartCoroutine(jumpbarload());
        }
        else
        {
            this.GetComponent<MyPlayerController>().enabled = false;
        }

        if (playerName == "")
        {
            GMNum++;
            CmdChangePlayerNum(GMNum.ToString());
            OnPlayerNameChange(GMNum.ToString());
        }
        else
        {
            CmdChangePlayerNum(playerName);
            OnPlayerNameChange(playerName);
        }

        CmdStartTimer();
    }
    private void Update()
    {
        if (TimeCountDownStart && !isTimeisZero)
        {
            // OnTimeChange(TimeCount.ToString());
            timer = TimeCount.ToString();
        }
    }

    IEnumerator jumpbarload()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            LocalGM.LGMinstanse.JumpBar.GetComponent<Slider>().value += 1f * Time.deltaTime;
        }
    }

    [Command]
    void CmdChangePlayerNum(string value)
    {
        playerName = value;
    }

    void OnPlayerNameChange(string value)
    {
        playerName = value;

        bool isNumber = int.TryParse(value, out _);

        if (isNumber)
        {
            this.gameObject.name = "Player" + value;
            PlayerName.text = "Player" + value;
        }
        else
        {
            this.gameObject.name = value;
            PlayerName.text = value;
        }
    }

    [Command]
    public void CmdOnWin(bool value)
    {
        RpcOnWin(value);
    }

    [Command]
    public void CmdStartTimer()
    {
        TimeStartManager();
    }

    public void TimeStartManager()
    {
        if (!TimeCountDownStart)
        {
            TimeCountDownStart = true;
            StartCoroutine(TimerCount());
        }
    }

    IEnumerator TimerCount()
    {
        while (TimeCountDownStart)
        {
            if (TimeCount > 0)
            {
                yield return new WaitForSeconds(1f);
                TimeCount--;
            }
            else
            {
                break;
            }
        }
    }
    public void OnTimeChange(string time)
    {
        timer = time;
        TimeCount = int.Parse(time);

        if (TimeCount > 0)
        {
            TimeTxt.text = "Timer : " + timer;
            Debug.Log(TimeCount);
        }
        else
        {
            Debug.Log(TimeCount);
            if (!isTimeisZero)
            {
                isTimeisZero = true;
                CmdTimeOutGo();
            }
        }
    }
    [Command]
    void CmdTimeOutGo()
    {
        RpcTimeOutGo();
        Debug.Log("CmdTimeOutGo");
    }

    [ClientRpc]
    void RpcTimeOutGo()
    {
        Debug.Log("RpcTimeOutGo");
        if (isLocalPlayer)
        {
            this.GetComponent<MyPlayerController>().enabled = true;
        }
        else
        {
            this.GetComponent<MyPlayerController>().enabled = false;
        }
        TimeTxt.text = "GO!";
        Invoke("DelayGoTxt", 1f);
    }
    void DelayGoTxt()
    {
        TimeTxt.gameObject.SetActive(false);
    }

    [ClientRpc]
    void RpcOnWin(bool value)
    {
        isWin = value;
    }
}
