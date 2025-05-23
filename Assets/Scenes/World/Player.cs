using Cinemachine;
using Coilbound.Contents.Items;
using Coilbound.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Coilbound.Scenes.World
{
  /// <summary>
  ///   물리처리 관련 구현
  /// </summary>
  public partial class Player : MonoBehaviour
  {
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Jump = Animator.StringToHash("Jump");

    [Header("Status")] 
    public Vector3 direction = Vector3.zero;
    public Stat<float> Speed => character.speed;
    public float jumpForce = 400f;
    [SerializeField] private PlayerFocusMode focusMode = PlayerFocusMode.ThirdPerson;
    [SerializeField] private ItemObject focusedItemObject;
    public ItemData FocusedItem => focusedItemObject?.Data;

    [Header("Components")] 
    [SerializeField] private Rigidbody body;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerItemCollector itemCollector;
    [SerializeField] private PlayerInventory inventory;

    [Header("Camera")] 
    [SerializeField] private new Camera camera;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineComponentBase beforeTransposer;
    [SerializeField] private CinemachinePOV pov;
    
    [Header("Dialog")]
    [SerializeField] private GameObject inventoryDialog;
    [SerializeField] private TMP_Text itemTitle, itemDescription;
    public bool IsLanding => character.isLanding;

    /// <summary>
    ///   ThirdPerson 일시 3인칭 뷰 모드
    ///   FirstPerson 일시 1인칭 뷰 모드
    /// </summary>
    public PlayerFocusMode FocusMode
    {
      get => focusMode;
      set
      {
        focusMode = value;
        if (beforeTransposer) DestroyImmediate(beforeTransposer);

        switch (value)
        {
          case PlayerFocusMode.FirstPerson:
            pov.m_VerticalAxis.m_MinValue = -70;
            beforeTransposer = virtualCamera.AddCinemachineComponent<CinemachineHardLockToTarget>();

            camera.cullingMask &= ~LayerMask.GetMask("Player");
            break;

          case PlayerFocusMode.ThirdPerson:
            pov.m_VerticalAxis.m_MinValue = 0;
            beforeTransposer = virtualCamera.AddCinemachineComponent<CinemachineFramingTransposer>();

            camera.cullingMask |= LayerMask.GetMask("Player");
            break;
        }
      }
    }

    #region Unity Events

    private void Awake()
    {
      pov = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
      beforeTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
      FocusMode = PlayerFocusMode.ThirdPerson;
      InitPlayerEntity();
    }

    private void Update()
    {
      // 1인칭일시 마우스 위치에 따라 플레이어가 바라보는 방향 변경
      // 머리 방향도 바꾸고 싶은데 애니메이션 자체에 바인딩이 되어있어서 어려움 ...ㅠ
      if (FocusMode == PlayerFocusMode.FirstPerson)
        characterTransform.localEulerAngles = new Vector3(0, camera.transform.localEulerAngles.y, 0);

      if (Input.GetKeyDown(KeyCode.F5)) FocusMode = FocusMode.Next();
    }

    private void FixedUpdate()
    {
      body.AddForce(characterTransform.TransformDirection(direction) * Speed,
        ForceMode.Impulse);

      if (direction != Vector3.zero && FocusMode == PlayerFocusMode.ThirdPerson)
      {
        // TODO 180 기준으로 더 가까운 쪽으로 회전하게 구현하기
        var rotation = Mathf.Lerp(
          characterTransform.localEulerAngles.y,
          camera.transform.localEulerAngles.y,
          Time.deltaTime * 5);

        characterTransform.localEulerAngles = new Vector3(0, rotation, 0);
      }
      
      // 포커싱된 아이템 체크
      var changed = false;
      if (focusMode == PlayerFocusMode.FirstPerson)
      {
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out var hit))
        {
          if (hit.collider.CompareTag("Item"))
          {
            var item = hit.collider.GetComponent<ItemObject>();
            changed = true;

            if (item != focusedItemObject)
            {
              inventoryDialog.SetActive(true);
              itemTitle.text = item.Data.localeName.GetLocalizedString();
              itemDescription.text = item.Data.localeDescription.GetLocalizedString();
              focusedItemObject = item;
            }
          } 
        }
      } 

      if (!changed & focusedItemObject)
      {
        inventoryDialog.SetActive(false);
        focusedItemObject = null;
      }
    }

    #endregion

    #region InputSystems

    /// <summary>
    ///   움직일 방향을 선택하고, 애니메이터에서 달리는 애니메이션을 설정할지 선택함
    /// </summary>
    /// <param name="value"></param>
    private void OnMove(InputValue value)
    {
      var val = value.Get<Vector2>();
      direction = new Vector3(val.x, 0, val.y);
      animator.SetBool(Run, direction != Vector3.zero);
    }

    private void OnJump()
    {
      var info = animator.GetCurrentAnimatorStateInfo(0);
      if (info.IsName("Unity_Chan_G_Jump") || !IsLanding) return;

      animator.SetTrigger(Jump);
      body.AddForce(Vector3.up * jumpForce, ForceMode.Acceleration);
    }

    private void OnClick()
    {

    }

    #endregion
  }

  public enum PlayerFocusMode
  {
    FirstPerson = 1,
    ThirdPerson = 2
  }
}