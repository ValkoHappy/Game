using Lean.Localization;
using TMPro;
using UnityEngine;

public class BuildingCharacteristics : MonoBehaviour
{
    [SerializeField] private HealthContainer _healthContainer;
    [SerializeField] private ShootTurret _shootTurret;
    [SerializeField] private GeneratorMining _extraction;
    [SerializeField] private Goods _goods;

    [SerializeField] private TMP_Text _labelText;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private GameObject _damageIcon;
    [SerializeField] private TMP_Text _attackDelayText;
    [SerializeField] private GameObject _attackDelayIcon;
    [SerializeField] private TMP_Text _extractionsText;
    [SerializeField] private GameObject _extractionsIcon;
    [SerializeField] private GameObject _radiusAttack;

    private Localization _localization;

    private void Awake()
    {
        _localization = FindObjectOfType<Localization>();
    }

    private void OnEnable()
    {
        _localization.LanguageChanged += UpdateTranslationText;
    }

    private void OnDisable()
    {
        _localization.LanguageChanged -= UpdateTranslationText;
    }

    private void Start()
    {
        UpdateTranslationText();

        if (_healthContainer != null)
            _healthText.text = _healthContainer.Health.ToString();

        if (_extraction != null)
            _extractionsText.text = _extraction.AmountMoneyProduced.ToString();
        else
            _extractionsIcon.SetActive(false);

        if (_shootTurret != null)
        {
            _damageText.text = _shootTurret.Damage.ToString();
            _attackDelayText.text = _shootTurret.ShootDelay.ToString();
            Vector3 scale = Vector3.one * _shootTurret.RadiusAttack * 2;
            _radiusAttack.transform.localScale = scale;
        }
        else
        {
            _damageIcon.SetActive(false);
            _attackDelayIcon.SetActive(false);
            if(_radiusAttack != null )
                _radiusAttack.SetActive(false);
        }
    }

    public void CloseRadiusAttack()
    {
        if (_radiusAttack != null)
            _radiusAttack.SetActive(false);
    }

    private void UpdateTranslationText()
    {
        _labelText.text = LeanLocalization.GetTranslationText(_goods.Label);
    }
}
