using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour(BoltNetworkModes.Server)]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
    public override void SceneLoadLocalDone(string map)
    {
        // randomize a position
        var spawnPosition = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        // instantiate cube
        BoltNetwork.Instantiate(BoltPrefabs.SciFiWarriorPBR, spawnPosition, Quaternion.identity);
    }
}