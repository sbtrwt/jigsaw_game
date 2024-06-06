using JigsawGame.Events;
using JigsawGame.Board;
using UnityEngine;
using JigsawGame.UI;

namespace JigsawGame.Main
{
    public class GameService : MonoBehaviour
    {
        private EventService eventService;
        private BoardService boardSevice;

        [SerializeField] private BoardSO boardSO;
        [SerializeField] private UIService uIService;
        [SerializeField] private Transform boardContainer;
        private void Start()
        {
            InitializeServices();
            InjectDependencies();
        }

        private void InitializeServices()
        {
            eventService = new EventService();
            boardSevice = new BoardService(boardSO, boardContainer);
        }

        private void InjectDependencies()
        {
            boardSevice.Init(eventService);
            uIService.Init(eventService);
        }
    }
}