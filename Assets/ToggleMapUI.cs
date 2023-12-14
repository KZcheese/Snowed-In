using UnityEngine;

public class ToggleMapUI : Clickable
{
    public GameObject UI; // Assign the UI element in the inspector

    // Call this method to show the map
    public void ShowMap()
    {
        if (UI != null && !UI.activeSelf)
        {
            UI.SetActive(true);
        }
    }

    // Call this method to hide the map
    public void HideMap()
    {
        if (UI != null && UI.activeSelf)
        {
            UI.SetActive(false);
        }
    }

    public override void onClick()
    {
        if (UI.activeSelf)
        {
            HideMap();
        }
        else
        {
            ShowMap();
        }
    }
}