using System;
using UnityEngine;
using UnityEngine.UI;

namespace Coilbound.Utils.UI
{
  public class UIGaugeBar : MonoBehaviour
  {
    public float max;

    [SerializeField] [GetSet("Value")] private float value;
    [SerializeField] private RectTransform rect;
    [SerializeField] private Image image;

#if UNITY_EDITOR
    [SerializeField] [GetSet("Color")] private Color color;
#endif

    public float Value
    {
      get => value;
      set
      {
        this.value = value < 0 ? 0 : Math.Min(value, max);
        rect.localScale = new Vector3(this.value / max, 1, 1);
      }
    }

    public Color Color
    {
      get => image.color;
      set => image.color = value;
    }

    public void ChangeMax(float maxValue)
    {
      max = maxValue;
      Value = Value;
    }
  }
}