using UnityEngine;
using Bolt.Matchmaking;
using System;
using UdpKit;

public class Menu : Bolt.GlobalEventListener
{
    public void StartServer()
    {
        // サーバーを立てる
        BoltLauncher.StartServer();
    }

    public void StartClient()
    {
        // 他のサーバーに入る
        BoltLauncher.StartClient();
    }
    
    
    public override void BoltStartDone()
    {
        if (BoltNetwork.IsServer)
        {
            string matchName = "TestMatch";
            BoltMatchmaking.CreateSession(matchName, null, "Scene");
            //BoltNetwork.SetServerInfo(matchName, null);

            //BoltNetwork.LoadScene("Scene");
        }
    }


    //サーバーが立ち上がった時と、排除されたときのcallback　数秒ごとに呼び出される
    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        Debug.LogFormat("Session list updated: " + sessionList.Count + " total sessions");

        foreach (var session in sessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;

            if (photonSession.Source == UdpSessionSource.Photon)
            {
                BoltNetwork.Connect(photonSession);
            }
        }
    }
}
