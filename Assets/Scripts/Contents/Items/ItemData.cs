using Coilbound.Contents.Buffs;
using UnityEngine;
using UnityEngine.Localization;

namespace Coilbound.Contents.Items
{
  [CreateAssetMenu(fileName = "new ItemData", menuName = "Coilbound/ItemData")]
  public class ItemData : ScriptableObject
  {
    public Mesh mesh;
    public Material material;
    public Vector3 center, size;
    public LocalizedString localeName, localeDescription;
    public Sprite sprite;

    public BuffInfo[] useEffect;
  }
}