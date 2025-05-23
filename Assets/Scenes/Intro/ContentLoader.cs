using Coilbound.Contents.Items;
using Coilbound.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Coilbound.Scenes.Intro
{
  public class ContentLoader : MonoBehaviour
  {
    public UnityEvent onLoad;
    public bool loaded;

    [Header("Asset Reference Labels")] [SerializeField]
    private AssetLabelReference itemLabel;

    private static GameManager manager => GameManager.Instance;

    private void Start()
    {
      LoadData();
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space) && loaded) SceneManager.LoadScene("MainMenuScene");
    }

    private void LoadData()
    {
      onLoad.Invoke();

      Addressables.LoadAssetsAsync<ItemData>(itemLabel, item => { manager.items[item.name] = item; })
        .WaitForCompletion();

      loaded = true;
    }
  }
}