using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour
{
    [Header("Componenent References")]
    public Rigidbody playerRigidbody;

    //Store values
    [Header("Movement Properties")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float turnSpeed = 0.1f;
    public Camera mainCamera; //


    private Vector3 movementDirection;

    private void FixedUpdate()
    {
        MoveThePlayer();
        TurnThePlayer();
    }

    public void UpdateMovementData(Vector3 newMovementDirection)
    {
        movementDirection = newMovementDirection;
    }

    private void MoveThePlayer()
    {
        Vector3 movement = CameraDirection(movementDirection) * speed * Time.deltaTime;
        playerRigidbody.MovePosition(this.transform.position + movement);
    }

    private void TurnThePlayer()
    {
        if(movementDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targeRotation = Quaternion.LookRotation(CameraDirection(movementDirection));
            Quaternion rotation = Quaternion.Slerp(playerRigidbody.rotation, targeRotation, turnSpeed);

            playerRigidbody.MoveRotation(rotation);
        }
    }

    private Vector3 CameraDirection(Vector3 movementDirection)
    {
        var cameraForward = mainCamera.transform.forward.normalized;
        var cameraRight = mainCamera.transform.right.normalized;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        return cameraForward * movementDirection.z + cameraRight * movementDirection.x;
    }
}
