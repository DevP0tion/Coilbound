using Coilbound.Contents.Entities;
using Coilbound.Utils;
using UnityEngine;

namespace Coilbound.Contents.Buffs.Effects
{
  [CreateAssetMenu(fileName = "new ChangeStat Buff Effect", menuName = "Coilbound/Buff effect/Change Stat")]
  public class ChangeStatBuffEffect : BuffEffect
  {
    public StatType type;
    public int value;
    public int levelMultiplier;

    /// <summary>
    ///   엔티티에 버프가 적용됬을 때 이벤트
    ///   깔끔하게 구현하기 위해 Remove 메소드 대신 info에 참조시킴
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="info"></param>
    public override void Apply(Entity entity, BuffInfo info)
    {
      switch (type)
      {
        case StatType.Health:
        {
          StatOperator<int> effect = origin => origin + value + levelMultiplier * info.level;
          entity.healthMultiplier.Add(effect);
          info.onRemove += () => entity.healthMultiplier.Remove(effect);
          break;
        }

        case StatType.Hungry:
        {
          StatOperator<int> effect = origin => origin + value + levelMultiplier * info.level;
          entity.hungryMultiplier.Add(effect);
          info.onRemove += () => entity.hungryMultiplier.Remove(effect);
          break;
        }

        case StatType.Mana:
        {
          StatOperator<int> effect = origin => origin + value + levelMultiplier * info.level;
          entity.manaMultiplier.Add(effect);
          info.onRemove += () => entity.manaMultiplier.Remove(effect);
          break;
        }

        case StatType.Speed:
        {
          StatOperator<float> effect = origin => origin + value + levelMultiplier * info.level;
          entity.speedMultiplier.Add(effect);
          info.onRemove += () => entity.speedMultiplier.Remove(effect);
          break;
        }
      }

      // 이후 object 참조를 remove에서 사용할 수 있게 매핑하기
    }

    /// <summary>
    ///   TODO 엔티티에 버프가 걸려있는 중일 때 업데이트 이벤트
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="info"></param>
    public override void UpdateBuff(Entity entity, BuffInfo info)
    {
    }

    /// <summary>
    ///   엔티티에서 버프가 해제될 때 이벤트
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="info"></param>
    public override void Remove(Entity entity, BuffInfo info)
    {
    }
  }

  public enum StatType
  {
    Health,
    Hungry,
    Mana,
    Speed
  }
}