using UnityEngine;

public class BuildingsViewManager : MonoBehaviour
{
    private static bool _menuOpen = false;
    private ScreenUI _currentlyOpenMenu;

    public bool MenuOpen => _menuOpen;

    private void OnEnable()
    {
        if (_currentlyOpenMenu != null)
        {
            _currentlyOpenMenu.Close();
            _currentlyOpenMenu = null;
        }
    }

    public void OpenMenu(ScreenUI menu)
    {
        if (_menuOpen || _currentlyOpenMenu != null)
        {
            return;
        }
        menu.Open();
        _currentlyOpenMenu = menu;
        _menuOpen = true;
    }

    public void CloseMenu(ScreenUI menu)
    {
        menu.Close();
        _currentlyOpenMenu = null;
        _menuOpen = false;
    }
}