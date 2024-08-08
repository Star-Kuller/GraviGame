using Services.ServiceLocator;

namespace Services.Interfaces
{
    public interface IHighScoreManager : IService
    {
        public float HighScore { get; }
        public void Save(float time);
        public float Get(int levelIndex);
    }
}