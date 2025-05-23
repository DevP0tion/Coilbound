#if UNITY_EDITOR

using Coilbound.Utils;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EditorMessageSender))]
public class EditorMessageSenderEditor : Editor
{
  private EditorMessageSender target => target;

  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();

    if (GUILayout.Button("Send Message"))
    {
      if (target.useEvent)
        target.onMessage.Invoke();
      else target.SendMessage(target.eventName);
    }
  }
}

#endif