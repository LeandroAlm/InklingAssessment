// file="PlayerController.cs" company=""
// Copyright (c) 2023 All Rights Reserved
// Author: Leandro Almeida
// Date: 20/03/2023

#region usings
using Game.Controller.Game;
using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
#endregion

namespace Game.Controller.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float moveSpeed = 5f;
        [SerializeField]
        private float jumpForce = 5f;
        [SerializeField]
        private float rotationSpeed = 200f;
        [SerializeField]
        private int groundLayer;
        [SerializeField]
        private int obstacleLayer;

        private Rigidbody rb;
        private bool isGrounded;
        #endregion

        void Start()
        {
            rb = GetComponent<Rigidbody>(); // Get the rigidbody component on start
            isGrounded = true;
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            if (GameController.gameState == Helper.Helper.GameState.Play)
                CheckMovement();
        }

        private void CheckMovement()
        {
            // Move the player forward/backward and left/right
            float horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            transform.Translate(-horizontal, 0f, -vertical);

            // Rotate the player left/right based on mouse movement
            if (Input.GetAxis("Mouse X") >= 0.01f || Input.GetAxis("Mouse X") <= -0.01f)
            {
                float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up * mouseX);
            }

            // Jump mechanic
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == groundLayer)
                isGrounded = true;
        }
    }
}