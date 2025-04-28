using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float sensitivity;
    private bool isInvertLook;

    void Start(){
        sensitivity=5;
        speed=60;
        Cursor.lockState = CursorLockMode.Locked;
        isInvertLook = PlayerPrefs.GetInt("InvertLook", 0) == 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerManager.Instance.playerAction.Attack();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerManager.Instance.SavePlayer();
            EnemyManager.Instance.SaveMonsters();
            PlayerPrefs.SetInt("Difficulty", GameManager.Instance.GetDifficulty());
            SceneChanger.ChangeSceneMenu();
        }

        if (Input.GetKeyDown(KeyCode.Space)){
            PlayerManager.Instance.inventory.ToggleInventory();
        }

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            PlayerManager.Instance.inventory.ChangeWeapon();
        }

        if (Input.GetKeyDown(KeyCode.F)) 
        {
            PlayerManager.Instance.inventory.BuyHeal();
        }


        float moveHorizontal= Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement= Camera.main.transform.forward;
        movement.y=0f;
        Vector3 movementRight = Camera.main.transform.right;
        Vector3 move=(movement * moveVertical + movementRight * moveHorizontal);
        transform.Translate( move*speed*Time.deltaTime);

        float mouseX= Input.GetAxis("Mouse X") * sensitivity;
        float mouseY= Input.GetAxis("Mouse Y") * sensitivity;

        if (isInvertLook)
        {
        mouseX = -mouseX;
        }

        Camera.main.transform.Rotate(transform.up * mouseX);
    }
}

