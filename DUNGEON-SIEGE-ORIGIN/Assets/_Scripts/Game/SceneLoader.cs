using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Scene]
    [SerializeField] private string scene;

    [BoxGroup("Listens to")]
    [SerializeField] private VoidEventChannelSO _loadSceneTriggerChannel;

    private void OnEnable()
    {
        _loadSceneTriggerChannel.OnEventTrigger += LoadScene;
    }

    private void OnDisable()
    {
        _loadSceneTriggerChannel.OnEventTrigger -= LoadScene;
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(scene);
    }
}
