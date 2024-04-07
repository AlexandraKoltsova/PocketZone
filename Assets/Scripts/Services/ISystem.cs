namespace Services
{
    public interface ISystem
    {
        public virtual void InitSystem()
        {
        }

        public virtual void StartSystem()
        {
        }
        
        public virtual void Tick(float deltaTime)
        {
        }
    }
}