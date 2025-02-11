using UnityEngine;
using System.Collections.Generic;

/*========================================================
 * Author: Tyler Remazki
 *
 * Class : Animation Controler
 *
 * Description: Dictates the current animation state of 
 * the actor.
 *
 * Also will provide functionality to swap out Mecanim motions
 * based on the ComponentCollection attached to this particular
 * actor.
 *
 * AnimatorOverrideController <---- Use this to override
 * the animations used by the AnimationController in Mecanim.
 * We're going to need a separate controller per character.
 =========================================================*/

public class AnimationController : MonoBehaviour
{
    Animator m_Animator;
    PhysicsController m_physicsController;
    MovementController m_movementController;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_physicsController = transform.gameObject.GetComponent<PhysicsController>();
        m_movementController = transform.gameObject.GetComponent<MovementController>();
    }

    void Update()
    {
        WalkingAnimations();
        JumpingAnimations();
        AttackAnimations();
    }

	//SEPERATE THSI INTO THE STATE MACHINE 
    void WalkingAnimations()
    {
        m_Animator.SetBool("Moving", m_movementController.IsMoving());
        m_Animator.SetFloat("MovementSpeed", Mathf.Abs(m_physicsController.Velocity.x));
    }

    void JumpingAnimations()
    {
        if (m_movementController.IsJumping())
        {
            m_Animator.SetBool("Jumping", true);
            m_Animator.SetBool("Falling", false);
        }
        if (m_physicsController.Velocity.y < 0)
        {
            m_Animator.SetBool("Jumping", false);
            m_Animator.SetBool("Falling", true);
        }
        if (!m_movementController.IsJumping())
            m_Animator.SetBool("Falling", false);


    }

    void AttackAnimations() {

    }

}