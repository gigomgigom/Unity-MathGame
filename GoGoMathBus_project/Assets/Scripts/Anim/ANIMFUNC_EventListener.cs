using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ANIMFUNC_EventListener : MonoBehaviour
{
    [SerializeField] UnityEvent[] events;

    public void OnEvent(int index)
    {
        events[index].Invoke();
    }
}
