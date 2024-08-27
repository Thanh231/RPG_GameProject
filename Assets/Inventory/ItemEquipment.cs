
using UnityEngine;

public enum Equipmenttype
{
    Sword,
    Armor,
    Flask,
    Amulet
}
[CreateAssetMenu(menuName = "Create new Data/Equipment", fileName = "Equipment")]
public class ItemEquipment : ItemData
{
    public Equipmenttype equimenttype;

    [Header("Major Stats")]
    public int strength;
    public int agility;
    public int intelligence;
    public int vitality;

    [Header("Offensive stats")]
    public int damage;
    public int critChance;
    public int critPower;

    [Header("Defensive stats")]
    public int maxHealth;
    public int evasion;
    public int armor;
    public int resistanceMagical;

    [Header("Magical DamageEffect")]
    public int fireDamage;
    public int iceDamage;
    public int ligtningDamage;

    public void AddModify()
    {
        PlayerStats player = PlayerManager.instance.player.GetComponent<PlayerStats>();

        player.strength.AddModify(strength);
        player.agility.AddModify(agility);
        player.intelligence.AddModify(intelligence);
        player.vitality.AddModify(vitality);

        player.damage.AddModify(damage);
        player.critPower.AddModify(critPower);
        player.critChance.AddModify(critChance);

        player.maxHealth.AddModify(maxHealth);
        player.evasion.AddModify(evasion);
        player.armor.AddModify(armor);
        player.resistanceMagical.AddModify(resistanceMagical);

        player.fireDamage.AddModify(fireDamage);
        player.iceDamage.AddModify(iceDamage);
        player.ligtningDamage.AddModify(ligtningDamage);

    }
    public void RemoveModify()
    {
        PlayerStats player = PlayerManager.instance.player.GetComponent<PlayerStats>();

        player.strength.RemoveModify(strength);
        player.agility.RemoveModify(agility);
        player.intelligence.RemoveModify(intelligence);
        player.vitality.RemoveModify(vitality);

        player.damage.RemoveModify(damage);
        player.critPower.RemoveModify(critPower);
        player.critChance.RemoveModify(critChance);

        player.maxHealth.RemoveModify(maxHealth);
        player.evasion.RemoveModify(evasion);
        player.armor.RemoveModify(armor);
        player.resistanceMagical.RemoveModify(resistanceMagical);

        player.fireDamage.RemoveModify(fireDamage);
        player.iceDamage.RemoveModify(iceDamage);
        player.ligtningDamage.RemoveModify(ligtningDamage);
    }
}
