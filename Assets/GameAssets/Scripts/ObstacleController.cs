// file="ObstacleController.cs" company=""
// Copyright (c) 2023 All Rights Reserved
// Author: Leandro Almeida
// Date: 20/03/2023

#region usings
using Game.Controller.Game;
using UnityEngine;
#endregion

namespace Game.Controller.Obstacle
{
    public class ObstacleController : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private int moveSpeed;
        [SerializeField]
        private int rotationSpeed;
        [SerializeField]
        private float driftRadius;
        [SerializeField]
        private bool moveBetweenWaypoints;
        [SerializeField]
        private bool spinAround;
        [SerializeField]
        private Transform[] waypoints;

        private int currentWaypoint = 0;
        [HideInInspector]
        public bool obstacleMove = false;
        #endregion

        #region Unity3D methods
        private void Update()
        {
            if (GameController.gameState == Helper.Helper.GameState.Play && obstacleMove)
            {
                if (moveBetweenWaypoints)
                {
                    if (Vector3.Distance(waypoints[currentWaypoint].position, transform.position) <= driftRadius)
                    {
                        currentWaypoint++;
                        if (currentWaypoint > waypoints.Length - 1)
                            currentWaypoint = 0;
                    }
                    else
                    {
                        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, Time.deltaTime * moveSpeed);
                    }
                }
                else if (spinAround)
                {
                    transform.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f);
                }
            }
        }
        #endregion

        #region Custom methods
        internal void ResetMovement()
        {
            currentWaypoint = 1;
            obstacleMove = false;

            if (waypoints.Length > 0)
                transform.SetPositionAndRotation(waypoints[0].position, waypoints[0].rotation);
        }
        #endregion
    }
}