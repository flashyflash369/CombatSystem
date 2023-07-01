using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationBehaviour : MonoBehaviour
{
    public Animator animator;

    int moveId;

    
    public void SetupBehaviour()
    {
        moveId = Animator.StringToHash("Move");
    }

    public void SetMovementAnimation(float speed)
    {
        animator.SetFloat(moveId, speed);
    }
}
