using JigsawGame.Events;
using UnityEngine;


namespace JigsawGame.Main
{
    public class GameService : MonoBehaviour
    {
        private EventService eventService;

        private void Start()
        {
            InitializeServices();
            InjectDependencies();
        }

        private void InitializeServices()
        {
            eventService = new EventService();
        
        }

        private void InjectDependencies()
        {
        }
    }
}