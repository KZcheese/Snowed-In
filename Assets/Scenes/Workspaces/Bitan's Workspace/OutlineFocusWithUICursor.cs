using UnityEngine;
using UnityEngine.InputSystem;

public class OutlineFocusWithUICursor : MonoBehaviour
{
    public LayerMask interactableLayerMask; // Set this in the inspector
    public float maxRaycastDistance = 10f; // Maximum distance for raycast
    private GameObject focusedObject = null;

    // Debounce variables
    public float debounceTime = 0.1f; // Time to wait before changing focus
    private float timeSinceLastFocusChange = 0f;
    private GameObject pendingFocusObject = null;

    void Update()
    {
        timeSinceLastFocusChange += Time.deltaTime;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRaycastDistance, interactableLayerMask))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Check if the hit object is an interactable or a door knob
            if (hitObject.CompareTag("interactable") || hitObject.CompareTag("DoorKnob"))
            {
                if (focusedObject != hitObject)
                {
                    pendingFocusObject = hitObject;

                    if (timeSinceLastFocusChange >= debounceTime)
                    {
                        ChangeFocus();
                    }
                }
            }
        }
        else
        {
            ClearFocus();
        }

        // Check for mouse click on a focused object
        if (Mouse.current.leftButton.wasPressedThisFrame && focusedObject != null)
        {
            AttemptInteraction(focusedObject);
        }
    }

    private void ClearFocus()
    {
        if (focusedObject != null)
        {
            focusedObject.layer = LayerMask.NameToLayer("Default");
            focusedObject = null;
            timeSinceLastFocusChange = 0f;
        }
    }

    private void ChangeFocus()
    {
        if (focusedObject != null)
        {
            // Revert the previously focused object back to its original layer
            focusedObject.layer = LayerMask.NameToLayer("Default");
        }

        // Set the new focused object's layer to 'Outlined'
        if (pendingFocusObject != null)
        {
            pendingFocusObject.layer = LayerMask.NameToLayer("Outlined Objects");
            focusedObject = pendingFocusObject;
        }

        // Reset the timer
        timeSinceLastFocusChange = 0f;
    }

    private void AttemptInteraction(GameObject interactableObject)
    {
        if (interactableObject.CompareTag("DoorKnob"))
        {
            ToggleDoor doorScript = interactableObject.GetComponentInParent<ToggleDoor>();
            if (doorScript != null)
            {
                doorScript.toggle();
            }
        }
    }
}
