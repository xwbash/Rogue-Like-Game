namespace Rune.Scripts.Interfaces
{
    public interface ISpawner
    {
        public void Spawn();
        public void DeSpawn(IPoolableObject poolableObject);
    }
}