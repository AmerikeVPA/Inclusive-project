using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AAAstdio.InclusiveProject
{
    public class EventEditor : Editor
    {
        [CustomEditor(typeof(GameEvent))]
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUI.enabled = Application.isPlaying;

            GameEvent gE = target as GameEvent;
            if (GUILayout.Button("Raise"))
            {
                gE.Raise();
            }
        }
    }
}
