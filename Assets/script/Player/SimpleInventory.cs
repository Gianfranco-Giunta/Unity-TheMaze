using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleInventory : MonoBehaviour
{
    
    public Text weapon;
    public Text Coin;
    public Text healCostText;
    public GameObject inventory;
    public Weapon currentWeapon;
    private int coin;
    private int dmg;
    private bool isInventoryOpen=false;
    private const int changeWeaponCost = 60;
    private int healCost = 20;
    [SerializeField] private Image lifeBar;
    public GameObject[] weaponPrefabs;
    private Weapon.WeaponType[] weaponTypes = { Weapon.WeaponType.Sword, Weapon.WeaponType.Axe, Weapon.WeaponType.Spear };

    void Start()
    {
        healCostText.text = "Costo Guarigione: " + healCost;
        if (!GameManager.Instance.GetBool()){
            currentWeapon = new Weapon(Weapon.WeaponType.Sword);
            int dmg= currentWeapon.GetDamage();
            PlayerManager.Instance.SetAttackDamage(dmg);
            UpdateInventory();
        }
    }

    public void AddCoin(int amount)
    {
        coin+= amount;
        UpdateInventory(); 
    }

    public void AddWeapon(Weapon.WeaponType weaponType)
    {
        currentWeapon = new Weapon(weaponType);
        int dmg= currentWeapon.GetDamage();
        PlayerManager.Instance.SetAttackDamage(dmg);
        UpdateInventory();
    }

    public void BuyHeal()
    {
        if (coin >= healCost)
        {
            coin -= healCost; 
            UpdateInventory(); 
            HealPlayer(); 
       }
       else
       {
            Debug.Log("Non hai abbastanza soldi per comprare la guarigione!");
       }
    }

    private void HealPlayer()
    {
            PlayerManager.Instance.SetPlayerHealth(PlayerManager.Instance.GetPlayerHealth() + 50);
            UpdateHealthBar(PlayerManager.Instance.GetPlayerHealth(),PlayerManager.Instance.GetMaxHealth());
            Debug.Log("Guarito completamente!");
    }


    public void ChangeWeapon()
    {
        if (currentWeapon.weaponType == Weapon.WeaponType.LegendarySword)
        {
            Debug.Log("Non puoi cambiare arma, hai già la Legendary Sword.");
            return;
        }
        int currentIndex = System.Array.IndexOf(weaponTypes, currentWeapon.weaponType);
        
        if (currentIndex < weaponTypes.Length - 1)
        {
            Weapon.WeaponType nextWeaponType = weaponTypes[currentIndex + 1];

            if (coin >= changeWeaponCost)
            {
                coin -= changeWeaponCost; 
                currentWeapon = new Weapon(nextWeaponType);
                int dmg= currentWeapon.GetDamage();
                PlayerManager.Instance.SetAttackDamage(dmg);
                UpdateInventory();
            }
            else
            {
                Debug.Log("Non hai abbastanza monete per cambiare tipo di arma.");
            }
        }
        else
        {
            Debug.Log("Hai già l'arma più potente.");
        }
    }

    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }
    
    public void SetCurrentWeapon(Weapon.WeaponType weaponType)
    {
        currentWeapon = new Weapon(weaponType);
        PlayerManager.Instance.SetAttackDamage(currentWeapon.GetDamage());
        UpdateInventory();
    }

    public int GetCoin()
    {
        return coin;
    }

    public void SetCoin(int playerCoin)
    {
        coin= playerCoin;
        UpdateInventory();
    }

    public void ResetCoin(){
        coin = 0;
    }

    private void ActivateWeaponPrefab(Weapon.WeaponType weaponType)
    {
        // Disattiva tutti i prefab
        foreach (GameObject weaponPrefab in weaponPrefabs)
        {
            weaponPrefab.SetActive(false);
        }

        // Attiva il prefab corretto in base al tipo di arma
        switch (weaponType)
        {
            case Weapon.WeaponType.Sword:
                weaponPrefabs[0].SetActive(true); 
                break;
            case Weapon.WeaponType.Axe:
                weaponPrefabs[1].SetActive(true); 
                break;
            case Weapon.WeaponType.Spear:
                weaponPrefabs[2].SetActive(true); 
                break;
            case Weapon.WeaponType.LegendarySword:
                weaponPrefabs[3].SetActive(true); 
                break;
            default:
                Debug.LogWarning("Tipo di arma non riconosciuto.");
                break;
        }
    }

    public void UpdateInventory(){
        ActivateWeaponPrefab(currentWeapon.weaponType);
        weapon.text= $"Arma Attuale: {currentWeapon.weaponType} (Danno: {currentWeapon.GetDamage()})";;
        Coin.text="Soldi: "+ coin;
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        float value = currentHealth / maxHealth;
        lifeBar.fillAmount= value;
    }

    public void ToggleInventory(){
        isInventoryOpen= !isInventoryOpen;
        inventory.SetActive(isInventoryOpen);
    }

}
