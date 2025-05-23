using UnityEngine;
using UnityEngine.Events;

namespace Coilbound.Utils
{
  public class EditorMessageSender : MonoBehaviour
  {
    public bool useEvent;
    public UnityEvent onMessage;
    public string eventName;
  }
}