namespace Project.Scripts.Core.Base
{
    public interface ISpawnable
    {
        public int CardMaxHealth { get; }
        public int CardActualHealth { get; set; }
        public abstract void Spawn();
        public abstract void OnSpawn();
        public abstract void OnDie();
    }
}