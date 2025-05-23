#if UNITY_EDITOR

using Coilbound.Contents.Items;
using UnityEditor;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.Localization.Tables;

public class ItemDataCreator : EditorWindow
{
  private const string Path = "Assets/Data/Items";
  private Vector3 center, size;

  private string id;
  private Material material;
  private Mesh mesh;
  private GameObject prevPrefab;
  private Sprite sprite;

  private void OnGUI()
  {
    var prefab = (GameObject)EditorGUILayout.ObjectField("Base Object", prevPrefab, typeof(GameObject), false);

    if (prevPrefab != prefab && prefab)
    {
      id = prefab.name;

      if (prefab.TryGetComponent<MeshRenderer>(out var renderer))
        material = renderer.sharedMaterial;

      if (prefab.TryGetComponent<MeshFilter>(out var filter))
        mesh = filter.sharedMesh;

      if (prefab.TryGetComponent<BoxCollider>(out var collider))
      {
        center = collider.center;
        size = collider.size;
      }
    }

    id = EditorGUILayout.TextField("Item id", id);
    mesh = (Mesh)EditorGUILayout.ObjectField("Mesh", mesh, typeof(Mesh), false);
    material = (Material)EditorGUILayout.ObjectField("Material", material, typeof(Material), false);
    center = EditorGUILayout.Vector3Field("Center", center);
    size = EditorGUILayout.Vector3Field("Size", size);
    sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", sprite, typeof(Sprite), false);

    if (GUILayout.Button("Create"))
    {
      var data = CreateInstance<ItemData>();
      data.mesh = mesh;
      data.material = material;
      data.center = center;
      data.size = size;
      data.sprite = sprite;

      var savePath = EditorUtility.SaveFilePanel("export item data", Path, "Item_" + id, "asset");

      if (!string.IsNullOrEmpty(savePath))
      {
        data.localeName = new LocalizedString("ItemNames", id);
        data.localeDescription = new LocalizedString("ItemDescriptions", id);

        AssetDatabase.CreateAsset(data, "Assets" + savePath[Application.dataPath.Length..]);
        AssetDatabase.SaveAssets();
      }
    }

    prevPrefab = prefab;
  }

  [MenuItem("Window/Coilbound/Item Data Creator")]
  private static void Init()
  {
    var window = GetWindow<ItemDataCreator>();
    window.titleContent = new GUIContent("Item Data Creator");
    window.Show();
  }
}

#endif