﻿using UnityEngine;
using System.Collections;

public class SpawnLocation : AbstractSpawn {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void ActivateSpell()
    {
        throw new System.NotImplementedException();
    }

    public override void UseIncantation(Force.ForceType _force)
    {
        throw new System.NotImplementedException();
    }

    public override void ReleaseIncantation(Force.ForceType _force)
    {
        throw new System.NotImplementedException();
    }
}
