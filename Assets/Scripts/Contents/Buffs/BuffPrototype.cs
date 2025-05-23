using UnityEngine;
using UnityEngine.Localization;

namespace Coilbound.Contents.Buffs
{
  [CreateAssetMenu(fileName = "new Buff Prototype", menuName = "Coilbound/Buff Prototype")]
  public class BuffPrototype : ScriptableObject
  {
    public LocalizedString description;
    public Sprite icon;
    public bool isPositive = false;
    public bool renewal = false;
    public BuffEffect[] effects;
  }
}