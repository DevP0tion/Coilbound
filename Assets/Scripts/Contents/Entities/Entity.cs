using System;
using System.Collections.Generic;
using System.Linq;
using Coilbound.Contents.Buffs;
using Coilbound.Utils;
using UnityEngine;

namespace Coilbound.Contents.Entities
{
  public class Entity : MonoBehaviour
  {
    public bool isLanding;
    public RangedStat health = new(100, 100);
    public RangedStat hungry = new(1000, 100);
    public RangedStat mana = new(1000, 100);
    public Stat<float> speed = new(10);
    [SerializeField] private List<BuffInfo> buffs = new();

    public Rigidbody body;
    public List<StatOperator<int>> healthMultiplier = new();
    public List<StatOperator<int>> hungryMultiplier = new();
    public List<StatOperator<int>> manaMultiplier = new();
    public List<StatOperator<float>> speedMultiplier = new();

    public void AddBuff(BuffInfo info)
    {
      var tempInfo = buffs.Find(v => v.prototype == info.prototype);

      if (tempInfo != null)
      {
        // 버프가 있을 때는 정보 업데이트
        if (info.prototype.renewal)
        {
          // 갱신형 버프일 때는 버프가 있을 때 지속시간을 추가하는 대신 재설정
          // 레벨은 비교하여 더 높은 값으로 설정
          info.level = Math.Max(info.level, info.level);
          info.duration = info.duration;
        }
        else
        {
          // 이외에는 지속시간 추가
          info.level = Math.Max(info.level, info.level);
          info.duration += info.duration;
        }
      }
      else
      {
        // 버프가 없으면 버프 추가
        var newInfo = new BuffInfo(info.prototype, info.duration, info.level);
        buffs.Add(newInfo);

        foreach (var effect in info.prototype.effects) effect.Apply(this, newInfo);
      }
    }

    public void AddBuff(BuffPrototype prototype, float duration, int level = 0)
    {
      AddBuff(new BuffInfo(prototype, duration, level));
    }

    public void RemoveBuff(BuffInfo buff)
    {
      if (buffs.Contains(buff))
      {
        buffs.Remove(buff);
        foreach (var effect in buff.prototype.effects) effect.Remove(this, buff);
        buff.Remove();
      }
    }

    /// <summary>
    ///   버프 데이터의 일관성을 위해 Get을 분리 구현했습니다.
    /// </summary>
    /// <returns></returns>
    public BuffInfo[] GetBuffList()
    {
      return buffs.ToArray();
    }

    #region Unity Events

    private void Awake()
    {
      health.MaxEffect = origin
        => healthMultiplier.Aggregate(origin, (current, multiplier) => current * multiplier(origin));
      hungry.MaxEffect = origin
        => hungryMultiplier.Aggregate(origin, (current, multiplier) => current * multiplier(origin));
      mana.MaxEffect = origin
        => manaMultiplier.Aggregate(origin, (current, multiplier) => current * multiplier(origin));
      speed.effect = origin
        => speedMultiplier.Aggregate(origin, (current, multiplier) => current * multiplier(origin));
    }

    private List<BuffInfo> removes = new();

    private void FixedUpdate()
    {
      foreach (var buff in buffs)
      {
        buff.duration -= Time.deltaTime;
        if (buff.duration < 0) removes.Add(buff);
      }
      
      if(removes.Count > 0)
      {
        foreach (var buff in removes) RemoveBuff(buff);
        removes.Clear();
      }
    }

    private void OnCollisionExit(Collision other)
    {
      if (other.gameObject.CompareTag("Ground")) isLanding = false;
    }

    private void OnCollisionStay(Collision other)
    {
      if (other.gameObject.CompareTag("Ground")) isLanding = true;
    }

    #endregion
  }
}