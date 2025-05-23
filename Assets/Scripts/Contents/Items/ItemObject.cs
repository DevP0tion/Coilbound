using Coilbound.Core;
using UnityEngine;

namespace Coilbound.Contents.Items
{
  public class ItemObject : MonoBehaviour
  {
    [SerializeField] private ItemData data;
    public float speed;
    public float amplitude = 0.2f;
    public float originalY;

    [Header("Components")] [SerializeField]
    private MeshFilter filter;

    [SerializeField] private new MeshRenderer renderer;
    [SerializeField] private new BoxCollider collider;
    [SerializeField] private bool isPooling = true; 
    public ItemData Data => data;

    public void SetData(ItemData originData)
    {
      data = originData;
      filter.mesh = data.mesh;
      renderer.material = data.material;
      collider.size = data.size;
      collider.center = data.center;
    }

    public void Release()
    {
      if(isPooling) ItemManager.Instance.Pool.Release(this);
      else Destroy(gameObject);
    }

    #region Unity Events

    public void OnEnable()
    {
      originalY = transform.position.y;
    }

    private void FixedUpdate()
    {
      transform.position = new Vector3(transform.position.x, originalY + Mathf.Sin(Time.time * speed) * amplitude,
        transform.position.z);
      transform.Rotate(Vector3.up, 30 * Time.deltaTime * speed);
    }

    #endregion
  }
}