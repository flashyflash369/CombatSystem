using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComboBehaviour : MonoBehaviour
{
    public List<AttackScriptableObject> combo;
    //public InputAction inputAction;

    private float lastClickedTime;
    private float lastComboEnd;
    private int comboCounter;

    public Animator animator;

    private void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        */
        ExitAttack();
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        if(value.started)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if(Time.time - lastComboEnd > 0.2f && comboCounter <= combo.Count)
        {
            CancelInvoke("EndCombo");

            if(Time.time - lastClickedTime >= 0.2f)
            {
                animator.runtimeAnimatorController = combo[comboCounter].animatorOV;
                animator.Play("Attack", 0, 0);
                comboCounter++;
                lastClickedTime = Time.time;

                if(comboCounter > combo.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }

    private void ExitAttack()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", .3f);
        }
    }

    private void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
    }
}
