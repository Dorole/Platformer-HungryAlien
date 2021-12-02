using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public BossHealth bossHealth;

    public Slider healthSlider;
    public Gradient gradient;
    public Image fill;

    private void Start()
    {
        healthSlider.maxValue = bossHealth.health;

        fill.color = gradient.Evaluate(1f);
    }

    private void Update()
    {
        healthSlider.value = bossHealth.health;

        fill.color = gradient.Evaluate(healthSlider.normalizedValue);
    }

}
