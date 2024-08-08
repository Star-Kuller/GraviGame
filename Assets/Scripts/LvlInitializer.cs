using Services;
using Services.EventBus;
using Services.Interfaces;
using Services.ServiceLocator;
using UnityEngine;

public class LvlInitializer : MonoBehaviour
{
    public void Awake()
    {
        var services = ServiceLocator.Current;

        Physics2D.gravity = new Vector2(0, 0);
        
        services.TryRegister<IEventBus>(new EventBus());
        services.TryRegister<IFinishSystem>(new FinishSystem());

        if (services.IsRegistered<ITimer>())
            services.Unregister<ITimer>();
        services.Register<ITimer>(new Timer());
        
        services.TryRegister<IHighScoreManager>(new HighScoreManager());
    }
}