using StarterAssets;
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

    public MouseItem mouseItem = new MouseItem();
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            inventory.addItem(new Item(item.item), item.amount);
            Destroy(other.gameObject);
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
                }
                if(EquippedItem.type == itemType.Healing || EquippedItem.type == itemType.Food)
                {
                    EquippedItem.UseItem();
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
                if (EquippedItem.ID == hotbar.Container.Items[slot].Item.ID)
                {
                    EquippedItem = null;
                    return;
                }
            }
            EquippedItem = hotbar.Container.Items[slot].Item;
            EquippedMesh = Instantiate(EquippedItem.playerHoldingObject);
            EquippedMesh.transform.SetParent(cam.transform);
         
            EquippedMesh.transform.localPosition = new Vector3(1.2f, -0.5f, 0.75f);
            EquippedMesh.transform.localRotation = Quaternion.identity;

        }
    }
}
