using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthManager : MonoBehaviour
{
    public HealthObject Health;
    public HealthObject Hunger;
    public UnityEngine.UI.Slider HealthSlider;
    public UnityEngine.UI.Slider HungerSlider;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI hungerText;
    public int decrement = -1;

    private void Start()
    {
        StartCoroutine(HungerCount());
        StartCoroutine(HungerLow());
    }
    // Update is called once per frame
    IEnumerator HungerCount()
    {
        while (true)
        {
            if(Hunger.GetHealth() > 0)
            {
                Hunger.SetHealth(decrement);
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
                    // end game;
                }
            }
    }
    private void OnApplicationQuit()
    {
        Health.ResetHP();
        Hunger.ResetHP();
    }
}
