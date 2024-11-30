using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EventManager : SingletonMonobehaviour<EventManager>
{
    public event Action SpawnMonsterEvent;
    public event Action GameOverEvent;

    protected override void Awake()
    {
        base.Awake();
    }

    public void CallSpawnMonsterEvent()
    {
       SpawnMonsterEvent?.Invoke();
    }

    public void CallGameOverEvent()
    {
        GameOverEvent?.Invoke();
    }

}
