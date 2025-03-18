using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WeaponChoiceUI : MonoBehaviour, IDataPersistence
{

    public MenuUI menu;
    [SerializeField] private Image weaponIcon;

    [SerializeField] private GameConfigSO _gameConfigSO;
    [SerializeField] private WeaponDataSO swordData;
    [SerializeField] private WeaponDataSO spellbookData;
    [SerializeField] private WeaponDataSO wandData;

    [SerializeField] private TextMeshProUGUI swordAD;
    [SerializeField] private TextMeshProUGUI swordAS;
    [SerializeField] private TextMeshProUGUI swordAR;

    [SerializeField] private TextMeshProUGUI spellbookAD;
    [SerializeField] private TextMeshProUGUI spellbookAS;
    [SerializeField] private TextMeshProUGUI spellbookAR;

    [SerializeField] private TextMeshProUGUI wandAD;
    [SerializeField] private TextMeshProUGUI wandAS;
    [SerializeField] private TextMeshProUGUI wandAR;

    [SerializeField] public GameObject titleGO;
    [SerializeField] public GameObject buttonbackGO;
    [SerializeField] public GameObject weapon1GO;
    [SerializeField] public GameObject weapon2GO;
    [SerializeField] public GameObject weapon3GO;


    private byte newWeaponID = 0;

    // Start is called before the first frame update
    void Start()
    {
    
        swordAD.text = swordData.Damages.ToString();
        swordAS.text = swordData.AttackSpeed.ToString();
        swordAR.text = swordData.Range.ToString();

        spellbookAD.text = spellbookData.Damages.ToString();
        spellbookAS.text = spellbookData.AttackSpeed.ToString();
        spellbookAS.text = spellbookData.Range.ToString();

        wandAD.text = wandData.Damages.ToString();
        wandAS.text = wandData.AttackSpeed.ToString();
        wandAR.text = wandData.Range.ToString();

        ChangeIcon();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeWeapon(WeaponDataSO _weaponData)
    {
        newWeaponID = _gameConfigSO.GetId(_weaponData);
        ChangeIcon();
        /* To link here */
        menu.WeaponsBack();
    }

    private void ChangeIcon()
    {
        weaponIcon.sprite = _gameConfigSO.GetWeapon(newWeaponID).UISprite;
    }

    public void LoadData(GameData data)
    {
        newWeaponID = data.weaponID;
        ChangeIcon();
    }
    
    public void SaveData(ref GameData data)
    {
        data.weaponID = newWeaponID;
        data.characterID = (byte)(newWeaponID - _gameConfigSO.BaseWeapons.Count);
    }
}
