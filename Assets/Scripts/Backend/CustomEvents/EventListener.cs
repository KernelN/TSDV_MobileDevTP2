namespace TheWasteland.EventManager
{
    public interface IEventListener
    {
        public void OnEventRaised(object[] data);
    }
}