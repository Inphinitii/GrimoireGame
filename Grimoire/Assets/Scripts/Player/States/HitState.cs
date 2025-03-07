﻿using UnityEngine;
using System.Collections;

namespace PlayerStates
{
	//TODO MAKE THE EXIT STATES
	//TODO LANDING STATE
	public class HitState : IState
	{
		private float				m_oldDampening;
		private Vector2		m_leftStick;

		private const float MOVEMENT_DAMPENER			= 0.35f;
		private const float GROUND_ACCEL_DAMPENER	= 0.93f;
		private const float SMOKE_THRESHOLD					= 10.0f;
		private const float BLOCK_TIME								= 0.25f;
		private const float MIN_X_VEL_EXIT_VALUE				= 0.25f;


		public HitState()
		{
		}

		public override void OnSwitch()
		{
			m_oldDampening = GetFSM().GetActorReference().GetMovementController().groundDampeningConstant;

			GetFSM().BlockStateSwitch( BLOCK_TIME );
			GetFSM().GetActorReference().GetAnimator().SetBool( "Hit", true );
			GetFSM().GetActorReference().GetMovementController().m_capAcceleration = false;
			GetFSM().GetActorReference().GetMovementController().groundDampeningConstant = GROUND_ACCEL_DAMPENER;
			GetFSM().StartChildCoroutine( Flash(Color.white) );

		}

        public override void OnPop()
        {
            OnExit();
        }
		public override void OnExit()
		{
			GetFSM().GetActorReference().GetAnimator().SetBool( "Hit", false );
			GetFSM().GetActorReference().GetMovementController().m_capAcceleration = true;
			GetFSM().GetActorReference().GetMovementController().groundDampeningConstant = m_oldDampening;
            GetFSM().GetActorReference().GetParticleManager().SetSmokeHitParticle( false );
		}

		public override void ExecuteState()
		{
			m_leftStick = GetFSM().GetInput().LeftStick();

			if ( GetFSM().GetInput().Triggers().thisFrame > 0.0f )
				if ( GetFSM().GetActorReference().GetSpellCharges().UseCharge() )
				{
					GetFSM().GetPhysics().ClearValues();
					GetFSM().SetCurrentState( PlayerFSM.States.CANCEL, true );
				}

			if ( GetFSM().GetActorReference().GetPhysicsController().Velocity.y > 0.0f )
				GetFSM().GetActorReference().GetMovementController().SetJumping( true );

			if(GetFSM().GetMovement().IsJumping())
				if ( m_leftStick.x != 0.0f )
					GetFSM().GetActorReference().GetMovementController().MoveX( m_leftStick * MOVEMENT_DAMPENER );

			DisplayParticles();
		}

		public override void ExitConditions()
		{
			m_leftStick = GetFSM().GetInput().LeftStick();

			//At the peak of the velocity upwards..
			if ( GetFSM().GetActorReference().GetPhysicsController().LastVelocity.y > 0.0f && GetFSM().GetActorReference().GetPhysicsController().Velocity.y < 0.0f )
                GetFSM().ReleaseStack();


			//Velocity approaches zero..
            if ( Mathf.Abs( GetFSM().GetActorReference().GetPhysicsController().Velocity.x ) <= MIN_X_VEL_EXIT_VALUE && !GetFSM().GetMovement().IsJumping() )
                GetFSM().ReleaseStack();

		}

		private void DisplayParticles()
		{
			if ( Mathf.Abs( GetFSM().GetActorReference().GetPhysicsController().Velocity.magnitude ) > SMOKE_THRESHOLD )
				GetFSM().GetActorReference().GetParticleManager().SetSmokeHitParticle( true );
			else
				GetFSM().GetActorReference().GetParticleManager().SetSmokeHitParticle( false );
		}

		//Currently debugging. Maybe turn this into something interesting and visually appealing later. 
		private IEnumerator Flash(Color _color)
		{
			GetFSM().GetActorReference().GetRenderer().material.color = _color;
			yield return new WaitForSeconds( 0.25f );
			GetFSM().GetActorReference().GetRenderer().material.color = GetFSM().GetActorReference().actorColor;

		}
	}
}
