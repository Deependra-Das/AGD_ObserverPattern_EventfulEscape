﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Responsible for Player and Camera Movement based on User Input.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    //Public Variables:
    [Header("Adjustments:")] 
    public bool showCursor; 
    public float jumpForce = 5f;
    public float raycastLength;
    
    [Header("Player Movement Speeds:")] 
    [Range(0.01f,10.0f)] public float sensitivity = 5f;
    [Range(0.01f, 32.0f)] public float walkSpeed = 0.50f, sprintSpeed = 0.85f;
    
    // Private fields:
    private Rigidbody m_rigidbody;
    private Camera m_camera;
    private const float rotationLimit = 0.5f;

    //Properties:
    private float Velocity { get => Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed; }
    private float HorizontalAxis { get => Input.GetAxis("Horizontal"); }
    private float VerticalAxis { get => Input.GetAxis("Vertical"); }
    private bool IsGrounded { get => Physics.Raycast(transform.position, -transform.up, raycastLength); }

    // Actions:
    public static Action OnPlayerInteracted;
    public static int s_KeysEquipped = 0;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_camera = gameObject.GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        Cursor.visible = showCursor;

        //Rigidbody movement and rotation.
        m_rigidbody.MoveRotation(m_rigidbody.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * sensitivity, 0)));
        m_rigidbody.MovePosition(transform.position + Time.fixedDeltaTime * Velocity * (transform.forward * VerticalAxis + transform.right * HorizontalAxis));

        //Camera rotation.
        float velocity = sensitivity * -Input.GetAxis("Mouse Y");
        m_camera.transform.Rotate(velocity, 0f, 0f);
        float rotationX = m_camera.transform.localRotation.x;

        //Cancel out any rotational velocity if the player tries to rotate camera beyond Rotation Limit.
        if (rotationX > rotationLimit || rotationX < -rotationLimit) { m_camera.transform.Rotate(-velocity, 0, 0); }

        //Jump:
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded) { m_rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); }

        if (Input.GetKeyDown(KeyCode.E))
            OnPlayerInteracted?.Invoke();
    }
}