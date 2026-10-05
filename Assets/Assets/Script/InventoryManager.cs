using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    InputAction inventoryAction;
    [SerializeField] private GameObject inventoryCanvas;
    private bool InventoryActive;
    void Start()
    {
        inventoryAction = InputSystem.actions.FindAction("Inventory");
        inventoryCanvas.SetActive(false);
        InventoryActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryAction.triggered && inventoryCanvas != null && InventoryActive == false)
        {
            InventoryActive = true;
            inventoryCanvas.SetActive(true);
            Time.timeScale = 0f; // Pause the game
            Debug.Log("Inventory dibuka");
        }else if(inventoryAction.triggered && inventoryCanvas != null && InventoryActive == true)
        {
            InventoryActive = false;
            inventoryCanvas.SetActive(false);
            Time.timeScale = 1f; // Resume the game
            Debug.Log("Inventory ditutup");
        }
    }
}
