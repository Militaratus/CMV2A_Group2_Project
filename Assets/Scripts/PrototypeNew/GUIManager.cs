using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    enum MenuPanel { Dialog, EscapePlan, HUD };

    private MenuPanel activePanel;

    public GameObject panelDialog;
    public GameObject panelEscapePlan;
    public GameObject panelHUD;

    public Image[] inventorySlots;
    public Text textInteract;

	// Use this for initialization
	void Start ()
    {
        ActivatePanel(MenuPanel.HUD);
        HideInteractionIcon();

    }

    void ActivatePanel(MenuPanel panel)
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

        textInteract.text = "[ INTERACT ]\nto Talk";
    }

    public void ShowInteractIconCollect()
    {
        if (!ShowHUD())
        {
            return;
        }

        textInteract.text = "[ INTERACT ]\nto Collect";
    }

    public void HideInteractionIcon()
    {
        if (!ShowHUD())
        {
            return;
        }

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
}
