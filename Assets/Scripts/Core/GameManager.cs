using Coilbound.Contents.Items;
using UnityEngine;

namespace Coilbound.Core
{
  [AddComponentMenu("Coilbound/GameManger")]
  public class GameManager : MonoBehaviour
  {
    public SoundManager sounds;
    public SerializableDictionary<string, ItemData> items = new();
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
      if (Instance == null)
      {
        Instance = this;
        DontDestroyOnLoad(gameObject);
      }
      else
      {
        Destroy(gameObject);
      }
    }
  }
}