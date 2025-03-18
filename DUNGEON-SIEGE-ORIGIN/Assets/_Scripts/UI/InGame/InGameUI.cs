using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{

    [SerializeField] private VoidEventChannelSO _playerDiedEventChannel;
    [SerializeField] private GameObject _deathScreen;
    [SerializeField] private GameObject _deathScreenBG;
    [SerializeField] private GameObject _buttonPause;
    [SerializeField] float _fadeTimer;

    /* Setting the event for the player's death */
    private void OnEnable()
    {
        _playerDiedEventChannel.OnEventTrigger += ShowDeathScreen;
        HideDeathScreen();
    }

    private void OnDisable()
    {
        _playerDiedEventChannel.OnEventTrigger -= ShowDeathScreen;
    }

    /* Manage Death Screen in the UI*/
    private void ShowDeathScreen()
    {
        _deathScreen.SetActive(true);
        StartCoroutine(DeathScreenFadeIn());
    }

    public void HideDeathScreen()
    {
        _deathScreen.SetActive(false);
    }


    /* Behavior when clicking pause */
    public void Pause()
    {
        
    }

    private IEnumerator DeathScreenFadeIn()
    {
        Image _image = _deathScreenBG.GetComponent<Image>();
        for (float i = 0; i <= _fadeTimer; i += Time.fixedDeltaTime)
        {
            _image.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        yield return new WaitForEndOfFrame();
    }

}
