namespace JigsawGame.Events
{
    public class EventService
    {
        public EventController<int> OnGameStart { get; private set; }
        public EventController<bool> OnGameOver { get; private set; }
        public EventService()
        {
            OnGameStart = new EventController<int>();
            OnGameOver = new EventController<bool>();
        }

    }
}
