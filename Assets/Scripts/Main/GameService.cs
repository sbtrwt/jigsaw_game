using JigsawGame.Events;
using JigsawGame.Board;
using UnityEngine;


namespace JigsawGame.Main
{
    public class GameService : MonoBehaviour
    {
        private EventService eventService;
        private BoardService boardSevice;

        private void Start()
        {
            InitializeServices();
            InjectDependencies();
        }

        private void InitializeServices()
        {
            eventService = new EventService();
            boardSevice = new BoardService();
        }

        private void InjectDependencies()
        {
            boardSevice.Init(eventService);
        }
    }
}