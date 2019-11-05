using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//サーバー側専用コード
[BoltGlobalBehaviour(BoltNetworkModes.Server)]
public class ServerCallback : Bolt.GlobalEventListener
{ 
    private int playerCount;    //サーバー用：プレイヤー番号を入ってきたクライアントを伝える
    public static int playerNumber;   //自分のプレイヤー番号

    public override void SceneLoadLocalDone(string map)
    {
        var spawnPosition = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        BoltNetwork.Instantiate(BoltPrefabs.SciFiWarriorPBR, spawnPosition, Quaternion.identity);
        playerCount = 1;
        playerNumber = playerCount;
    }

    public override void Connected(BoltConnection newConnect)
    {
        playerCount++;
        var casingEvnt = NetworkConnected.Create(newConnect);
        casingEvnt.PlayerNumber = playerCount;
        casingEvnt.Send();
    }

    public override void Disconnected(BoltConnection connection)
    {
        playerCount--;
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }
}
