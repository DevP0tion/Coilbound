using System;
using UnityEngine;
using UnityEngine.Events;

namespace Coilbound.Utils
{
  public delegate T StatOperator<T>(T origin);

  [Serializable]
  public class Stat<T>
  {
    public UnityEvent<T> onChanged;
    [SerializeField] protected T baseValue;
    public StatOperator<T> effect;

    public Stat(T baseValue, StatOperator<T> effect = null)
    {
      this.baseValue = baseValue;
      this.effect = effect;
    }

    public virtual T BaseValue
    {
      get => baseValue;
      set
      {
        var prevValue = baseValue;
        baseValue = value;
        onChanged?.Invoke(prevValue);
      }
    }

    public virtual T Value => effect != null ? effect(baseValue) : baseValue;

    public static implicit operator T(Stat<T> stat)
    {
      return stat.Value;
    }
  }
}