using Coilbound.Contents.Entities;
using UnityEngine;

namespace Coilbound.Contents.Buffs
{
  // 버프의 효과를 정의하는 코드
  // 인스턴스 재사용에 주의하며 구현할 것
  public abstract class BuffEffect : ScriptableObject
  {
    // 엔티티에 버프가 적용됬을 때
    public abstract void Apply(Entity entity, BuffInfo info);

    // 엔티티에 버프가 추가된 상태에서 업데이트될 때
    public abstract void UpdateBuff(Entity entity, BuffInfo info);

    // 엔티티에서 버프가 제거될 때
    public abstract void Remove(Entity entity, BuffInfo info);
  }
}