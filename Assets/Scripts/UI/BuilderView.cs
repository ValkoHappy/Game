using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuilderView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;

    public string _translationKey;
    public Localization _localization;
    private Goods _building;
    public event UnityAction<Goods, BuilderView> SellButtonClick;

    private void Awake()
    {
        _localization = FindObjectOfType<Localization>();
    }

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
        _localization.LanguageChanged += GetTranslationText;
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
        _localization.LanguageChanged -= GetTranslationText;
    }

    public void TryLockItem()
    {
        if (_building.IsBuyed && _building.IsSingleProduct == false)
        {
            _sellButton.interactable = false;
        }
    }

    public void Render(Goods building)
    {
        _building = building;
        _translationKey = building.Label;
        GetTranslationText();
        _price.text = building.Price.ToString();
        _icon.sprite= _building.Icon;
    }

    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(_building, this);
        TryLockItem();
    }

    private void GetTranslationText()
    {
        _label.text = LeanLocalization.GetTranslationText(_translationKey);
    }
}
