
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, GrabableItem
{
    private InputAction inventoryAction;

    [SerializeField] private GameObject InventoryCanvas;
    [SerializeField] private int slotCount = 16;

    private bool InventoryActive = false;

    public ItemSlot[] itemSlot;

    void Start()
    {
        // Periksa InventoryCanvas
        if (InventoryCanvas == null)
        {
            Debug.LogError("InventoryCanvas belum dihubungkan di Inspector!", this);
            enabled = false;
            return;
        }

        // Ambil Input Action Inventory
        inventoryAction = InputSystem.actions.FindAction("Inventory");

        if (inventoryAction == null)
        {
            Debug.LogError("Action 'Inventory' tidak ditemukan di Input Actions!");
            enabled = false;
            return;
        }

        // Buat slot inventory
        CreateSlots();

        // Sembunyikan inventory saat game dimulai
        InventoryCanvas.SetActive(false);
        InventoryActive = false;
    }

    void Update()
    {
        if (inventoryAction == null || InventoryCanvas == null)
            return;

        if (inventoryAction.triggered)
        {
            InventoryActive = !InventoryActive;

            InventoryCanvas.SetActive(InventoryActive);

            // Pause dan lanjutkan game
            Time.timeScale = InventoryActive ? 0f : 1f;

            Debug.Log(InventoryActive
                ? "Inventory dibuka"
                : "Inventory ditutup");
        }
    }

    private void CreateSlots()
    {
        // Ambil prefab ItemSlot dari folder Resources
        ItemSlot slotPrefab = Resources.Load<ItemSlot>("ItemSlot");

        if (slotPrefab == null)
        {
            Debug.LogError(
                "Prefab ItemSlot tidak ditemukan! Pastikan ada di folder Resources.",
                this
            );

            itemSlot = new ItemSlot[0];
            return;
        }

        // Cari GridLayoutGroup untuk menampung slot
        GridLayoutGroup grid =
            InventoryCanvas.GetComponentInChildren<GridLayoutGroup>(true);

        if (grid == null)
        {
            Debug.LogError(
                "GridLayoutGroup tidak ditemukan di dalam InventoryCanvas!",
                this
            );

            itemSlot = new ItemSlot[0];
            return;
        }

        // Buat slot sebanyak slotCount
        itemSlot = new ItemSlot[slotCount];

        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i] = Instantiate(slotPrefab, grid.transform);
        }
    }

    public void AddItem(ItemDetails item)
    {
        if (item == null)
        {
            Debug.LogWarning("ItemDetails belum diisi pada item yang diambil!");
            return;
        }

        Debug.Log(
            "Item added to inventory: " +
            item.ItemName +
            " Quantity: " +
            item.Quantity
        );

        if (itemSlot == null || itemSlot.Length == 0)
        {
            Debug.LogWarning("Slot inventory belum berhasil dibuat!");
            return;
        }

        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i] == null)
                continue;

            if (!itemSlot[i].isFull)
            {
                itemSlot[i].AddItem(item);
                return;
            }
        }

        Debug.LogWarning(
            "Inventory penuh, item '" + item.ItemName + "' tidak dapat ditambahkan."
        );
    }
}