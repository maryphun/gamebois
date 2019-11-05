using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Bolt.EntityBehaviour<ICubeStateCustom>
{
    public float moveSpeed;
    private Camera cam;
    private Animator animator;
    //private CharacterController characterController;
    
    //start()
    public override void Attached()
    {
        //ネットワーク関係
        state.SetTransforms(state.CubeTransform, transform);
        
        //コンポネント
        //characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cam = FindObjectOfType<Camera>();
    }

    //update()
    public override void SimulateOwner()
    {
        //初期化
        var speed = moveSpeed;
        var movement = Vector3.zero;

        //入力
        if (Input.GetKey(KeyCode.W)) { movement.z += 1; }
        if (Input.GetKey(KeyCode.S)) { movement.z -= 1; }
        if (Input.GetKey(KeyCode.A)) { movement.x -= 1; }
        if (Input.GetKey(KeyCode.D)) { movement.x += 1; }
        state.Shot = Input.GetMouseButton(0);

        //マウスポインターの座標を所得
        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

            //向かせる
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        //座標更新
        if (movement != Vector3.zero)
        {
            transform.position = transform.position + (movement.normalized * speed * BoltNetwork.FrameDeltaTime);
        }
        
        //アニメータ
        animator.SetBool("Run", movement != Vector3.zero);
        animator.SetBool("Shot", state.Shot);

        
        if (state.Shot)
        {
            var boltevent = ShotHappened.Create(Bolt.GlobalTargets.Everyone);
            boltevent.FromWho = ServerCallback.playerNumber;
            boltevent.Position = transform.position;
            boltevent.Angle = transform.rotation;
            boltevent.Send();

            //Instantiate((GameObject)Resources.Load("Bullet"), transform.position, transform.rotation);
        }
    }
}