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

    [Header("Weapons")]
    public Weapon currentWeapon;
    public int currentWeaponIndex;
    public int nextWeaponIndex;
    public List<Weapon> weapons = new List<Weapon>();

    [Header("GameObjects")]
    public GameObject playerCamera;
    public GameObject playerBody;

    [Header("Transforms")]
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
        if(weapons.Count > 1 && Input.GetAxisRaw("Mouse ScrollWheel") != 0) {

            ChangeWeapons(Input.GetAxisRaw("Mouse ScrollWheel"));

        }

    }

    public void InstantiateWeapon(GameObject weapon) {

        if(currentWeapon != null) {

            currentWeapon.gameObject.SetActive(false);

        }

        currentWeapon = Instantiate(weapon, weaponHolder).GetComponent<Weapon>();
        currentWeaponIndex = weapons.Count;
        weapons.Add(currentWeapon);

    }

    private void ChangeWeapons(float direction) {

        if (direction >= 0.1f) {

            nextWeaponIndex = currentWeaponIndex + 1;

            if(currentWeaponIndex == weapons.Count - 1) {

                nextWeaponIndex = 0;

            }

        } else if(direction <= -0.1f) {

            nextWeaponIndex = currentWeaponIndex - 1;

            if (currentWeaponIndex == 0) {

                nextWeaponIndex = weapons.Count - 1;

            }

        }

        currentWeapon.gameObject.SetActive(false);
        currentWeapon = weapons[nextWeaponIndex];
        currentWeapon.gameObject.SetActive(true);
        currentWeaponIndex = weapons.IndexOf(currentWeapon);

    }

}
