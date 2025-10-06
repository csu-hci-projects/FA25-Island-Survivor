using System.Linq;
using StarterAssets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private bool ShowInventory = false;
    private Item EquippedItem;
    private GameObject EquippedMesh;
    public GameObject InventoryScreen;
    public InventoryObject inventory;
    public InventoryObject hotbar;
    public Camera cam;
    int currentSlot = -1;
    public LayerMask doorLayerMask;
    public TextMeshProUGUI ammoGUI;

    public MouseItem mouseItem = new MouseItem();
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            inventory.addItem(new Item(item.item), item.amount);
            Destroy(other.gameObject);
        }
        var secondtype = other.GetComponent<AmmoItem>();
        if (secondtype)
        {
            Debug.Log("component AmmoItem Found");
            int ID = secondtype.ammo.weapon.ID;
            for (int i = 0; i < inventory.Container.Items.Length; i++)
            {
                if (inventory.Container.Items[i].id == ID)
                {
                    Debug.Log("ID found, adding ammo count");
                    secondtype.ammo.weapon.ammo += secondtype.ammo.ammoCount;
                    Destroy(other.gameObject);
                }
            }
            for (int i = 0; i < hotbar.Container.Items.Length; i++)
            {
                if (hotbar.Container.Items[i].id == ID) 
                {
                    Debug.Log("ID found, adding ammo count");
                    secondtype.ammo.weapon.ammo += secondtype.ammo.ammoCount;
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
        if(Input.GetKeyDown(KeyCode.Tab))
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
                    if(((WeaponObject)EquippedItem.itemObject).weaponType == weaponType.Melee)
                    {
                        GetComponent<Gun>().weapon = (WeaponObject)EquippedItem.itemObject;
                        GetComponent<Gun>().SwingWeapon();
                    }
                    else if (((WeaponObject)EquippedItem.itemObject).isAutomatic)
                    {
                        GetComponent<Gun>().weapon = (WeaponObject)EquippedItem.itemObject;
                        GetComponent<Gun>().FireWeapon();
                        //play muzzle flash particle
                    }
                }
            }
        }if (Input.GetMouseButtonDown(0))
        {
            if (EquippedItem != null)
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
                if(EquippedItem.type == itemType.Healing || EquippedItem.type == itemType.Food)
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
                        GetComponent<Gun>().FireWeapon();
                        //play muzzle flash particle
                    }
                }
                if(EquippedItem.type == itemType.Key)
                {
                    Debug.Log("Using Key");
                    Collider[] doors = Physics.OverlapBox(transform.position, new Vector3(4,2,4), Quaternion.identity, doorLayerMask);
                    foreach (Collider door in doors)
                    {
                        Debug.Log("Key Interacting With Door");
                        KeyObject key = (KeyObject)EquippedItem.itemObject;
                        if (door.GetComponent<Door>().doorID.Contains(key.doorID))
                        {
                            door.GetComponent<Door>().openDoor(key.doorID);
                            bool removeItem = hotbar.UseItem(currentSlot);
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
            if(EquippedItem != null)
            {
                if(EquippedItem.type == itemType.Weapon)
                {
                    WeaponObject weapon = (WeaponObject)EquippedItem.itemObject;
                    //play Reload animation
                    weapon.Reload();
                }
            }
        }
    }
    public void EquipSlot(int slot)
    {
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
            EquippedMesh.transform.localRotation = Quaternion.identity;
            currentSlot = slot;
            if (EquippedItem.type == itemType.Weapon)
            {
                ammoGUI.gameObject.SetActive(true);
                ammoGUI.gameObject.GetComponent<AmmoInterface>().currentWeapon = (WeaponObject)EquippedItem.itemObject;
            }
        }
    }
}
