using Coilbound.Contents.Items;
using UnityEngine;
using UnityEngine.Events;

namespace Coilbound.Scenes.World
{
  public class PlayerItemCollector : MonoBehaviour
  {
    public UnityEvent<ItemData> onCollect;
    public float collectSpeed = 10f;
    [SerializeField] public Player player;

    #region Unity Events

    /// <summary>
    ///   아이템 가까이 있을 때 가져오기
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
      if (other.CompareTag("Item"))
      {
        var item = other.GetComponent<ItemObject>();
        var target = Vector3.Lerp(other.transform.position, transform.position, Time.deltaTime * collectSpeed);
        item.originalY = target.y;
        item.transform.position = target;

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
          onCollect.Invoke(item.Data);
          item.Release();
          foreach (var buff in item.Data.useEffect)
          {
            player.Character.AddBuff(buff);
          }
        }
      }
    }

    #endregion
  }
}