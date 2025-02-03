public class EventService
{
    private static EventService instance;
    public static EventService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventService();
            }
            return instance;
        }
    }

    public EventController OnLightSwitchToggled { get; private set; }
    public EventController<int> OnKeyPickedUp { get; private set; }
    public EventController OnLightsOffByGhostEvent { get; private set; }
    public EventController OnObjectFloatingEvent { get; private set; }
    public EventController OnPlayerEscapedEvent { get; private set; }
    public EventController OnPlayerDeathEvent { get; private set; }
    public EventController OnRatRushEvent { get; private set; }
    public EventController OnSkullDrop { get; private set; }
    public EventController<int,int> OnPotionDrinkEvent { get; private set; }

    public EventController OnMasterShadow { get; private set; }
    public EventService()
    {
        OnLightSwitchToggled = new EventController();
        OnKeyPickedUp = new EventController<int>();
        OnPotionDrinkEvent = new EventController<int,int>();
        OnLightsOffByGhostEvent = new EventController();
        OnObjectFloatingEvent = new EventController();
        OnRatRushEvent = new EventController();
        OnSkullDrop = new EventController();
        OnPlayerEscapedEvent = new EventController();
        OnPlayerDeathEvent = new EventController();
        OnMasterShadow = new EventController();
    }
}
