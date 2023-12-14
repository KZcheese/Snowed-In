using UnityEngine;

public class ToggleMapUI : Clickable
{
    public GameObject mapUI; // Assign the UI element in the inspector

    // Call this method to show the map
    public void ShowMap()
    {
        if (mapUI != null && !mapUI.activeSelf)
        {
            mapUI.SetActive(true);
        }
    }

    // Call this method to hide the map
    public void HideMap()
    {
        if (mapUI != null && mapUI.activeSelf)
        {
            mapUI.SetActive(false);
        }
    }

    public override void onClick()
    {
        if (mapUI.activeSelf)
        {
            HideMap();
        }
        else
        {
            ShowMap();
        }
    }
}