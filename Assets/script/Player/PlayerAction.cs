using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    public LayerMask enemyLayer; 
    public Transform attackPoint; 
    private bool canAttack = true;
    [SerializeField] private Image lifeBar;

    public void Attack()
    {
        if (canAttack)
        { 
            StartCoroutine(PerformAttack());
        }
    }

    private IEnumerator PerformAttack()
    {
        canAttack = false;
        Nemici closestEnemy = GetClosestEnemy();
        if (closestEnemy != null)
        {
            Debug.Log("Ciao");
            MusicManager.Instance.PlayAttackPlayer();
            closestEnemy.TakeDamage(PlayerManager.Instance.GetAttackDamage());
            Debug.Log("Attaccato: " + closestEnemy.name);
        }
        yield return new WaitForSeconds(PlayerManager.Instance.GetAttackCoolDown());
        canAttack = true;
    }

    private Nemici GetClosestEnemy()
    {
        Nemici closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Nemici enemy in EnemyManager.Instance.enemyList)
        {
            if (enemy != null)
            {
                float distance = Vector3.Distance(attackPoint.position, enemy.transform.position);
                if (distance < closestDistance && distance <= PlayerManager.Instance.GetAttackRange())
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }
        return closestEnemy;
    }

    public void TakeDamage(int damage)
    {
        PlayerManager.Instance.SetPlayerHealth(PlayerManager.Instance.GetPlayerHealth() - damage);
        UpdateHealthBar(PlayerManager.Instance.GetPlayerHealth(),PlayerManager.Instance.GetMaxHealth());
        if (PlayerManager.Instance.GetPlayerHealth() <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Il giocatore è morto!");
        PlayerManager.Instance.inventory.ResetCoin();
        GameManager.Instance.EndGame();
    }

     public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        float value = currentHealth / maxHealth;
        lifeBar.fillAmount= value;
    }
}
