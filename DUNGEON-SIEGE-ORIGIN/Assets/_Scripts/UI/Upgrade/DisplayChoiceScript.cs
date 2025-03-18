using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EChoiceType
{
    NONE,
    TRADE,
    UPGRADE,
    TRADESETUP,
}

public class DisplayChoiceScript : MonoBehaviour
{
    [SerializeField] Image _backgroundImage;

    public string _sceneToLoad;
    Choice _choice;
    EChoiceType _choiceType = EChoiceType.NONE;
    byte newWeaponID = 0;
    public void OnClick()
    {
        switch (_choiceType)
        {
            case EChoiceType.NONE:
                Debug.Log("Something went wrong ...");
                break;
            case EChoiceType.TRADE:
                Trade();
                break;
            case EChoiceType.UPGRADE:
                Upgrade();
                break;
            case EChoiceType.TRADESETUP:
                Debug.Log("TradeSetup");
                transform.GetComponentInParent<ChoiceUIScript>().ChoseTrade();
                break;
            default:
                break;
        }
    }

    public void Upgrade()
    {
        transform.GetComponentInParent<ChoiceUIScript>().SetDoUpgrade(true);
        LoadNextScene();
    }

    public void Trade()
    {
        transform.GetComponentInParent<ChoiceUIScript>().SetNewWeaponID(newWeaponID);
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync(_sceneToLoad);
    }


    public void SetChoiceType(EChoiceType choiceType)
    {
        _choiceType = choiceType;
    }

    public void SetImageSprite(Sprite newSprite)
    {
        _backgroundImage.sprite = newSprite;
    }

    public void SetNewWeaponID(byte _newWeaponID)
    {
        newWeaponID = _newWeaponID;
    }
}
