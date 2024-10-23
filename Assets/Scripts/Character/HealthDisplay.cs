using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private CharacterHealth health;
    [SerializeField] private Canvas Canvas;
    [SerializeField] private Image healthImageUI;
    [SerializeField] private Color goodeColor = Color.green;
    [SerializeField] private Color poorColor = Color.red;

    public void ShowDisplay(bool show = true)
    {
        if (Canvas != null)
            Canvas.gameObject.SetActive(show);
    }

    private void OnEnable()
    {
        if (health != null)
            health.OnHealthChanged.AddListener(HandleHealthChange);
    }

    private void OnDisable()
    {
        if (health != null)
            health.OnHealthChanged.RemoveListener(HandleHealthChange);
    }

    private void HandleHealthChange(int oldHealth, int newHealth)
    {
        float value = (float)newHealth / health.MaxHealth;

        healthImageUI.fillAmount = value;
        healthImageUI.color = Color.Lerp(poorColor, goodeColor, value);
    }
}
