using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class TrainingScreen : MonoBehaviour
{
    [SerializeField] private MainMenuScreen _mainMenuScreen;
    [SerializeField] private ShopScreen _shopScreen;
    [SerializeField] private MovingCameraSpawnEnemies _cameraSpawnEnemies;
    [SerializeField] private SaveSystem _saveSystem;

    [SerializeField] private GameObject _panel1;
    [SerializeField] private Button _button1;

    [SerializeField] private GameObject _panel3;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Image _indicator1;

    [SerializeField] private GameObject _panel4;
    [SerializeField] private GameObject _indicators;
    [SerializeField] private Button _button4;

    [SerializeField] private GameObject _panel5;

    [SerializeField] private GameObject _panel6;
    [SerializeField] private Button _button6;

    [SerializeField] private GameObject _indicator2;
    [SerializeField] private GameObject _indicator3;
    [SerializeField] private GameObject _indicator4;
    [SerializeField] private GameObject _indicator5;

    [SerializeField] private Button _exitShopButton;
    [SerializeField] private Button _buttleButton;
    [SerializeField] private Button _buttleExitButton;

    [SerializeField] private Button _turretsShopButton;
    [SerializeField] private Button _generationsShopButton;
    [SerializeField] private Button _fencesShopButton;
    [SerializeField] private BuildingsGrid _buildingGrid;

    private bool _isOpenWindow1 = true;
    private bool _isEndOfTutorial = true;
    private bool _isButtleOfTutorial = true;
    private bool isHowMuchProducedByGenerators = true;

    public event UnityAction TutorialFinished;


    private void Start()
    {
        int level = PlayerPrefs.GetInt("Level");
        if (level <= 1)
        {
            _mainMenuScreen.TurnOffAllButton();
            _shopScreen.TurnOffAllButton();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }



    private void OnEnable()
    {
        _cameraSpawnEnemies.AnimationIsFinished += OpenWindow1;

        _button1.onClick.AddListener(OnOpenFollowingWindow2);
        _shopButton.onClick.AddListener(OnOpenFollowingWindow4);
        _button4.onClick.AddListener(OnOpenIndicator5);
        _button6.onClick.AddListener(CloseEndTutorialWindow);

        _buildingGrid.CreatedBuilding += OnCloseIndicator1;

        _buildingGrid.BuildingSupplied += OnOpenIndicators;
        _buildingGrid.RemoveBuilding += OnOpenIndicator1;

        _generationsShopButton.onClick.AddListener(OnCloseIndicator3);
        _fencesShopButton.onClick.AddListener(OnCloseIndicator4);
        _exitShopButton.onClick.AddListener(OnCloseIndicator5);

        _buttleButton.onClick.AddListener(OnCloseFollowingWindow5);
    }

    private void OnDisable()
    {
        _cameraSpawnEnemies.AnimationIsFinished -= OpenWindow1;

        _button1.onClick.RemoveListener(OnOpenFollowingWindow2);
        _shopButton.onClick.RemoveListener(OnOpenFollowingWindow4);
        _button4.onClick.RemoveListener(OnOpenIndicator5);
        _button6.onClick.RemoveListener(CloseEndTutorialWindow);

        _buildingGrid.CreatedBuilding -= OnCloseIndicator1;

        _buildingGrid.BuildingSupplied -= OnOpenIndicators;

        _buildingGrid.RemoveBuilding -= OnOpenIndicator1;

        _generationsShopButton.onClick.RemoveListener(OnCloseIndicator3);
        _fencesShopButton.onClick.RemoveListener(OnCloseIndicator4);
        _exitShopButton.onClick.RemoveListener(OnCloseIndicator5);

        _buttleButton.onClick.RemoveListener(OnCloseFollowingWindow5);
    }


    private void OpenWindow1()
    {
        if (_isOpenWindow1)
        {
            _panel1.SetActive(true);
            _isOpenWindow1 = false;
        }

    }

    public void OpenEndTutorialWindow()
    {
        if (_isEndOfTutorial)
        {
            _panel6.SetActive(true);
            _isEndOfTutorial = false;
        }
    }

    public void CloseEndTutorialWindow()
    {
        _panel6.SetActive(false);
        _mainMenuScreen.EnabletAllButton();
        _shopScreen.EnabletAllButton();
        TutorialFinished?.Invoke();
        _buttleExitButton.gameObject.SetActive(true);
        gameObject.SetActive(false);    
    }

    //private void OnOpenFollowingWindow2()
    //{
    //    _panel1.SetActive(false);
    //    _panel2.SetActive(true);
    //}

    private void OnOpenFollowingWindow2()
    {
        _panel1.SetActive(false);
        _panel3.SetActive(true);
        _indicator1.gameObject.SetActive(true);
        _mainMenuScreen.EnabletShopButton();
    }

    private void OnOpenFollowingWindow4()
    {
        if (isHowMuchProducedByGenerators)
        {
            _indicator1.gameObject.SetActive(false);
            _panel3.SetActive(false);
            _indicator2.SetActive(true);
            isHowMuchProducedByGenerators = false;
        }
    }

    //private void OnCloseFollowingWindow4()
    //{
    //    _panel4.SetActive(false);
    //    _indicator2.SetActive(true);
    //}

    private void OnCloseIndicator1()
    {
        _indicator2.SetActive(false);
    }

    private void OnOpenIndicator1()
    {
        _indicator2.SetActive(true);
    }

    private void OnOpenIndicators(Building building)
    {
        if(building.tag == "Turret")
        {
            _shopScreen.EnabletGeneratorsButton();
            _indicator3.SetActive(true);
        }
        else if (building.tag == "Oil")
        {
            _shopScreen.EnabletFencesButton();
            _indicator4.SetActive(true);
        }
        else if (building.tag == "Fence")
        {
            _panel4.SetActive(true);
            _indicators.SetActive(true);
            //_indicator5.SetActive(true);
        }
    }

    private void OnCloseIndicator3()
    {
        _indicator3.SetActive(false);
        _indicator2.SetActive(true);
    }

    private void OnCloseIndicator4()
    {
        _indicator4.SetActive(false);
        _indicator2.SetActive(true);
    }

    private void OnOpenIndicator5()
    {
        _shopScreen.EnabletExitButton();
        _indicator5.SetActive(true);
        _panel4.SetActive(false);
        _indicators.SetActive(false);
    }

    private void OnCloseIndicator5()
    {
        _indicator5.SetActive(false);
        _panel5.SetActive(true);
        _mainMenuScreen.EnabletButtleButton();
        _buttleExitButton.gameObject.SetActive(false);
    }

    private void OnCloseFollowingWindow5()
    {
        _panel5.SetActive(false);
    }
}
