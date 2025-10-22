using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using StarterAssets;
using Unity.VisualScripting;
using System;

public class PlayerHealthManager : MonoBehaviour
{
    public HealthObject Health;
    public HealthObject Hunger;
    public UnityEngine.UI.Slider HealthSlider;
    public UnityEngine.UI.Slider HungerSlider;
    public Canvas endScreen;
    public Canvas UI;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI hungerText;
    public int decrement = -1;
    private int Sprinting = 0;
    private int inDesert = 0;

    private void Start()
    {
        Hunger.ResetHP();
        Health.ResetHP();
        StartCoroutine(HungerCount());
        StartCoroutine(HungerLow());
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Desert"))
        {
            inDesert = 1;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Desert"))
        {
            inDesert = 0;
        }
    }
    // Update is called once per frame
    IEnumerator HungerCount()
    {
        
        while (true)
        {
            if (Hunger.GetHealth() > 0)
            {
                Hunger.SetHealth(decrement - (Sprinting + inDesert) * 2);
                HungerSlider.value = Hunger.GetHealth();
                hungerText.text = "Hunger: " + Hunger.GetHealth();
            }
            yield return new WaitForSeconds(2f);
        }
    }
    IEnumerator HungerLow()
    {
        while (true)
        {
            if (Hunger.GetHealth() <= 0)
            {
                Health.SetHealth(decrement * 2);
                HealthSlider.value = Health.GetHealth();
                healthText.text = "Health: " + Health.GetHealth();
            }
            yield return new WaitForSeconds(3f);
        }
    }

    public void dealDamage(int damage)
    {
        Health.SetHealth(damage);
        HealthSlider.value = Health.GetHealth();
        healthText.text = "Health: " + Health.GetHealth();
    }
    void Update()
    {
            if (Health.GetHealth() <= 0)
            {
                if (!this.CompareTag("Player"))
                {
                    Destroy(this);
                }
                else
                {
                    UI.gameObject.SetActive(false);
                    endScreen.gameObject.SetActive(true);
                    Time.timeScale = 0.0f;
                    UnityEngine.Cursor.visible = true;
                    UnityEngine.Cursor.lockState = CursorLockMode.None;
                    gameObject.GetComponent<FirstPersonController>().enabled = false;
                }
            }
        Sprinting = Input.GetKey(KeyCode.LeftShift) ? 1 : 0;
    }
    private void OnApplicationQuit()
    {
        Health.ResetHP();
        Hunger.ResetHP();
    }
}
