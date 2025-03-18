using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class DebugCommandMenuEditor : EditorWindow
{
    private VoidEventChannelSO _completeLevelChannel;
    private VoidEventChannelSO _launchLevelTransitionChannel;
    private GameObject _player;

    private bool _playerHealthEnabled = true;
    private bool _playerDamagesEnabled = true;

    [MenuItem("Window/Debug Command Menu")]
    public static void Init()
    {
        GetWindow(typeof(DebugCommandMenuEditor));
    }

    private void OnEnable()
    {
        _completeLevelChannel = AssetDatabase.LoadAssetAtPath("Assets/_ScriptableObjects/Events/Level/LevelCompletedChannel.asset", typeof(VoidEventChannelSO)) as VoidEventChannelSO;
        _launchLevelTransitionChannel = AssetDatabase.LoadAssetAtPath("Assets/_ScriptableObjects/Events/Level/LaunchLevelTransitionChannel.asset", typeof(VoidEventChannelSO)) as VoidEventChannelSO;
        _player = GameObject.FindGameObjectWithTag("Player");

        EditorApplication.playModeStateChanged += ResetCommands;
    }

    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= ResetCommands;

    }

    private void ResetCommands(PlayModeStateChange state)
    {
        _playerHealthEnabled = true;
        _playerDamagesEnabled = true;
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Debug Command Menu", EditorStyles.boldLabel);
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Complete level"))
        {
            LaunchVoidEventCommand(_completeLevelChannel);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Launch level transition"))
        {
            LaunchVoidEventCommand(_launchLevelTransitionChannel);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Kill all enemies"))
        {
            KillAllEnemies();
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Toggle player invulnerability"))
        {
            TogglePlayerInvulnerability();
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Toggle player damages"))
        {
            TogglePlayerDamages();
        }
        GUILayout.EndHorizontal();
    }

    private void KillAllEnemies()
    {
        int nbEnemiesKilled = 0;
        foreach (GameObject go in EditorSceneManager.GetActiveScene().GetRootGameObjects())
        {
            foreach (AIBaseController aiController in go.GetComponentsInChildren<AIBaseController>())
            {
                ICharacterHealth aiHealth = aiController.gameObject.GetComponent<ICharacterHealth>();
                if (aiHealth != null)
                {
                    aiHealth.Die();
                    ++nbEnemiesKilled;
                }
            }
        }
        Debug.Log("Command Kill all enemies : " + nbEnemiesKilled + " enemies killed");
    }

    private void TogglePlayerInvulnerability()
    {
        if (_player != null)
        {
            CharacterHealth playerHealth = _player.GetComponent<CharacterHealth>();
            if (playerHealth != null)
            {
                _playerHealthEnabled = !_playerHealthEnabled;
                playerHealth.enabled = _playerHealthEnabled;
            }
        }
    }

    // For enemies invulnerability : for now it is enough but it would be better to disable enemies health on spawn
    private void TogglePlayerDamages()
    {
        if (_player != null)
        {
            WeaponUser playerWeaponUser = _player.GetComponent<WeaponUser>();
            if (playerWeaponUser != null)
            {
                _playerDamagesEnabled = !_playerDamagesEnabled;
                if (_playerDamagesEnabled)
                {
                    playerWeaponUser.ResetDamages();
                }
                else
                {
                    playerWeaponUser.Damages = 0;
                }
            }
        }
    }

    private void LaunchVoidEventCommand(VoidEventChannelSO eventToLaunch)
    {
        if (eventToLaunch != null)
        {
            eventToLaunch.RequestRaiseEvent();
        }
    }
}
