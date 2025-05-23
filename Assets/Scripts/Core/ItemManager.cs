using Coilbound.Contents.Items;
using UnityEngine;
using UnityEngine.Pool;

namespace Coilbound.Core
{
  public class ItemManager : MonoBehaviour
  {
    [Header("Pooling")] [SerializeField] private GameObject itemPrefab;

    [SerializeField] private Transform releasedContainer;
    public static ItemManager Instance { get; private set; }
    public ObjectPool<ItemObject> Pool { get; private set; }

    private void Awake()
    {
      Instance = this;

      Pool = new ObjectPool<ItemObject>(() =>
      {
        // create
        var obj = Instantiate(itemPrefab, releasedContainer);
        var item = obj.GetComponent<ItemObject>();
        item.transform.position = Vector3.zero;

        return item;
      }, item =>
      {
        // get
        item.gameObject.SetActive(true);
        item.transform.position = Vector3.zero;
        item.gameObject.transform.SetParent(transform);
      }, item =>
      {
        // release
        item.gameObject.SetActive(false);
        item.gameObject.transform.SetParent(releasedContainer);
      });
    }

    public ItemObject SpawnItem(ItemData data, Vector3 position)
    {
      var item = Pool.Get();
      item.SetData(data);
      item.transform.position = position;
      return item;
    }
  }
}