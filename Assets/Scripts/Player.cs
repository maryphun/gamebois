using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Bolt.EntityBehaviour<ICubeStateCustom>
{
    private Camera camera;
    private CharacterController characterController;
    private Animator animator;
    //start()
    public override void Attached()
    {
        //network
        state.SetTransforms(state.CubeTransform, transform);
        
        //player
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        camera = FindObjectOfType<Camera>();
    }

    //update()
    public override void SimulateOwner()
    {
        var speed = 5f;
        var movement = Vector3.zero;
        state.Shot = Input.GetMouseButton(0);

        if (Input.GetKey(KeyCode.W)) { movement.z += 1; }
        if (Input.GetKey(KeyCode.S)) { movement.z -= 1; }
        if (Input.GetKey(KeyCode.A)) { movement.x -= 1; }
        if (Input.GetKey(KeyCode.D)) { movement.x += 1; }

        Ray cameraRay = camera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        if (movement != Vector3.zero)
        {
            transform.position = transform.position + (movement.normalized * speed * BoltNetwork.FrameDeltaTime);
        }
        
        animator.SetBool("Run", movement != Vector3.zero);
        animator.SetBool("Shot", state.Shot);
    }
}