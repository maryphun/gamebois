﻿using UnityEngine;
using System.Collections;
using System;
using UdpKit;

public class Menu : Bolt.GlobalEventListener
{
    //public void OnGUI()
    //{
    //    GUILayout.BeginArea(new Rect(10, 10, Screen.width - 20, Screen.height - 20));

    //    if (GUILayout.Button("Start Server", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
    //    {
    //        // START SERVER
    //        BoltLauncher.StartServer();
    //    }

    //    if (GUILayout.Button("Start Client", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
    //    {
    //        // START CLIENT
    //        BoltLauncher.StartClient();
    //    }

    //    GUILayout.EndArea();
    //}

    int ssssss = 0;

    public void StartServer()
    {
        if (ssssss == 0)
        {
            ssssss += 1;
            // サーバーを立てる
            BoltLauncher.StartServer();
        }
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
            string matchName = Guid.NewGuid().ToString();

            BoltNetwork.SetServerInfo(matchName, null);
            BoltNetwork.LoadScene("Scene");
        }
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        Debug.LogFormat("Session list updated: {0} total sessions", sessionList.Count);

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
