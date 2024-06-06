using JigsawGame.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JigsawGame.UI
{
    public class UIService : MonoBehaviour
    {
        private EventService eventService;

        [Header("Start Panel")]
        [SerializeField] private GameObject gameStartPanel;
        [SerializeField] private Button startButton;

        [Header("Game Panel")]
        [SerializeField] private GameObject gameMenuPanel;
        [SerializeField] private Button suffleButton;

        [Header("Gameover Panel")]
        [SerializeField] private GameObject gameoverPanel;
        [SerializeField] private Button restartButton;
        private void Start()
        {
            startButton.onClick.AddListener(OnClickStartButton);
            suffleButton.onClick.AddListener(OnClickSuffleButton);
            restartButton.onClick.AddListener(OnClickRestartButton);
            SetActiveStartPanel(true);
        }
        public void Init(EventService eventService)
        {
            this.eventService = eventService;
            SubscribeEvents();
        }
        public void SubscribeEvents()
        {
            eventService.OnGameOver.AddListener(OnGameOver);
        }
        private void OnClickStartButton ()
        {
            eventService.OnGameStart.InvokeEvent(1);
            SetActiveStartPanel(false);
            SetActiveGameMenuPanel(true);
        }
        private void SetActiveStartPanel(bool isVisible) => gameStartPanel.SetActive(isVisible);
        private void SetActiveGameMenuPanel(bool isVisible) => gameMenuPanel.SetActive(isVisible);
        private void SetActiveGameoverPanel(bool isVisible) => gameoverPanel.SetActive(isVisible);
        private void OnClickSuffleButton()
        {
            eventService.OnTileSuffle.InvokeEvent(true);
           
        }

        private void OnClickRestartButton()
        {
            eventService.OnGameStart.InvokeEvent(1);
            SetActiveGameoverPanel(false);
            SetActiveGameMenuPanel(true);
        }

        private void OnGameOver(bool isGameOver)
        {
            if (isGameOver)
            {
                SetActiveGameoverPanel(true);
            }
        }
    }

}