using Cinemachine;
using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class ObjectivePresenter : MonoBehaviour
{
    // For now ObjectivePresenter will only handle moves from player to objectives
    // [MAYBE] For a more sofisticated behaviour, we should take any vcam currently focused via an event that sends it to us
    [SerializeField] private GameObject _playerVCam;
    [SerializeField] private GameObject _objectiveVCam;
    [SerializeField] private CinemachineBrain _brain;

    [SerializeField] private CinemachineBlendDefinition.Style _moveInBlend;
    [SerializeField] private float _moveInDuration;

    [SerializeField] private CinemachineBlendDefinition.Style _moveOutBlend;
    [SerializeField] private float _moveOutDuration;

    [SerializeField] private float _showDuration;

    [BoxGroup("Listens to")]
    [SerializeField] private VoidEventChannelSO _objectiveCompletedChannel;

    private Coroutine _runningCoroutine;

    private void OnEnable()
    {
        _objectiveCompletedChannel.OnEventTrigger += LaunchShowObjectiveCoroutine;
    }

    private void OnDisable()
    {
        _objectiveCompletedChannel.OnEventTrigger -= LaunchShowObjectiveCoroutine;
    }

    private void LaunchShowObjectiveCoroutine()
    {
        if (_runningCoroutine == null)
        {
            _runningCoroutine = StartCoroutine(ShowObjective());
        }
    }

    private IEnumerator ShowObjective()
    {
        Debug.Log(name + " : Show Objective started");
        _brain.m_DefaultBlend = new CinemachineBlendDefinition(_moveInBlend, _moveInDuration);
        _objectiveVCam.SetActive(true);
        _playerVCam.SetActive(false);

        yield return new WaitForSeconds(_moveInDuration);
        yield return new WaitForSeconds(_showDuration);

        _brain.m_DefaultBlend = new CinemachineBlendDefinition(_moveOutBlend, _moveOutDuration);
        _objectiveVCam.SetActive(false);
        _playerVCam.SetActive(true);

        yield return new WaitForSeconds(_moveOutDuration);
        _runningCoroutine = null;
        Debug.Log(name + " : Show Objective ended");
    }
}
