using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceUIScript : MonoBehaviour, IDataPersistence
{
    [SerializeField] GameConfigSO _gameConfigSO;
    [SerializeField] PlayerDataSO _playerDataSO;
    [SerializeField] WeaponDataSO _swordDataSO;
    [SerializeField] GameConfigWeaponUpgradesSO _swordUpgradeSO;
    [SerializeField] WeaponDataSO _wandDataSO;
    [SerializeField] GameConfigWeaponUpgradesSO _wandUpgradeSO;
    [SerializeField] WeaponDataSO _bookDataSO;
    [SerializeField] GameConfigWeaponUpgradesSO _bookUpgradeSO;
    [SerializeField] GameObject _upgradeChoiceHolder;
    [SerializeField] GameObject _twoChoice;
    [SerializeField] List<GameObject> _twoChoiceList = new List<GameObject>();
    [SerializeField] float _fadeTimer;
    [SerializeField] GameObject _backgroundGO;
    [BoxGroup("Listen to")]
    [SerializeField] VoidEventChannelSO _launchLevelTransitionChannel;
    [Scene]
    [SerializeField] string _sceneToLoad;

    [Header("Sprite")]
    [SerializeField] Sprite _tradeSprite;
    [SerializeField] Sprite _upgradeSprite;

    //Constants
    const int NumberOfFirstChoice = 2;
    const int NumberOfTradeChoice = 2;
    const int NumberOfUpgradeChoice = 3;

    private byte newWeaponID = 0;
    private bool upgradeWeapon = false;

    private void OnEnable()
    {
        _launchLevelTransitionChannel.OnEventTrigger += StartLevelTransition;
    }

    private void StartLevelTransition()
    {
        _backgroundGO.SetActive(true);
        StartCoroutine(BackgroundFadeIn());
    }

    private void SetupFirstChoice()
    {
        _twoChoice.SetActive(true);
        for (int i = 0; i < NumberOfFirstChoice; i++)
        {
            DisplayChoiceScript displayChoiceScript = _twoChoiceList[i].GetComponent<DisplayChoiceScript>();
            switch (i)
            {
                case 0:
                    displayChoiceScript.SetChoiceType(EChoiceType.TRADESETUP);
                    displayChoiceScript.SetImageSprite(_tradeSprite);
                    break;
                case 1:
                    displayChoiceScript.SetChoiceType(EChoiceType.UPGRADE);
                    displayChoiceScript.SetImageSprite(_upgradeSprite);
                    displayChoiceScript._sceneToLoad = _sceneToLoad;
                    break;
                default:
                    break;
            }
        }
        StartCoroutine(TwoChoiceFadeIn());
    }

    public void ChoseTrade()
    {
        StartCoroutine(TwoChoiceFadeIn());
        StartCoroutine(TwoChoiceFadeOut());
        for (int i = 0; i < NumberOfTradeChoice; i++)
        {
            DisplayChoiceScript displayChoiceScript = _twoChoiceList[i].GetComponent<DisplayChoiceScript>();
            displayChoiceScript.SetChoiceType(EChoiceType.TRADE);
            displayChoiceScript._sceneToLoad = _sceneToLoad;
        }
        switch (newWeaponID)
        {
            case 3:
                _twoChoiceList[0].GetComponent<DisplayChoiceScript>().SetImageSprite(_bookDataSO.TradeIcon);
                _twoChoiceList[0].GetComponent<DisplayChoiceScript>().SetNewWeaponID(_gameConfigSO.GetId(_bookDataSO));
                _twoChoiceList[1].GetComponent<DisplayChoiceScript>().SetImageSprite(_wandDataSO.TradeIcon);
                _twoChoiceList[1].GetComponent<DisplayChoiceScript>().SetNewWeaponID(_gameConfigSO.GetId(_wandDataSO));
                break;
            case 4:
                _twoChoiceList[0].GetComponent<DisplayChoiceScript>().SetImageSprite(_swordDataSO.TradeIcon);
                _twoChoiceList[0].GetComponent<DisplayChoiceScript>().SetNewWeaponID(_gameConfigSO.GetId(_swordDataSO));
                _twoChoiceList[1].GetComponent<DisplayChoiceScript>().SetImageSprite(_wandDataSO.TradeIcon);
                _twoChoiceList[1].GetComponent<DisplayChoiceScript>().SetNewWeaponID(_gameConfigSO.GetId(_wandDataSO));
                break;
            case 5:
                _twoChoiceList[0].GetComponent<DisplayChoiceScript>().SetImageSprite(_swordDataSO.TradeIcon);
                _twoChoiceList[0].GetComponent<DisplayChoiceScript>().SetNewWeaponID(_gameConfigSO.GetId(_swordDataSO));
                _twoChoiceList[1].GetComponent<DisplayChoiceScript>().SetImageSprite(_bookDataSO.TradeIcon);
                _twoChoiceList[1].GetComponent<DisplayChoiceScript>().SetNewWeaponID(_gameConfigSO.GetId(_bookDataSO));
                break;
            default:
                break;
        }
    }

    private void OnDisable()
    {
        _launchLevelTransitionChannel.OnEventTrigger -= StartLevelTransition;
    }


    private IEnumerator BackgroundFadeIn()
    {
        Image _backgroundImage = _backgroundGO.GetComponent<Image>();
        for (float i = 0; i <= _fadeTimer; i += Time.fixedDeltaTime)
        {
            _backgroundImage.color = new Color(0, 0, 0, i);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        yield return new WaitForEndOfFrame();
        SetupFirstChoice();
    }

    private IEnumerator TwoChoiceFadeIn()
    {
        Image ChoiceOneImage = _twoChoiceList[0].GetComponentInChildren<Image>();
        Image ChoiceTwoImage = _twoChoiceList[1].GetComponentInChildren<Image>();

        for (float i = 0; i <= _fadeTimer / 2; i += Time.fixedDeltaTime)
        {
            ChoiceOneImage.color = new Color(1, 1, 1, i);
            ChoiceTwoImage.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        ChoiceOneImage.color = new Color(1, 1, 1, 1);
        ChoiceTwoImage.color = new Color(1, 1, 1, 1);

        yield return new WaitForEndOfFrame();
    }

    private IEnumerator TwoChoiceFadeOut()
    {
        Image ChoiceOneImage = _twoChoiceList[0].GetComponentInChildren<Image>();
        Image ChoiceTwoImage = _twoChoiceList[1].GetComponentInChildren<Image>();

        for (float i = 0; i >= _fadeTimer / 2; i -= Time.fixedDeltaTime)
        {
            ChoiceOneImage.color = new Color(1, 1, 1, i);
            ChoiceTwoImage.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        ChoiceOneImage.color = new Color(1, 1, 1, 0);
        ChoiceTwoImage.color = new Color(1, 1, 1, 0);

        yield return new WaitForEndOfFrame();
    }

    public void SetNewWeaponID(byte newID)
    {
        newWeaponID = newID;
    }

    public void SetDoUpgrade(bool doUpgrade) 
    {
        upgradeWeapon = doUpgrade;
        WeaponStatisticUgradeSO[] weaponUpgrades;
        _gameConfigSO.TryGetWeaponUpgrades(_gameConfigSO.GetWeapon(newWeaponID), out weaponUpgrades);
        foreach (WeaponStatisticUgradeSO weaponUpgrade in weaponUpgrades)
        {
            _playerDataSO.IncrementUpgrade(_gameConfigSO.GetId(weaponUpgrade));
        }
    }

    public void LoadData(GameData data)
    {
        newWeaponID = data.weaponID;
    }

    public void SaveData(ref GameData data)
    {
        if (upgradeWeapon)
        {
            WeaponStatisticUgradeSO[] weaponUpgrades;
            _gameConfigSO.TryGetWeaponUpgrades(_gameConfigSO.GetWeapon(newWeaponID), out weaponUpgrades);
            foreach (WeaponStatisticUgradeSO weaponUpgrade in weaponUpgrades)
            {
                data.weaponUpgrade[_gameConfigSO.GetId(weaponUpgrade)] += 1 ;
            }
        }
        else
        {
            data.weaponID = newWeaponID;
        }
    }
}
