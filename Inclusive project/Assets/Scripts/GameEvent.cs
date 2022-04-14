/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AAAstdio.InclusiveProject
{
    [CreateAssetMenu(menuName = "VicPiña/GameEvent")]
    public class GameEvent : MonoBehaviour
    {
        public readonly List<GameEventListener> eventListeners = new List<GameEventListener>();
        public void Raise()
        {
            foreach(GameEventListener geLstnr in eventListeners)
            {
                geLstnr.OnEventRaised();
            }
        }
        public void RegisterListener(GameEventListener sender)
        {
            if (!eventListeners.Contains(sender))
            {
                eventListeners.Add(sender);
            }
        }
        public void UnregisterListener(GameEventListener sender)
        {
            if (eventListeners.Contains(sender))
            {
                eventListeners.Remove(sender);
            }
        }
    }
}
*/