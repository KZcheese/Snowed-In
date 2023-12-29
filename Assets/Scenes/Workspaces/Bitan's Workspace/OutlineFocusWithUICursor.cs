using UnityEngine;
using UnityEngine.InputSystem;

public class OutlineFocusWithUICursor : MonoBehaviour
{
    public LayerMask interactableLayerMask; // Set this in the inspector
    public float maxRaycastDistance = 10f; // Maximum distance for raycast
    private GameObject focusedObject;


    // Debounce variables
    public float debounceTime = 0.1f; // Time to wait before changing focus
    private float timeSinceLastFocusChange;
    private GameObject pendingFocusObject;

    private void Update()
    {
        timeSinceLastFocusChange += Time.deltaTime;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if(Physics.Raycast(ray, out RaycastHit hit, maxRaycastDistance, interactableLayerMask))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Check if the hit object is an interactable or a door knob
            if(hitObject.CompareTag("interactable")
               || hitObject.CompareTag("DoorKnob")
               || hitObject.CompareTag("Map")
               || hitObject.CompareTag("Report"))
            {
                if(focusedObject != hitObject)
                {
                    pendingFocusObject = hitObject;
                    if(timeSinceLastFocusChange >= debounceTime) ChangeFocus();
                }
            }
        }
        else ClearFocus();

        // Check for mouse click on a focused object
        if(Mouse.current.leftButton.wasPressedThisFrame && focusedObject != null)
            AttemptInteraction(focusedObject);
    }

    private void ClearFocus()
    {
        if(!focusedObject) return;

        focusedObject.layer = LayerMask.NameToLayer("Default");
        if(focusedObject.CompareTag("Map") || focusedObject.CompareTag("Report"))
        {
            ToggleMapUI mapBehavior = focusedObject.GetComponent<ToggleMapUI>();
            if(mapBehavior) mapBehavior.HideMap(); // Hide the map UI when focus is lost
        }

        focusedObject = null;
        timeSinceLastFocusChange = 0f;
    }

    private void ChangeFocus()
    {
        if(focusedObject)
        {
            // Revert the previously focused object back to its original layer
            focusedObject.layer = LayerMask.NameToLayer("Default");
        }

        // Set the new focused object's layer to 'Outlined'
        if(pendingFocusObject)
        {
            pendingFocusObject.layer = LayerMask.NameToLayer("Outlined Objects");
            focusedObject = pendingFocusObject;
        }

        // Reset the timer
        timeSinceLastFocusChange = 0f;
    }

    private static void AttemptInteraction(GameObject interactableObject)
    {
        /* if (interactableObject.CompareTag("DoorKnob"))
         {
             ToggleDoor doorScript = interactableObject.GetComponentInParent<ToggleDoor>();
             if (doorScript != null)
             {
                 doorScript.toggle();
             }
         }*/

        Clickable behavior = interactableObject.GetComponent<Clickable>();
        if(behavior) behavior.onClick();
        else
        {
            behavior = interactableObject.GetComponentInChildren<Clickable>();
            if(behavior) behavior.onClick();
            else
            {
                behavior = interactableObject.GetComponentInParent<Clickable>();
                if(behavior) behavior.onClick();
                else
                    Debug.LogWarning("Warning: failed to find click behavior when interacting with " + interactableObject.name);
            }
        }
    }
}