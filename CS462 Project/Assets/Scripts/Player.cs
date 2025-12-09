using System.Linq;
using StarterAssets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private bool ShowInventory = false;
    public Item EquippedItem;
    private GameObject EquippedMesh;
    public GameObject InventoryScreen;
    public InventoryObject inventory;
    public InventoryObject hotbar;
    public Camera cam;
    int currentSlot = -1;
    public LayerMask doorLayerMask;
    public TextMeshProUGUI ammoGUI;
    public GroundItem touching_item;
    public bool isPaused = false;
    public Canvas PauseMenu;

    public MouseItem mouseItem = new MouseItem();
    public void OnTriggerEnter(Collider other)
    {
        var type = other.GetComponent<AmmoItem>();
        if (type)
        {
            int ID = type.ammo.weapon.ID;
            for (int i = 0; i < inventory.Container.Items.Length; i++)
            {
                if (inventory.Container.Items[i].id == ID)
                {
                    type.ammo.weapon.ammo += type.ammo.ammoCount;
                    Destroy(other.gameObject);
                }
            }
            for (int i = 0; i < hotbar.Container.Items.Length; i++)
            {
                if (hotbar.Container.Items[i].id == ID) 
                {
                    type.ammo.weapon.ammo += type.ammo.ammoCount;
                    Destroy(other.gameObject);
                }
            }
        }
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[20];
        hotbar.Container.Items = new InventorySlot[7];

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused= !isPaused;
            PauseMenu.gameObject.SetActive(isPaused);
            if (isPaused)
            {
                Time.timeScale = 0.0f;
                UnityEngine.Cursor.visible = true;
                UnityEngine.Cursor.lockState = CursorLockMode.None;
                gameObject.GetComponent<FirstPersonController>().enabled = false;
            }
            else
            {
                Time.timeScale = 1.0f;
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                UnityEngine.Cursor.visible = false;
                gameObject.GetComponent<FirstPersonController>().enabled = true;
                
            }
        }
        if (!isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!ShowInventory)
                {
                    ShowInventory = true;
                    InventoryScreen.SetActive(true);
                    UnityEngine.Cursor.visible = true;
                    UnityEngine.Cursor.lockState = CursorLockMode.None;
                    gameObject.GetComponent<FirstPersonController>().enabled = false;
                }
                else
                {
                    ShowInventory = false;
                    InventoryScreen.SetActive(false);
                    UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                    UnityEngine.Cursor.visible = false;
                    gameObject.GetComponent<FirstPersonController>().enabled = true;

                }

            }
            if (!ShowInventory)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    EquipSlot(0);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    EquipSlot(1);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    EquipSlot(2);
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    EquipSlot(3);
                }
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    EquipSlot(4);
                }
                if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    EquipSlot(5);
                }
                if (Input.GetKeyDown(KeyCode.Alpha7))
                {
                    EquipSlot(6);
                }
                if (Input.GetMouseButton(0))
                {
                    if (EquippedItem != null)
                    {

                        if (EquippedItem.type == itemType.Weapon)

                        {
                            if (((WeaponObject)EquippedItem.itemObject).weaponType == weaponType.Melee)
                            {
                                GetComponent<Gun>().weapon = (WeaponObject)EquippedItem.itemObject;
                                GetComponent<Gun>().animator = EquippedMesh.GetComponentInChildren<Animator>();
                                GetComponent<Gun>().SwingWeapon();
                            }
                            else if (((WeaponObject)EquippedItem.itemObject).isAutomatic)
                            {
                                GetComponent<Gun>().weapon = (WeaponObject)EquippedItem.itemObject;
                                GetComponent<Gun>().animator = EquippedMesh.GetComponentInChildren<Animator>();
                                GetComponent<Gun>().FireWeapon();
                                //play muzzle flash particle
                            }
                        }
                    }
                }
                if (Input.GetMouseButtonDown(0))
                {
                    if (EquippedItem != null || EquippedItem.ID != -1)
                    {

                        if (EquippedItem.type == itemType.Equipment)
                        {
                            EquippedItem.UseItem();
                            GetComponent<SpeedManager>().UseEquipment((EquipmentObject)EquippedItem.itemObject);
                            bool removeItem = hotbar.UseItem(currentSlot);
                            if (removeItem)
                            {
                                Destroy(EquippedMesh);
                                EquippedItem = null;
                            }
                            return;
                        }
                        if(EquippedItem.type == itemType.Healing)
                        {
                            if (gameObject.GetComponent<PlayerHealthManager>().Health.currentHealth <100)
                            {
                                gameObject.GetComponent<PlayerHealthManager>().dealDamage(((HealingObject)EquippedItem.itemObject).healAmount);
                                bool removeItem = hotbar.UseItem(currentSlot);
                                if (removeItem)
                                {
                                    Destroy(EquippedMesh);
                                    EquippedItem = null;
                                }
                            }
                            return;

                        }
                            if (EquippedItem.type == itemType.Food)
                        {
                            if (EquippedItem.UseItem())
                            {
                                bool removeItem = hotbar.UseItem(currentSlot);
                                if (removeItem)
                                {
                                    Destroy(EquippedMesh);
                                    EquippedItem = null;
                                }
                            }
                            return;
                        }
                        //Subtract 1 value from inventory location if not weapon
                        if (EquippedItem.type == itemType.Weapon)
                        {
                            if (!((WeaponObject)EquippedItem.itemObject).isAutomatic && ((WeaponObject)EquippedItem.itemObject).weaponType == weaponType.Ranged)
                            {
                                GetComponent<Gun>().weapon = (WeaponObject)EquippedItem.itemObject;
                                GetComponent<Gun>().animator = EquippedMesh.GetComponentInChildren<Animator>();
                                GetComponent<Gun>().FireWeapon();
                                //play muzzle flash particle
                            }
                        }
                        if (EquippedItem.type == itemType.Key)
                        {
                            Collider[] doors = Physics.OverlapBox(transform.position, new Vector3(4, 2, 4), Quaternion.identity, doorLayerMask);
                            foreach (Collider door in doors)
                            {
                                KeyObject key = (KeyObject)EquippedItem.itemObject;
                                if (door.GetComponent<Door>().doorID.Contains(key))
                                {
                                    door.GetComponent<Door>().openDoor(key);
                                    this.GetComponent<PickupTextManager>().updateText(door.GetComponent<Door>());
                                    bool removeItem = hotbar.UseItem(currentSlot);//delete this if you want to use a unique key for multiple different doors
                                    if (removeItem)
                                    {
                                        Destroy(EquippedMesh);
                                        EquippedItem = null;
                                    }
                                }
                            }
                            return;
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (EquippedItem != null)
                    {
                        if (EquippedItem.type == itemType.Weapon)
                        {
                            WeaponObject weapon = (WeaponObject)EquippedItem.itemObject;
                            //play Reload animation
                            weapon.Reload();
                        }
                    }
                }
            }
        }
    }
    public void EquipSlot(int slot)
    {
        if(slot == -1)
        {
            Destroy(EquippedMesh);
            ammoGUI.gameObject.SetActive(false);
            EquippedItem = null;
            currentSlot = -1;
            return;
        }
        if (hotbar.Container.Items[slot].Item.playerHoldingObject != null)
        {
            if (EquippedItem != null)
            {
                Destroy(EquippedMesh);
                ammoGUI.gameObject.SetActive(false);
                if (EquippedItem.ID == hotbar.Container.Items[slot].Item.ID)
                {
                    EquippedItem = null;
                    currentSlot = -1;
                    return;
                }
            }
            EquippedItem = hotbar.Container.Items[slot].Item;
            EquippedMesh = Instantiate(EquippedItem.playerHoldingObject);
            EquippedMesh.transform.SetParent(cam.transform);
         
            EquippedMesh.transform.localPosition = new Vector3(1.2f, -0.5f, 0.75f);
            EquippedMesh.transform.localRotation = Quaternion.Euler(0,180,0); 
            currentSlot = slot;
            if (EquippedItem.type == itemType.Weapon)
            {
                ammoGUI.gameObject.SetActive(true);
                ammoGUI.gameObject.GetComponent<AmmoInterface>().currentWeapon = (WeaponObject)EquippedItem.itemObject;
            }
        }
    }
    public void addInventoryItem(GroundItem item)
    {
        int ID = item.item.ID;
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            if (inventory.Container.Items[i].id == ID)
            {
                inventory.addItem(new Item(item.item), item.amount);
                return;
            }
        }
        if(!hotbar.addItem(new Item(item.item), item.amount))
        {
            inventory.addItem(new Item(item.item), item.amount);
        }
    }
}
