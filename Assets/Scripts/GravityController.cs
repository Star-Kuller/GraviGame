using System;
using Services.EventBus;
using Services.ServiceLocator;
using UnityEngine;
using UnityEngine.EventSystems;

public class GravityController : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private const float GravityValue = 9.81f;
    private bool _isStarted = false;
    private EventBus _eventBus;
    private void Start()
    {
        var services = ServiceLocator.Current;
        _eventBus = services.Get<EventBus>();
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

        Debug.Log($"Gravity: {Physics2D.gravity}");
    }
    
    public void OnDrag(PointerEventData data)
    {

    }
}

