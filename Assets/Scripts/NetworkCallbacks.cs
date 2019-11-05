using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour(BoltNetworkModes.Client)]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
    public override void SceneLoadLocalDone(string map)
    {
        var spawnPosition = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        BoltNetwork.Instantiate(BoltPrefabs.SciFiWarriorPBR, spawnPosition, Quaternion.identity);
    }

    public override void OnEvent(NetworkConnected evnt)
    {
        int playerNumber;   //自分のプレイヤー番号
        playerNumber = evnt.PlayerNumber;   //サーバーから渡してきた自分のプレイヤー番
    }
}