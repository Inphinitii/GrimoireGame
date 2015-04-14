﻿using System.Collections;
using UnityEngine;

/*========================================================
 * Author: Tyler Remazki
 *
 * Class : Page
 *
 * Description Similar to BasicAttack.cs but houses the four available attakcs that a Page can have.
 * Used inside of the Grimoire.cs MonoBehaviour. 
 =========================================================*/

public class Page : MonoBehaviour
{
	public enum Type
	{
		STANDING_NEUTRAL,
		STANDING_DIRECTIONAL,
		AIR_NEUTRAL,
		AIR_DIRECTIONAL
	};

	public Properties.ForceType forceType;
	public GameObject				parent;
	public bool							useActorForce;
	public float							chargeRefresh;

	public AbstractAttack standingNeutral;
	public AbstractAttack standingDirectional;
	public AbstractAttack airNeutral;
	public AbstractAttack airDirectional;

	private AbstractAttack[]	m_attacks;

	/// <summary>
	/// Initialize the page. Used instead of the default MonoBehaviour Start() to ensure that things
	/// are created in a particular order.
	/// </summary>
	public virtual void Init()
	{
		m_attacks = new AbstractAttack[4];
		m_attacks[0] = standingNeutral;
		m_attacks[1] = standingDirectional;
		m_attacks[2] = airNeutral;
		m_attacks[3] = airDirectional;

		if(useActorForce)
			forceType = parent.GetComponent<Actor>().forceType;

		AbstractAttack _tempAtk;
		for ( int i = 0; i < m_attacks.Length; i++ )
		{
			_tempAtk									= (AbstractAttack)Instantiate( m_attacks[i], this.transform.position, Quaternion.identity ) as AbstractAttack;
			_tempAtk.transform.parent		= parent.transform;
			_tempAtk.forceType					= forceType;
			m_attacks[i]							= _tempAtk;
		}
	}

	/// <summary>
	/// Use the page and return the AbstractAttack to the PlayerFSM.
	/// </summary>
	/// <param name="_type">Type of attack.</param>
	/// <returns>AbstractAttack Reference.</returns>
	public virtual AbstractAttack UsePage( Type _type )
	{
		OnPageUse();
		return m_attacks[(int)_type];
	}

	/// <summary>
	/// Called when the page is used.
	/// </summary>
    public virtual void OnPageUse()
    {

    }
}
