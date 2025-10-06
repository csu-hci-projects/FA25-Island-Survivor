using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class AmmoInterface : MonoBehaviour
{
    public WeaponObject currentWeapon;
    public TextMeshProUGUI textMeshPro;
    // Update is called once per frame
    void Update()
    {
        if(currentWeapon != null) textMeshPro.text = currentWeapon.bulletCount.ToString() + "/" + currentWeapon.ammo.ToString();
    }
}
