// file="GameController.cs" company=""
// Copyright (c) 2023 All Rights Reserved
// Author: Leandro Almeida
// Date: 20/03/2023

#region usings
using Game.Controller.Level;
using Game.Controller.UI;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using static Game.Helper.Helper;
#endregion

namespace Game.Controller.Game
{
    public class GameController : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private GameObject player;
        [SerializeField]
        private GameObject[] levels;
        [SerializeField]
        private GameObject countdown;

        private UIController uiController;
        private int currentLevel;
        private LevelController currentLevelController;

        public static GameState gameState;
        #endregion

        #region Unity3D methods
        private void Awake()
        {
            for (int i = 0; i < levels.Length; i++)
            {
                levels[i].SetActive(false);
            }  
        }

        void Start()
        {
            uiController = GetComponent<UIController>();

            currentLevel = 1;
            uiController.RefreshLevelUI(currentLevel);

            countdown.SetActive(false);

            gameState = GameState.Menu;
        }

        void Update()
        {
            if (gameState == GameState.Play)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    uiController.OnClickEscape();
                }
            }
        }
        #endregion

        #region Custom methods
        /// <summary>
        /// Starts a level by a giving level id
        /// </summary>
        internal void StartLevel(int _level)
        {
            player.SetActive(true);

            levels[_level -1].SetActive(true);

            currentLevelController = levels[_level - 1].GetComponent<LevelController>();

            StartCoroutine(CountdownAnimation());
        }

        private IEnumerator CountdownAnimation()
        {
            countdown.SetActive(true);

            TextMeshProUGUI label = countdown.GetComponentInChildren<TextMeshProUGUI>();

            for (int i = 3; i > 0; i--)
            {
                label.text = i.ToString();
                yield return new WaitForSeconds(1.0f);
            }

            gameState = GameState.Play;
            countdown.SetActive(false);
            currentLevelController.TriggerTimeAndObstacles();
        }
        #endregion
    }
}