using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuilderView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;
    [SerializeField] private LeanLocalizedTextMeshProUGUI _leanLocalized;

    private Goods _building;
    public event UnityAction<Goods, BuilderView> SellButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
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
        _label.text = building.Label;
        _price.text = building.Price.ToString();
        _icon.sprite= _building.Icon;
    }

    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(_building, this);
        TryLockItem();
    }
}
