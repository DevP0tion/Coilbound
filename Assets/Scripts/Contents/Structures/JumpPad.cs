using Coilbound.Contents.Entities;
using UnityEngine;

namespace Coilbound.Contents.Structures
{
  public class JumpPad : MonoBehaviour
  {
    public float jumpHeight = 100f;

    private void OnCollisionEnter(Collision other)
    {
      if (other.gameObject.CompareTag("Player"))
      {
        var player = other.gameObject.GetComponent<Entity>();

        player.body.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
      }
    }
  }
}