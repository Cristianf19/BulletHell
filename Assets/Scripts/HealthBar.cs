using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    private int maxHealth;

    public void Setup(int max)
    {
        maxHealth = max;
        fillImage.fillAmount = 1f;
    }

    public void UpdateHealth(int current)
    {
        fillImage.fillAmount = (float)current/maxHealth;
    }
}
