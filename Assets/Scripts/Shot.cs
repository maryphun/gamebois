using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        var speed = 2;
        // 位置に速度を加算する
        transform.Translate(Vector3.forward * Time.deltaTime);
        //transform.Translate(transform.rotation.x * speed, 0, transform.rotation.z * speed);
        //transform.position = transform.position + (movement.normalized * speed * BoltNetwork.FrameDeltaTime);
    }
}
