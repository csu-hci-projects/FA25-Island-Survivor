using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public WeaponObject weapon;
    public Transform playerCam;

    public float currentCooldown;

    public void SwingWeapon()
    {
        if(currentCooldown <= 0)
        {
            currentCooldown = weapon.fireCooldown;
            spawnRay();
        }
    }

    public void FireWeapon()
    {
        if(currentCooldown <= 0f)
        {

            if (weapon.bulletCount > 0)
            {
                weapon.Use();
                spawnRay();
                currentCooldown = weapon.fireCooldown;
            }
            else
            {
                //call reload animation
                weapon.Reload();
                currentCooldown = weapon.reloadCooldown;
            }
        }
    }
    public void Update()
    {
        currentCooldown -= Time.deltaTime;
    }
    public void spawnRay()
    {
        Ray gunRay = new Ray(playerCam.position, playerCam.forward);
        if (Physics.Raycast(gunRay, out RaycastHit hitInfo, weapon.range))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out EnemyActor enemy)){
                Debug.Log("Enemy Hit");
                enemy.enemyType.EnemyHealth.currentHealth -= weapon.damage;
            }

        }
    }
}
