using JigsawGame.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JigsawGame.Board
{
    public class BoardService
    {
        private EventService eventService;
        private BoardSO boardSO;
        public BoardService(BoardSO boardSO)
        {
            this.boardSO = boardSO;
        }

        public void Init(EventService eventService)
        {
            this.eventService = eventService;
            SubscriveEvents();
        }

        private void SubscriveEvents()
        {
            eventService.OnGameStart.AddListener(OnGameStart);
        }
        public void OnGameStart(int level)
        {

        }
    }

}