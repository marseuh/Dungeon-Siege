using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [BoxGroup("Listen To")]
    [SerializeField] private FloatSenderEventChannelSO _putTimerValue;
    [BoxGroup("Listens to")]
    [SerializeField] private VoidEventChannelSO _playerDeathChannel;
    [BoxGroup("Listen to")]
    [SerializeField] VoidEventChannelSO _launchLevelTransitionChannel;

    [SerializeField] private TextMeshProUGUI _timerTextField;
    [SerializeField] private TextMeshProUGUI _waveCounterTextField;
    [SerializeField] private GameObject _wholeTimerUI;
    [SerializeField] private GameObject _waveOnlyUI;

    private float _timeBeforeNextWave;
    private Coroutine _runningCoroutine;
    private float _currentWave;

    private void OnEnable()
    {
        _putTimerValue.OnEventTrigger += StartTimerCoroutine;
        _playerDeathChannel.OnEventTrigger += HideUI;
        _launchLevelTransitionChannel.OnEventTrigger += HideUI;
    }

    private void OnDisable()
    {
        _putTimerValue.OnEventTrigger -= StartTimerCoroutine;
        _playerDeathChannel.OnEventTrigger -= HideUI;
        _launchLevelTransitionChannel.OnEventTrigger -= HideUI;
    }

    private void StartTimerCoroutine(float timeBeforeNextWave)
    {
        if(_runningCoroutine != null )
            StopTimerCoroutine();

        _timeBeforeNextWave = timeBeforeNextWave;
        _runningCoroutine = StartCoroutine(TimerBeforeNextWave());
    }

    private void StopTimerCoroutine()
    {
        StopCoroutine(_runningCoroutine);
    }

    private IEnumerator TimerBeforeNextWave()
    {
        _currentWave += 1;
        while (_timeBeforeNextWave >= 0)
        {
            _timerTextField.text = _timeBeforeNextWave.ToString();
            yield return new WaitForSeconds(1f);
            _timeBeforeNextWave -= 1f;
        }
    }

    private void Start()
    {
        _waveOnlyUI.SetActive(false);
        _currentWave = 0;
        _waveCounterTextField.text = _currentWave.ToString();
        _timerTextField.text = "10";
    }

    private void Update()
    {
        _waveCounterTextField.text = _currentWave.ToString();

        if (_currentWave == 4 && _timerTextField.text == "0")
        {
            _currentWave += 1;
            _wholeTimerUI.SetActive(false);
            _timerTextField.canvasRenderer.SetAlpha(0f);
            _waveOnlyUI.SetActive(true);
        }
    }

    private void HideUI()
    {
        _waveOnlyUI.SetActive(false);
        _wholeTimerUI.SetActive(false);
        _timerTextField.canvasRenderer.SetAlpha(0f);
        _waveCounterTextField.canvasRenderer.SetAlpha(0f);
    }
}
