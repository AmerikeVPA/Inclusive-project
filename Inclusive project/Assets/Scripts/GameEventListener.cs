using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AAAstdio.InclusiveProject
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent _event;
        public UnityEvent _response;
        public void OnEnable()
        {
            _event.RegisterListener(this);
        }
        public void OnDisable()
        {
            _event.UnregisterListener(this);
        }
        public void OnEventRaised()
        {
            _response.Invoke();
        }
    }
}
