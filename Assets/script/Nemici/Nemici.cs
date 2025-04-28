using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Nemici : MonoBehaviour
{

    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    private int attackRange=30;
    [SerializeField] private int attackCoolDown; 
    private bool canAttack = true;
    protected NavMeshAgent agent;
    protected Transform player;
    [SerializeField] private int coinValue=10;
    private bool statsLoaded=false;
    [SerializeField] private Image lifeBar;

    protected virtual void Start(){
      if(!statsLoaded){
         if (GameManager.Instance.GetDifficulty() == 0){

            if (CompareTag("Boss")){
                health=200;
                maxHealth=200;
                damage=20;
                attackCoolDown=3;
            } 
            else{
                health=20;
                maxHealth=20;
                damage=10;
                attackCoolDown=3;
            }
         }

         else{
            if (CompareTag("Boss")){
                health=400;
                maxHealth=400;
                damage=50;
                attackCoolDown=3;
            }
            else{
                health=40;
                maxHealth=40;
                damage=20;
                attackCoolDown=3;
            }
         }

         StartCoroutine(PotenziaMostri());
      }

       agent = GetComponent<NavMeshAgent>();
       agent.speed = 40f;
       player = GameObject.FindGameObjectWithTag("Player").transform;
       agent.stoppingDistance = 30;
    }

    protected virtual void Update()
    {
        MoveTowardsPlayer();
        lifeBar.transform.LookAt(Camera.main.transform.position);

        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            AttackPlayer();
        }
    }

    public void AttackPlayer()
    {
        if (canAttack)
        {
            StartCoroutine(PerformAttack());
        }
    }

    private IEnumerator PotenziaMostri(){
        while(true){

             yield return new WaitForSeconds(120);

             if (GameManager.Instance.GetDifficulty() == 0){
                if (CompareTag("Boss")){
                   health+=50;
                   maxHealth+=50;
                   damage+=20;
               } 
               else{
                   health+=50;
                   maxHealth+=20;
                   damage+=10;
                   attackCoolDown=3;
               }
            }

            else{
               if (CompareTag("Boss")){
                   health+=100;
                   maxHealth+=100;
                   damage+=50;
               }
               else{
                   health+=60;
                   maxHealth+=60;
                   damage+=20;
               }
            }

        }


    }

    private IEnumerator PerformAttack()
    {
        canAttack = false;
        PlayerAction playerComponent = player.GetComponent<PlayerAction>();
        if (playerComponent != null)
        {
            MusicManager.Instance.PlayAttackGoblin();
            playerComponent.TakeDamage(damage);
            Debug.Log("Nemico ha attaccato il giocatore!");
        }

        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }

     protected void MoveTowardsPlayer()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > agent.stoppingDistance)
            {
                agent.SetDestination(player.position);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthBar((float)health,(float)maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Nemico ucciso: " + gameObject.name);
        SaveScore.KillMonster();
        SimpleInventory playerInventory = FindObjectOfType<SimpleInventory>();
        if (CompareTag("Boss"))
        {
           coinValue=100;
           int playerMaxHealth=PlayerManager.Instance.GetMaxHealth();
           PlayerManager.Instance.SetMaxHealth(playerMaxHealth+50);
           playerInventory.AddWeapon(Weapon.WeaponType.LegendarySword);
           Debug.Log("Hai ricevuto una nuova arma: Legendary Sword!");
        }
        playerInventory.AddCoin(coinValue);
        EnemyManager.Instance.RemoveEnemy(this);
        Destroy(gameObject);
    }


    public int GetMonsterHealth()
    {
        return health;
    }

    public int GetMonsterMaxHealth()
    {
        return maxHealth;
    }


    public int GetMonsterDamage()
    {
        return damage;
    }

    public int GetMonsterAttackCoolDown()
    {
        return attackCoolDown;
    }

    public void SetMonsterHealth(int number)
    {
        health = number;
    }
    public void SetMonsterMaxHealth(int number)
    {
        maxHealth = number;
    }

     public void SetMonsterDamage(int number)
    {
        damage = number;
    }

     public void SetMonsterAttackCoolDown(int number)
    {
        attackCoolDown= number;
    }

     public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        float value = currentHealth / maxHealth;
        lifeBar.fillAmount= value;
    }

    public void ToggleStatsLoaded(){
       statsLoaded= !statsLoaded;
    }
}
