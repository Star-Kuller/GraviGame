using System;
using Services.EventBus;
using Services.ServiceLocator;
using UnityEngine;

public class MenuShower : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject victory;
    [SerializeField] private GameObject loose;
    [SerializeField] private GameObject pause;
    private void Start()
    {
        var services = ServiceLocator.Current;
        var eventBus = services.Get<EventBus>();
        
        eventBus.Subscribe(EventList.Victory, ShowVictory);
        eventBus.Subscribe(EventList.Pause, ShowPause);
        eventBus.Subscribe(EventList.Resume, HiddenPause);
        eventBus.Subscribe(EventList.Loose, ShowLoose);
    }

    private void ShowVictory()
    {
        Debug.Log("Victory");
        background.SetActive(true);
        victory.SetActive(true);
    }
    private void ShowPause()
    {
        background.SetActive(true);
        pause.SetActive(true);
    }

    private void HiddenPause()
    {
        background.SetActive(false);
        pause.SetActive(false);
    }
    private void ShowLoose()
    {
        background.SetActive(true);
        loose.SetActive(true);
    }
}