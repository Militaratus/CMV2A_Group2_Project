using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public enum MenuPanel { Dialog, EscapePlan, HUD };

    internal MenuPanel activePanel = MenuPanel.HUD;

    public GameObject panelDialog;
    public GameObject panelEscapePlan;
    public GameObject panelHUD;

    public Image[] inventorySlots;
    public Text textInteract;

	// Use this for initialization
	void Awake ()
    {
        ActivatePanel(MenuPanel.HUD);
        HideInteractionIcon();

    }

    public void ActivatePanel(MenuPanel panel)
    {
        panelDialog.SetActive(false);
        panelEscapePlan.SetActive(false);
        panelHUD.SetActive(false);

        switch (panel)
        {
            case MenuPanel.Dialog:
                panelDialog.SetActive(true); break;
            case MenuPanel.EscapePlan:
                panelEscapePlan.SetActive(true); break;
            case MenuPanel.HUD:
                panelHUD.SetActive(true); break;
            default:
                Debug.Log("FUCKING ERROR HAS OCCURED!"); break;
        }
        activePanel = panel;
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    public void ShowInteractIconTalk()
    {
        if (!ShowHUD())
        {
            return;
        }

        textInteract.transform.parent.parent.gameObject.SetActive(true);

        textInteract.text = "[ INTERACT ]\nto Talk";
    }

    public void ShowInteractIconCollect()
    {
        if (!ShowHUD())
        {
            return;
        }
        textInteract.transform.parent.parent.gameObject.SetActive(true);
        textInteract.text = "[ INTERACT ]\nto Collect";
    }

    public void ShowInteractIconOpen()
    {
        if (!ShowHUD())
        {
            return;
        }
        textInteract.transform.parent.parent.gameObject.SetActive(true);
        textInteract.text = "[ INTERACT ]\nto Open";
    }

    public void ShowInteractIconUse()
    {
        if (!ShowHUD())
        {
            return;
        }
        textInteract.transform.parent.parent.gameObject.SetActive(true);
        textInteract.text = "[ INTERACT ]\nto Use";
    }

    public void ShowInteractIconLocked()
    {
        if (!ShowHUD())
        {
            return;
        }
        textInteract.transform.parent.parent.gameObject.SetActive(true);
        textInteract.text = "Locked with a Key";
    }

    public void HideInteractionIcon()
    {
        if (!ShowHUD())
        {
            return;
        }
        textInteract.transform.parent.parent.gameObject.SetActive(false);
        textInteract.text = "";
    }

    bool ShowHUD()
    {
        bool showHUD = true;

        if (activePanel != MenuPanel.HUD)
        {
            showHUD = false;
        }

        return showHUD;
    }

    public void ShowInventory(int slot, string name)
    {
        inventorySlots[slot].color = Color.white;
        inventorySlots[slot].transform.GetChild(0).GetComponent<InventoryImage>().ShowImage(name);
    }

    public void HideInventory(int slot)
    {
        inventorySlots[slot].color = Color.gray;
        inventorySlots[slot].transform.GetChild(0).GetComponent<InventoryImage>().HideImage();
    }

    public void ExpandInventory()
    {
        for (int i = 2; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].color = Color.gray;
        }
    }
}
