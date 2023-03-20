// file="LevelController.cs" company=""
// Copyright (c) 2023 All Rights Reserved
// Author: Leandro Almeida
// Date: 20/03/2023

#region usings
using Game.Controller.Obstacle;
using Game.Controller.UI;
using UnityEngine;
#endregion

namespace Game.Controller.Level
{
    public class LevelController : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float duration;

        [SerializeField]
        private Transform player;
        [SerializeField]
        private Transform playerWaypoint;
        [SerializeField]
        private ObstacleController[] obstacles;
        [SerializeField]
        private UIController uiController;

        private bool levelStart = false;
        private float timer;
        #endregion

        #region Unity3D methods
        void Start()
        {

        }

        private void OnEnable()
        {
            player = playerWaypoint;
        }

        void Update()
        {
            if (levelStart)
            {
                timer -= Time.deltaTime;

                uiController.RefreshLevelDuration((int)timer + 1);

                if (timer <= 0.0f)
                {
                    // Ended level
                }
            }
        }

        /// <summary>
        /// Triggers the level obstacles after a countdown
        /// </summary>
        public void TriggerTimeAndObstacles()
        {
            levelStart = true;
            timer = duration;

            foreach (ObstacleController obstacle in obstacles)
                obstacle.obstacleMove = true;
        }
        #endregion
    }
}