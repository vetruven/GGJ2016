using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("Player/Player Movement")]
public class PlayerMovement : CharacterMovement
{
    // Update is called once per frame
    protected override void Update()
    {
        InputUpdate();

        base.Update();
    }

    private void InputUpdate()
    {
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
