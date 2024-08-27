using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Entity entity;
    private RectTransform rectTransform;
    private CharacterStats stats;
    private Slider slider;
    void Start()
    {
        entity= GetComponentInParent<Entity>();
        rectTransform = GetComponent<RectTransform>();
        stats = GetComponentInParent<CharacterStats>();
        slider = GetComponentInChildren<Slider>();

        entity.onFlip += FlipUI;
        stats.onUpdateHp += UpdateHealthUI;

        UpdateHealthUI();
    }

    private void FlipUI()
    {
        rectTransform.Rotate(0, 180, 0);
    }
    
    private void UpdateHealthUI()
    {
        slider.maxValue = stats.GetMaxHealth();
        slider.value = stats.curentHealth;

    }
    private void OnDisable()
    {
        entity.onFlip -= FlipUI;
        stats.onUpdateHp -= UpdateHealthUI;
    }
}
