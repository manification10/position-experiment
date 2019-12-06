using UnityEngine;

public class UIManager: MonoBehaviour
{

    public enum Menus
    {
        MainMenu = 0,
        SettingsMenu = 1
    }

    public static UIManager instance;

    public GameObject[] menus;

    private void Awake()
    {
        instance = this;
        HideAllMenus();
    }

    public void ShowMenu(Menus pMenu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(i == (int)pMenu);
        }
    }

    public void HideAllMenus()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(false);
        }
    }

    public void OnMainMenu()
    {
        ShowMenu(Menus.MainMenu);
    }

    public void OnSettingsMenu()
    {
        ShowMenu(Menus.SettingsMenu);
    }

    public void OnCloseAllMenus()
    {
        HideAllMenus();
    }
}