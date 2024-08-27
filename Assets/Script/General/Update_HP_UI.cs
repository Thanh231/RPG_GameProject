using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Update_HP_UI : MonoBehaviour
{
    private Player player;
    private Slider hpSlider;
    public TextMeshProUGUI hpText;
    private PlayerStats playerStats;

    void Start()
    {
        player = PlayerManager.instance.player;
        hpSlider = GetComponentInChildren<Slider>();
        playerStats = player.GetComponent<PlayerStats>();


        playerStats.onUpdateHp += UpdateUI;
        UpdateUI();
    }

    private void UpdateUI()
    {
        hpSlider.maxValue = playerStats.maxHealth.GetValue();
        hpSlider.value = playerStats.curentHealth;
        hpText.text = playerStats.curentHealth + "/" + playerStats.maxHealth.GetValue();
    }


}
