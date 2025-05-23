using System;

namespace Coilbound.Contents.Buffs
{
  [Serializable]
  public class BuffInfo
  {
    public BuffPrototype prototype;
    public float duration;
    public int level;

    public event Action onRemove = (() => {});
    
    public BuffInfo(BuffPrototype prototype, float duration, int level)
    {
      this.prototype = prototype;
      this.duration = duration;
      this.level = level;
    }

    public void Remove() => onRemove();
  }
}