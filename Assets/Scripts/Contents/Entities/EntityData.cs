using UnityEngine;

namespace Coilbound.Contents.Entities
{
  [CreateAssetMenu(fileName = "new Entity Data", menuName = "Coilbound/Entity Data", order = 0)]
  public class EntityData : ScriptableObject
  {
    public GameObject prefab;
  }
}