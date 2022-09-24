using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCameraTransform : MonoBehaviour
{
    // GameObjects ////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////

    public Camera camera;

    // Constants //////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////

    // Mouse-based camera rotation speed 
    private static float lookSpeed = 5f;

    // Q / E Roll Speed
    private float rollSpeed = 0.025f;

    // WASD / SPC CTRL Translation Speed
    private float translationSpeed = 0.6f;

    // Lerping Smooth Speed
    private float smoothSpeed = 0.025f;

    // Variables //////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////

    // The current target position 
    private Vector3 targetPosition;

    // The current intermediary Lerp Position
    private Vector3 smoothPosition;

    // Current Target rotation
    public Quaternion targetRotation;

    // The current intermediary Lerp rotation
    private Quaternion smoothRotation;

    private bool isLeftRolling = false;
    private Vector3 rollSpeedVec3;



    // Start //////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////
    void Start()
    {
        // Align camera and creator.
        camera.transform.forward = transform.forward;

        // Set target position
        targetPosition = new Vector3(3f, 3f, 3f);
        //targetRotation = transform.rotation;
        targetRotation = Quaternion.Euler(25, 0, 0);
        //Cursor.visible = false;

        rollSpeedVec3 = new Vector3(rollSpeed, 0, 0);

    }


    public void OnMakeCameraRoll()
    {
        isLeftRolling = true;
        //Debug.Log("In OnRollLeftPerformed method");

        //Quaternion deltaRotation = Quaternion.Euler(lookSpeed, lookSpeed, 0);
        //deltaRotation *= Quaternion.Euler(0, 0, rollSpeed);

        //// Set target rotation
        //targetRotation *= deltaRotation;

        //// Lerp toward target position at all times.
        //smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        //transform.position = smoothPosition;

        //// Lerp toward target rotation at all times.
        //smoothRotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothSpeed);
        //transform.rotation = smoothRotation;
    }


    // Update /////////////////////////////////////////////////////////////////
    // Detect Maneuvers //////////////////////////////////////////////////////
    void Update()
    {
        // Perform Rotations ////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////

        // Rotate Camera based on XY input
        //Vector2 delta = Vector2.zero;
        //delta.y += Input.GetAxis("Mouse X");
        //delta.x -= Input.GetAxis("Mouse Y");
        Quaternion deltaRotation = Quaternion.Euler(0, 0, 0);
        Vector3 deltaPosition = new Vector3(0, 0, 0);

        //// Roll camera based on QE input
        //if (isLeftRolling)
        //{ 
        //    deltaRotation *= Quaternion.Euler(rollSpeed, 0, 0);

        //    // Set target rotation
        //    targetRotation *= deltaRotation;

        //    if (targetRotation.eulerAngles.x > deltaRotation.eulerAngles.x)
        //    {
        //        isLeftRolling = false;
        //    }
        //}

        //if (Input.GetKey(KeyCode.E))
        //{ deltaRotation *= Quaternion.Euler(0, 0, -rollSpeed); }



        // Perform Translations /////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////

        // Move towards object
        if (isLeftRolling)
        {
            //deltaRotation *= Quaternion.Euler(rollSpeed, 0, 0);

            // Set target rotation
            //targetRotation *= deltaRotation;

            // Lerp toward target rotation at all times.
            smoothRotation = Quaternion.Lerp(transform.rotation, targetRotation, rollSpeed);
            transform.rotation = smoothRotation;

            // Lerp toward target position at all times.
            smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = smoothPosition;

            if (targetPosition.z > 3)
            {
                isLeftRolling = false;
            }
        }



        //if (Input.GetKey(KeyCode.A))
        //{ targetPosition -= transform.right * translationSpeed; }
        //if (Input.GetKey(KeyCode.S))
        //{ targetPosition -= transform.forward * translationSpeed; }
        //if (Input.GetKey(KeyCode.D))
        //{ targetPosition += transform.right * translationSpeed; }

        //// SPC CTRL
        //if (Input.GetKey(KeyCode.Space))
        //{ targetPosition += transform.up * translationSpeed; }
        //if (Input.GetKey(KeyCode.LeftControl))
        //{ targetPosition -= transform.up * translationSpeed; }
    }

    // Late Update ////////////////////////////////////////////////////////////
    // Lerp toward target position and rotation //////////////////////////////
    void LateUpdate()
    {
        //// Lerp toward target position at all times.
        //smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        //transform.position = smoothPosition;

        //// Lerp toward target rotation at all times.
        //smoothRotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothSpeed);
        //transform.rotation = smoothRotation;
    }
}