﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator anim;
    private int jumpHash = Animator.StringToHash("Jump");
    private Rigidbody rb;
    private float jumpForce = 290.0f;
    private Quaternion oQuat;


    // Use this for initialization
    private void Start ( )
    {
        anim = GetComponent<Animator> ( );
        rb = GetComponent<Rigidbody> ( );
        oQuat = transform.rotation;
    }

    // Update is called once per frame
    private void Update ( )
    {
        float move = Input.GetAxis("Vertical");
        anim.SetFloat ( "Speed", move );

        float turnSpeed = Input.GetAxis("Horizontal");
        // anim.SetFloat ( "TurnSpeed", turnSpeed );



        // Limit jump to a state.  HACKL3Y: This isn't working.
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if ( Input.GetKeyDown ( KeyCode.Space ) && stateInfo.fullPathHash != jumpHash )
        {
            // Frame left ground / fps / anim speed. HACKL3Y: derive this float. 
            float delay = 10.0f / 24.0f / 0.5f;
            CoHelp.Instance.DoWhen ( delay, delegate { Jump ( ); } );
            anim.SetTrigger ( jumpHash );
        }

        // HACKL3Y: Add raycast ground and transition from jump to root anim.

        // Rotate player about Y axis.  Right = 90 degrees.  Left = 270 degrees. 
        float degrees = 90.0f * -turnSpeed + 90.0f;
        print ( degrees );
        Quaternion nQuat = Quaternion.AngleAxis ( degrees, Vector3.up );
        transform.rotation = oQuat * nQuat;
        

    }

    private void Jump ( )
    {
        rb.AddForce ( Vector3.up * jumpForce );
    }
}