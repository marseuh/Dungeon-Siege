using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CanEditMultipleObjects]
public class SceneDataEditor : EditorWindow
{
    private SceneDataSO _sceneData;

    private bool _fold = true;
    private SerializedProperty _enemiesType;
    private SerializedProperty _enemiesCount;

    [MenuItem("Window/Scene Data Editor %#e")]
    public static void Init()
    {
        GetWindow(typeof(SceneDataEditor));
    }

    private void OnEnable()
    {
        if (EditorPrefs.HasKey("SceneDataPath"))
        {
            string objectPath = EditorPrefs.GetString("SceneDataPath");
            _sceneData = AssetDatabase.LoadAssetAtPath(objectPath, typeof(SceneDataSO)) as SceneDataSO;
            if (_sceneData != null)
            {
                RefreshSerializedProperties();
            }
        }

        EditorSceneManager.sceneSaved += RefreshCurrentSceneData;
    }

    private void OnDisable()
    {
        EditorSceneManager.sceneSaved -= RefreshCurrentSceneData;
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Scene Data Editor", EditorStyles.boldLabel);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create New SceneData"))
        {
            CreateNewSceneData();
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        GUI.enabled = false;
        EditorGUILayout.TextField("Current scene", EditorSceneManager.GetActiveScene().name);
        GUI.enabled = true;

        _sceneData = EditorGUILayout.ObjectField("Selection", _sceneData, typeof(SceneDataSO), false) as SceneDataSO;

        if (_sceneData != null)
        {
            if (GUI.changed)
            {
                RefreshSerializedProperties();
            }

            GUILayout.Space(10);
            _fold = EditorGUILayout.InspectorTitlebar(_fold, _sceneData);
            if (_fold)
            {
                GUI.enabled = false;
                EditorGUILayout.PropertyField(_enemiesType, new GUIContent("Enemies type"));
                EditorGUILayout.PropertyField(_enemiesCount, new GUIContent("Enemies count"));
                GUI.enabled = true;
            }


            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Show in Project"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = _sceneData;
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Refresh data"))
            {
                RefreshCurrentSceneData(EditorSceneManager.GetActiveScene());
            }
            GUILayout.EndHorizontal();

        }
    }

    private void CreateNewSceneData()
    {
        _sceneData = CreateSceneData.Create();
        if (_sceneData)
        {
            string path = AssetDatabase.GetAssetPath(_sceneData);
            EditorPrefs.SetString("SceneDataPath", path);
            RefreshCurrentSceneData(EditorSceneManager.GetActiveScene());
        }
    }

    private void RefreshCurrentSceneData(UnityEngine.SceneManagement.Scene scene)
    {
        if (_sceneData == null)
        {
            return;
        }

        Dictionary<GameObject, int> enemiesCountByType = new();
        foreach (GameObject go in scene.GetRootGameObjects())
        {
            foreach (WaveSpawner spawner in go.GetComponentsInChildren<WaveSpawner>())
            {
                foreach (WaveSpawnerOptions wave in spawner.GetData().WaveOptions)
                {
                    if (wave.DoSpawn)
                    {
                        if (enemiesCountByType.TryGetValue(wave.Prefab, out int count))
                        {
                            enemiesCountByType[wave.Prefab] = ++count;
                        }
                        else
                        {
                            enemiesCountByType.Add(wave.Prefab, 1);
                        }
                    }
                }
            }
        }

        _sceneData.EnemiesType.Clear();
        _sceneData.EnemiesCount.Clear();

        foreach (KeyValuePair<GameObject, int> pair in enemiesCountByType)
        {
            _sceneData.EnemiesType.Add(pair.Key);
            _sceneData.EnemiesCount.Add(pair.Value);
        }
        EditorUtility.SetDirty(_sceneData);

        // Need this otherwise new sceneData display is not updated until we select it.
        Selection.activeObject = _sceneData;
        RefreshSerializedProperties();
    }

    private void RefreshSerializedProperties()
    {
        SerializedObject so = new(_sceneData);
        _enemiesType = so.FindProperty("EnemiesType");
        _enemiesCount = so.FindProperty("EnemiesCount");
    }
}

public class CreateSceneData
{
    public static SceneDataSO Create()
    {
        SceneDataSO newAsset = ScriptableObject.CreateInstance<SceneDataSO>();

        string folder = "Assets/_ScriptableObjects";
        string name = "NewSceneDataSO_";
        string extension = ".asset";

        string[] sameNameAssets = AssetDatabase.FindAssets(name, new[] { folder });

        int index = 0;
        string indexStr = index.ToString();
        foreach (string guid in sameNameAssets)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);

            int diffStart = folder.Length + name.Length + 1;
            int communLength = diffStart + extension.Length;
            string diff = path.Substring(diffStart, path.Length - communLength);

            if (diff != indexStr)
            {
                break;
            }
            ++index;
            indexStr = index.ToString();
        }

        AssetDatabase.CreateAsset(newAsset, folder + "/" + name + indexStr + extension);
        AssetDatabase.SaveAssets();
        return newAsset;
    }
}
