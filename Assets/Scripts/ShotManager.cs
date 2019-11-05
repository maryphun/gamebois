using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour]
public class ShotManager : Bolt.GlobalEventListener
{ 
    public override void OnEvent(ShotHappened evnt)
    {
        // 弾生成
        Instantiate((GameObject)Resources.Load("Bullet"), evnt.Position, evnt.Angle);
    }
}
