using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Services;
using Services.EventBus;
using Services.Interfaces;
using Services.ServiceLocator;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public bool IsFinished { get; private set; }
    
    [SerializeField] private int needForFinish;
    [SerializeField] private List<Collider2D> required = new List<Collider2D>();
    
    private readonly List<Collider2D> _colliders = new List<Collider2D>();
    private IEventBus _eventBus;
    private void Start()
    {
        var services = ServiceLocator.Current;
        _eventBus = services.Get<IEventBus>();
        services.Get<IFinishSystem>().AddFinish(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        _colliders.Add(other);
        
        var allRequiredContains = required.Count(r => _colliders.Contains(r)) == required.Count;
        
        IsFinished = (_colliders.Count >= needForFinish) && allRequiredContains;

        StartCoroutine(CallEventCoroutine());
    }
    
    private IEnumerator CallEventCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        _eventBus.CallEvent(EventList.Finished);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        _colliders.Remove(other);
        
        IsFinished = _colliders.Count >= needForFinish;
        
        Debug.Log("call EventList.Unfinished");
        _eventBus.CallEvent(EventList.Unfinished);
    }
}
