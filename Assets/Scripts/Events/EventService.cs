namespace JigsawGame.Events
{
    public class EventService
    {
        public EventController<int> OnLevelSelected { get; private set; }
        public EventController<bool> OnGameOver { get; private set; }
        public EventService()
        {
            OnLevelSelected = new EventController<int>();
            OnGameOver = new EventController<bool>();
        }

    }
}
