using Services;
using Services.EventBus;
using Services.ServiceLocator;
using UnityEngine;

public class LvlInitializer : MonoBehaviour
{
    public void Awake()
    {
        var services = ServiceLocator.Current;

        Physics2D.gravity = new Vector2(0, 0);
        
        services.TryRegister(new EventBus());
        services.TryRegister(new FinishSystem());

        if (services.IsRegistered<Timer>())
            services.Unregister<Timer>();
        
        services.Register(new Timer());
        
        services.TryRegister(new HighScoreManager());
    }
}