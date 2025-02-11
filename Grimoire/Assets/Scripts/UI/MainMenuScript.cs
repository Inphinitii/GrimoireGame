﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using GamepadInput;

namespace UI
{

	public class MainMenuScript : MonoBehaviour
	{

		private Button[] m_buttonList;
		private Button m_currentSelection;
		private EventSystem m_eventSystem;
		private int m_selectionIndex;
		private float m_currentTime;


		private const float SCROLL_DELAY = 0.25f;

		// Use this for initialization
		void Start()
		{
			m_currentTime = 0.0f;
			m_selectionIndex = 0;
			m_buttonList = GetComponentsInChildren<Button>() ;
			m_currentSelection = m_buttonList[m_selectionIndex];
			m_eventSystem = transform.GetComponentInChildren<EventSystem>();
			m_eventSystem.SetSelectedGameObject( m_currentSelection.gameObject );
		}

		// Update is called once per frame
		void Update()
		{
			//Hacky fix to compensate for Unity not liking the gamepad input system.
			Vector2 input = GamePad.GetAxis( GamepadInput.GamePad.Axis.LeftStick, 0, false );
			if ( input.y != 0 && m_currentTime <= 0.0f )
			{
				m_currentTime = SCROLL_DELAY;
				Scroll( input.y );
			}
			else if ( m_currentTime > 0.0f )
				m_currentTime -= Time.deltaTime;
		}

		void Scroll( float _upDown )
		{
			if ( _upDown < 0 )
				m_selectionIndex++;
			else
				m_selectionIndex--;

			if ( m_selectionIndex >= m_buttonList.Length ) m_selectionIndex = 0;
			else if ( m_selectionIndex < 0 ) m_selectionIndex = m_buttonList.Length - 2;

			m_currentSelection = m_buttonList[m_selectionIndex];
			m_eventSystem.SetSelectedGameObject( m_currentSelection.gameObject );
		}

		public void Test()
		{
			Debug.Log( "Ding" );
		}
	}

}