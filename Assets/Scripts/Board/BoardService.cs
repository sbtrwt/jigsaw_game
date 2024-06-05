using JigsawGame.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JigsawGame.Board
{
    public class BoardService
    {
        private EventService eventService;
        public BoardService()
        {

        }

        public void Init(EventService eventService)
        {
            this.eventService = eventService;
        }
    }

}