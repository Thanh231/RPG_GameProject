using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Update_Ui_Boss : MonoBehaviour
{
    private RedHood boss;
    private Slider hpSlider;
    public TextMeshProUGUI hpText;
    private EnemyStats rhStats;

    void Start()
    {
        boss = GameManager.ins.boss;
        hpSlider = GetComponentInChildren<Slider>();
        rhStats = boss.GetComponent<EnemyStats>();

        rhStats.onUpdateHp += UpdateUI;
        UpdateUI();
    }

    private void UpdateUI()
    {
        hpSlider.maxValue = rhStats.maxHealth.GetValue();
        hpSlider.value = rhStats.curentHealth;
        hpText.text = rhStats.curentHealth + "/" +rhStats.maxHealth.GetValue();
    }

}
