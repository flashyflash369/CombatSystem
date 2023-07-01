using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Sub behaviours")]
    public PlayerMovementBehaviour playerMovementBehaviour;
    public PlayerAnimationBehaviour playerAnimationBehaviour;

    [Header("Input Settings")]
    public PlayerInput playerInput;
    public float movementSmoothingSpeed = 1f;
    private Vector3 rawInputMovement;
    private Vector3 smoothInpuMovement;

    private void Awake()
    {
        playerAnimationBehaviour.SetupBehaviour();
    }
    
    private void Update()
    {
        CalculateMovementInputSmoothing();
        UpdatePlayerMovement();
        UpdatePlayerAnimation();
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0f, inputMovement.y);
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        if(value.started)
        {
            Debug.Log("Attack");
        }
    }

    private void CalculateMovementInputSmoothing()
    {
        smoothInpuMovement = Vector3.Lerp(smoothInpuMovement, rawInputMovement, movementSmoothingSpeed * Time.deltaTime);
    }

    private void UpdatePlayerMovement()
    {
        playerMovementBehaviour.UpdateMovementData(smoothInpuMovement);
    }

    private void UpdatePlayerAnimation()
    {
        playerAnimationBehaviour.SetMovementAnimation(smoothInpuMovement.magnitude);
    }


}
