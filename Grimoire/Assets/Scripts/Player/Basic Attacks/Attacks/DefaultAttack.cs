﻿using UnityEngine;
using System.Collections;

public class DefaultAttack : AbstractAttack {

	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update()
	{
	
	}

	public override void HandleInput (InputHandler _input)
	{
		base.HandleInput( _input );
	}

	public override void HitEnemy(Collider2D _collider)
	{
		base.HitEnemy( _collider );
	}
}