using UnityEngine;

namespace Coilbound.Scenes.World
{
  public class WorldManager : MonoBehaviour
  {
    public static WorldManager Instance { get; private set; }

    private void Awake()
    {
      if(!Instance) Instance = this;
      else Destroy(this);
    }
  }
}