using Coilbound.Contents.Entities;
using Coilbound.Utils.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Coilbound.Scenes.World
{
  /// <summary>
  ///   플레이어 - 게임 시스템 연동 간 구현
  /// </summary>
  public partial class Player
  {
    [Header("Player Entity")]
    [SerializeField] private Entity character;
    [SerializeField] private UIGaugeBar healthBar;
    [SerializeField] private UIGaugeBar hungryBar;
    [SerializeField] private UIGaugeBar manaBar;

    private UnityAction<int> healthAction, hungryAction, manaAction;
    public Entity Character => character;
    private Transform characterTransform => character?.transform;

    private void InitPlayerEntity()
    {
      if (character == null) return;
      SetCharacter(character);
    }

    public void SetCharacter(Entity entity)
    {
      if (entity == null) return;
      if (healthAction != null) character.health.onChanged.RemoveListener(healthAction);
      if (hungryAction != null) character.hungry.onChanged.RemoveListener(hungryAction);
      if (manaAction != null) character.mana.onChanged.RemoveListener(manaAction);

      character = entity;
      entity.transform.SetParent(transform);

      healthAction = _ => { healthBar.Value = entity.health.Value; };
      hungryAction = _ => { hungryBar.Value = entity.hungry.Value; };
      manaAction = _ => { manaBar.Value = entity.mana.Value; };

      entity.health.onChanged.AddListener(healthAction);
      entity.hungry.onChanged.AddListener(hungryAction);
      entity.mana.onChanged.AddListener(manaAction);
    }
  }
}