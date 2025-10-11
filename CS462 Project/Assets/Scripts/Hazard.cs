using System.Collections;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hazard : MonoBehaviour
{
    public int damage;
    public float time;
    private bool cooldown = true;
    private bool playerInObject = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Player>() != null)
        {
            playerInObject = true;
            if (cooldown)
            {
                Player player = other.gameObject.GetComponent<Player>();
                StartCoroutine(TickDamage(player));
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            playerInObject = false;
        }
    }
    IEnumerator TickDamage( Player player)
    {
        player.GetComponent<FirstPersonController>().MoveSpeed /= 2;
        player.GetComponent<FirstPersonController>().SprintSpeed /= 2f;
        while (playerInObject)
        {
            cooldown = false;
            player.GetComponent<PlayerHealthManager>().dealDamage(-damage);
            yield return new WaitForSeconds(time);
            cooldown = true;
        }
        player.GetComponent<FirstPersonController>().MoveSpeed *= 2f;
        player.GetComponent<FirstPersonController>().SprintSpeed *= 2f;
    }
}
