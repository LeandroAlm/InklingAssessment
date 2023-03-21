// file="LevelController.cs" company=""
// Copyright (c) 2023 All Rights Reserved
// Author: Leandro Almeida
// Date: 20/03/2023

#region usings
using Game.Controller.Game;
using Game.Controller.Obstacle;
using Game.Controller.Player;
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
        [SerializeField]
        private GameController gameController;

        private bool levelStart = false;
        private float timer;
        #endregion

        #region Unity3D methods
        void Start()
        {

        }

        void Update()
        {
            if (levelStart && GameController.gameState == Helper.Helper.GameState.Play)
            {
                timer -= Time.deltaTime;

                uiController.RefreshLevelDuration((int)timer + 1);

                if (timer <= 0.0f)
                {
                    // Ended level
                    gameController.WinCurrentLevel();
                }
            }
        }
        #endregion

        #region Custom methods
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

        /// <summary>
        /// Resets the level to default state
        /// </summary>
        internal void ResetLevel()
        {
            // reset all obstacles
            foreach (ObstacleController obstacle in obstacles)
                obstacle.ResetMovement();

            levelStart = false;

            player.GetComponent<PlayerController>().ResetPlayerTransform(playerWaypoint);

            uiController.RefreshLevelDuration((int)duration);
        }
        #endregion
    }
}