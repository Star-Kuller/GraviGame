using Services.ServiceLocator;

namespace Services.Interfaces
{
    public interface IFinishSystem : IService
    {
        public void AddFinish(Finish finish);
    }
}