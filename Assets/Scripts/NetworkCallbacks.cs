using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
    public override void SceneLoadLocalDone(string map)
    {
        // randomize a position
        var spawnPosition = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));

        GameObject player = (GameObject)Resources.Load("SciFiWarriorPBR");
        // instantiate cube
        BoltNetwork.Instantiate(player, spawnPosition, Quaternion.identity);
    }
}