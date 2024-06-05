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

        private void Start()
        {
            startButton.onClick.AddListener(OnClickStartButton);
            SetActiveStartPanel(true);
        }
        public void Init(EventService eventService)
        {
            this.eventService = eventService;
        }

        private void OnClickStartButton ()
        {
            eventService.OnGameStart.InvokeEvent(1);
            SetActiveStartPanel(false);
        }
        private void SetActiveStartPanel(bool isVisible) => gameStartPanel.SetActive(isVisible);
    }

}