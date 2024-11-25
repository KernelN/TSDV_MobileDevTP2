namespace TheWasteland.EventManager
{
    public interface EventListener
    {
        public void OnEventRaised(object[] data);
    }
}