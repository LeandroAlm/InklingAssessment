// file="UIController.cs" company=""
// Copyright (c) 2023 All Rights Reserved
// Author: Leandro Almeida
// Date: 20/03/2023

#region usings
using Game.Controller.Game;
using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
#endregion

namespace Game.Controller.UI
{
    public class UIController : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private GameObject uiPanel;
        [SerializeField]
        private Transform uiInGamePanel;
        [SerializeField]
        private GameObject loseLabel;
        [SerializeField]
        private GameObject winLabel;
        [SerializeField]
        private GameObject nextLevelBtt;
        [SerializeField]
        private GameObject restartBtt;
        [SerializeField]
        private GameObject resumeBtt;
        [SerializeField]
        private Button[] levelsUI;
        [SerializeField]
        private TextMeshProUGUI levelTimerText;

        private GameController gameController;
        #endregion

        #region Unity3D methods
        void Start()
        {
            gameController = GetComponent<GameController>();
        }

        void Update()
        {

        }
        #endregion

        #region Custom methods
        #region On click methods
        public void OnClickEixt()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void OnClickLevel(int _level)
        {
            uiPanel.SetActive(false);
            gameController.StartLevel(_level);
        }

        public void OnClickEscape()
        {
            GameController.gameState = Helper.Helper.GameState.Menu;
            uiPanel.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void OnClickResume()
        {
            GameController.gameState = Helper.Helper.GameState.Play;
            uiPanel.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void OnClickBackMenu()
        {
            winLabel.SetActive(false);
            loseLabel.SetActive(false);
            levelTimerText.transform.parent.gameObject.SetActive(false);
            gameController.CloseLevel();
        }

        public void OnClickRestart()
        {
            gameController.RestartCurrentLevel();

            uiPanel.SetActive(false);
            winLabel.SetActive(false);
            loseLabel.SetActive(false);

            StartLevel();
        }

        public void OnClickNextLevel()
        {
            nextLevelBtt.SetActive(false);
            winLabel.SetActive(false);
            loseLabel.SetActive(false);
            uiPanel.SetActive(false);

            gameController.StartNextLevel();
        }
        #endregion

        internal void StartLevel()
        {
            levelTimerText.transform.parent.gameObject.SetActive(true);

            for (int i = 0; i < uiInGamePanel.childCount; i++)
            {
                GameObject obj = uiInGamePanel.GetChild(i).gameObject;
                if (obj != nextLevelBtt
                    && obj != restartBtt)
                    obj.SetActive(true);
                else
                    obj.SetActive(false);
            }
        }

        /// <summary>
        /// Activate win label and buttons options
        /// </summary>
        internal void WinAnimation()
        {
            levelTimerText.transform.parent.gameObject.SetActive(false);
            uiPanel.SetActive(true);
            winLabel.SetActive(true);

            if (gameController.NextLevelExists())
                nextLevelBtt.SetActive(true);

            resumeBtt.SetActive(false);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        /// <summary>
        /// Activate lose label and buttons options
        /// </summary>
        internal void LoseAnimation()
        {
            levelTimerText.transform.parent.gameObject.SetActive(false);
            loseLabel.SetActive(true);
            uiPanel.SetActive(true);
            restartBtt.SetActive(true);

            resumeBtt.SetActive(false);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        /// <summary>
        /// handle UI buttons to select level
        /// </summary>
        /// <param name="_currentLevel"></param>
        internal void RefreshLevelUI(int _currentLevel)
        {
            for (int i = 0; i < levelsUI.Length; i++)
            {
                if (i < _currentLevel)
                    levelsUI[i].interactable = true;
                else
                    levelsUI[i].interactable = false;
            }
        }

        /// <summary>
        /// Refresh level timer
        /// </summary>
        internal void RefreshLevelDuration(int _value)
        {
            levelTimerText.text = "LEVEL TIME: " + _value;
        }

        public void InGameMenu(int[] num_1)
        {

        }
        #endregion
    }
}