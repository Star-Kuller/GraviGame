using System;
using Services.EventBus;
using Services.Interfaces;
using Services.ServiceLocator;
using UnityEngine;
using UnityEngine.EventSystems;

public class GravityController : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private const float GravityValue = 9.81f;
    private bool _isStarted = false;
    private IEventBus _eventBus;
    private void Start()
    {
        var services = ServiceLocator.Current;
        _eventBus = services.Get<IEventBus>();
    }

    public void OnBeginDrag(PointerEventData data)
    {
        if (_isStarted == false)
        {
            _eventBus.CallEvent(EventList.Start);
            _isStarted = true;
        }
        if(Mathf.Abs(data.delta.x) > Mathf.Abs(data.delta.y))
        {
            Physics2D.gravity = data.delta.x > 0 ?
                new Vector2(GravityValue, 0) : // right
                new Vector2(-GravityValue, 0); // left
        }
        else
        {
            Physics2D.gravity = data.delta.y > 0 ?
                new Vector2(0, GravityValue) : // up
                new Vector2(0, -GravityValue); // down
        }
    }
    
    public void OnDrag(PointerEventData data)
    {

    }
}

