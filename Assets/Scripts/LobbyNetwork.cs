using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class LobbyNetwork : LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager,
                                                          GameObject lobbyPlayer,
                                                          GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        SetUpLocalPlayer actualPlayer = gamePlayer.GetComponent<SetUpLocalPlayer>();
        Debug.Log(" lobby.playerName = " + lobby.playerName);
        Debug.Log(" actualPlayer.pName  = " + actualPlayer.playerName);
        actualPlayer.playerName = lobby.playerName;
    }
}
