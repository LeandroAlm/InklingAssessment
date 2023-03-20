// file="UIController.cs" company=""
// Copyright (c) 2023 All Rights Reserved
// Author: Leandro Almeida
// Date: 20/03/2023

#region usings
using Game.Controller.Game;
using System;
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
        private GameObject menuOptionsPanel;
        [SerializeField]
        private GameObject menuLevelOptionsPanel;
        [SerializeField]
        private GameObject gameOptionsPanel;
        [SerializeField]
        private Button[] levelsUI;

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
            menuOptionsPanel.SetActive(false);
            menuLevelOptionsPanel.SetActive(false);
            gameOptionsPanel.SetActive(true);
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
            menuOptionsPanel.SetActive(true);
            menuLevelOptionsPanel.SetActive(false);
            gameOptionsPanel.SetActive(false);

            // Close level
        }
        #endregion

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

        internal void RefreshLevelDuration(int _value)
        {
            
        }
        #endregion
    }
}