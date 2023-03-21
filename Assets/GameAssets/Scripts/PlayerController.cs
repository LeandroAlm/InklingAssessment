// file="PlayerController.cs" company=""
// Copyright (c) 2023 All Rights Reserved
// Author: Leandro Almeida
// Date: 20/03/2023

#region usings
using Game.Controller.Game;
using UnityEngine;
#endregion

namespace Game.Controller.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private GameController gameController;
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private float slowSpeed;
        [SerializeField]
        private float jumpForce;
        [SerializeField]
        private float rotationSpeed;
        [SerializeField]
        private int groundLayer;
        [SerializeField]
        private int obstacleLayer;

        private Rigidbody rb;
        private bool isGrounded;
        private float speed = 5f;
        private Quaternion targetRotation;
        #endregion

        #region Unity3D methods
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            targetRotation = transform.rotation;
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isGrounded = true;
        }

        void Update()
        {
            if (GameController.gameState == Helper.Helper.GameState.Play)
                CheckMovement();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == obstacleLayer)
                gameController.LoseCurrentLevel();

            if (collision.gameObject.layer == groundLayer)
                isGrounded = true;
        }
        #endregion

        #region Custom methods
        private void CheckMovement()
        {
            // Adjust speed by ground check
            if (!isGrounded)
                speed = slowSpeed;
            else
                speed = moveSpeed;

            // Move the player forward/backward and left/right
            transform.Translate(-(Input.GetAxis("Horizontal") * speed * Time.deltaTime), 0f, -Input.GetAxis("Vertical") * speed * Time.deltaTime);

            // Smoothly interpolate rotation to target rotation
            targetRotation *= Quaternion.Euler(Vector3.up * Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);

            // Jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }

        /// <summary>
        /// Reset player's position and rotation
        /// </summary>
        internal void ResetPlayerTransform(Transform _waypoint)
        {
            transform.SetPositionAndRotation(_waypoint.position, _waypoint.rotation);
            targetRotation = _waypoint.rotation;
        }
        #endregion
    }
}