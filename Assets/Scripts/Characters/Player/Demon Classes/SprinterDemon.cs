using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("Player/Demon Classes/Sprinter")]
public class SprinterDemon : BaseDemon {
    
    // Use this for initialization
    protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}

    protected override void activateSkill()
    {
    }

    protected override void cancelSkill()
    {
    }

    protected override void moveSkill(float i_HorizontalInput, float i_VerticalInput)
    {
    }
}
