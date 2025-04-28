using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public PlayerAction playerAction;
    public SimpleInventory inventory;

    [SerializeField] private int health;
    private int maxHealth;
    [SerializeField] private int attackDamage;
    private int attackRange = 30; 
    private int attackCoolDown=1;
    public Transform spawnPoint;

    void Awake()
    {

            Instance = this;
            playerAction = GetComponent<PlayerAction>();
            inventory = GetComponent<SimpleInventory>();

    }
    
    void Start(){


        if (GameManager.Instance.GetDifficulty()==0){
                maxHealth=200;
                health=maxHealth;
        }

        else{
               maxHealth=100;
               health=maxHealth;
        }

        if (GameManager.Instance.GetBool()){
            LoadPlayer();
        }
    }

    public int GetPlayerHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public int GetAttackDamage()
    {
        return attackDamage;
    }

    public int GetAttackRange()
    {
        return attackRange;
    }

    public int GetAttackCoolDown()
    {
        return attackCoolDown;
    }

    public void SetPlayerHealth(int healthValue)
    {
        health = healthValue;
        if (health > maxHealth) health = maxHealth;
    }

    public void SetMaxHealth(int number){
         maxHealth= number;
    }

     public void SetAttackDamage(int dmg){
         attackDamage=dmg;
    }


    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    public void SetPlayerPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SavePlayer()
    {
        Weapon playerWeapon = inventory.GetCurrentWeapon();
        int playerCoin = inventory.GetCoin();
        Vector3 playerPosition = GetPlayerPosition();
        
        PlayerPrefs.SetInt("CurrentWeapon", (int)playerWeapon.weaponType);
        PlayerPrefs.SetInt("PlayerCoin", playerCoin);
        PlayerPrefs.SetInt("PlayerHealth", health);
        PlayerPrefs.SetInt("PlayerMaxHealth", maxHealth);
        PlayerPrefs.SetFloat("PlayerPosX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerPosY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerPosZ", playerPosition.z);
    }

    private void LoadPlayer()
    {
        if (PlayerPrefs.HasKey("PlayerHealth"))
        {
            int playerCoin = PlayerPrefs.GetInt("PlayerCoin");
            int playerHealth = PlayerPrefs.GetInt("PlayerHealth");
            int playerMaxHealth = PlayerPrefs.GetInt("PlayerMaxHealth");
            int weaponTypeInt = PlayerPrefs.GetInt("CurrentWeapon", 0);
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");
            Vector3 playerPosition = new Vector3(x, y, z);

            inventory.SetCurrentWeapon((Weapon.WeaponType)weaponTypeInt);
            SetPlayerHealth(playerHealth);
            SetMaxHealth(playerMaxHealth);
            playerAction.UpdateHealthBar(health,maxHealth);
            inventory.UpdateHealthBar(health,maxHealth);
            inventory.SetCoin(playerCoin);
            SetPlayerPosition(playerPosition);
        }
        else
        {
            // Se non ci sono dati di salvataggio, spawn il giocatore nel punto predefinito
            PlayerManager.Instance.SetPlayerPosition(spawnPoint.position);
        }
    }
}
