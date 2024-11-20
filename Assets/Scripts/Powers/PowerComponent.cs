namespace TheWasteland.Gameplay.Powers
{
    public interface PowerComponent
    {
        public void Set(PowerData data);
        public void Update(float dt);

        public void Cast();
    }
}