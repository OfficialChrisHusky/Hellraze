using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player instance;

    private void Awake() {

        if(instance == null) {

            instance = this;

        }
        
    }

    [Header("Scripts")]
    public PlayerMovement movement;
    public PlayerLook cameraLook;

    [Header("Player Variables")]
    public bool canMove;
    public bool canLook;

    public GameObject playerCamera;
    public GameObject playerBody;

    public Weapon currentWeapon;
    public Transform weaponHolder;

    private void Start() {

        //

    }

    private void Update() {

        if(currentWeapon != null && Input.GetMouseButtonDown(0)) {

            currentWeapon.Shoot();

        }

        if(currentWeapon != null && Input.GetKeyDown(KeyCode.R)) {

            StartCoroutine(currentWeapon.Reload());

        }

    }

    public void InstantiateWeapon(GameObject weapon) {

        currentWeapon = Instantiate(weapon, weaponHolder).GetComponent<Weapon>();

    }

}
