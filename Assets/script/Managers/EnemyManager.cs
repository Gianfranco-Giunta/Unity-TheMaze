using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyManager: MonoBehaviour
{
    public static EnemyManager Instance;
    public Transform[] spawnPoints;
    public Transform[] bossSpawnPoints;
    public GameObject normalEnemyPrefab;
    public GameObject bossEnemyPrefab;
    public List<Nemici> enemyList = new List<Nemici>();
    public int maxEnemiesInGame = 15;
    public float spawnInterval = 15f;
    private int lastIndexBoss=1;

    void Awake()
    {
        Instance=this;

    }
    
    void Start(){
     if(!GameManager.Instance.GetBool()){
        StartCoroutine(SpawnEnemiesPeriodically());
     }
     else{
        StartCoroutine(LoadMonsters());
     }
    }


    private IEnumerator SpawnEnemiesPeriodically()
    {
        while (true)
        {
            if (enemyList.Count < maxEnemiesInGame)
            {
                 this.SpawnEnemies(spawnPoints, normalEnemyPrefab, bossEnemyPrefab, bossSpawnPoints);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    public  void AddEnemy(Nemici enemy)
    {
        enemyList.Add(enemy);
    }

    public void RemoveEnemy(Nemici enemy)
    {
        enemyList.Remove(enemy);
    }

    public void SaveMonsters()
    {
        PlayerPrefs.SetInt("MonsterCount", enemyList.Count);
        Debug.Log(enemyList.Count);
        for (int i = 0; i < enemyList.Count; i++)
        {
            Nemici enemy = enemyList[i];
            if (enemy != null)
            {
               PlayerPrefs.SetFloat($"Monster{i}_PosX", enemy.transform.position.x);
               PlayerPrefs.SetFloat($"Monster{i}_PosY", enemy.transform.position.y);
               PlayerPrefs.SetFloat($"Monster{i}_PosZ", enemy.transform.position.z);
               PlayerPrefs.SetInt($"Monster{i}_Health", enemy.GetMonsterHealth());
               PlayerPrefs.SetInt($"Monster{i}_MaxHealth", enemy.GetMonsterMaxHealth());
               PlayerPrefs.SetInt($"Monster{i}_Damage", enemy.GetMonsterDamage());
               PlayerPrefs.SetInt($"Monster{i}_AttackCoolDown", enemy.GetMonsterAttackCoolDown());
               int enemyType = (enemy.CompareTag("Boss")) ? 1 : 0;
               PlayerPrefs.SetInt($"Monster{i}_Type", enemyType); 
            }
        }
        Debug.Log($"Salvando {enemyList.Count} mostri.");
        PlayerPrefs.Save();
    }

    private IEnumerator LoadMonsters()
    {
        int monsterCount = PlayerPrefs.GetInt("MonsterCount", 0);
        enemyList.Clear();

        for (int i = 0; i < monsterCount; i++)
        {

            float x = PlayerPrefs.GetFloat($"Monster{i}_PosX");
            float y = PlayerPrefs.GetFloat($"Monster{i}_PosY");
            float z = PlayerPrefs.GetFloat($"Monster{i}_PosZ");
            int health = PlayerPrefs.GetInt($"Monster{i}_Health");
            int maxHealth = PlayerPrefs.GetInt($"Monster{i}_MaxHealth");
            int damage = PlayerPrefs.GetInt($"Monster{i}_Damage");
            int attackCoolDown= PlayerPrefs.GetInt($"Monster{i}_AttackCoolDown");
            int enemyType = PlayerPrefs.GetInt($"Monster{i}_Type");
            GameObject prefabToInstantiate = (enemyType == 1) ? bossEnemyPrefab : normalEnemyPrefab;
            GameObject newEnemy = Instantiate(prefabToInstantiate, new Vector3(x, y, z), Quaternion.identity);
            Nemici enemyComponent = newEnemy.GetComponent<Nemici>();
            enemyComponent.SetMonsterHealth(health);
            enemyComponent.SetMonsterMaxHealth(maxHealth);
            enemyComponent.SetMonsterDamage(damage);
            enemyComponent.SetMonsterAttackCoolDown(attackCoolDown);
            enemyComponent.UpdateHealthBar(health,maxHealth);
            enemyComponent.ToggleStatsLoaded();
            enemyList.Add(enemyComponent);
        }

        Debug.Log($"Caricando {monsterCount} mostri.");

        yield return new WaitForSeconds(spawnInterval);

        StartCoroutine(SpawnEnemiesPeriodically());

    }

    public int GetLastIndex(){
         return lastIndexBoss;
    }

    public void SetLastIndex(int i){
         lastIndexBoss=i;
    }
}
