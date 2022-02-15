using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [SerializeField] private bool melee;

    [Header("Firing")]
    [SerializeField] private int damage;
    [SerializeField] private int maxDistance;

    [Header("Ammo")]
    [SerializeField] private int ammo;
    [SerializeField] private int maxAmmo;
    [SerializeField] private int ammoInInventory;

    [Header("Reloading")]
    [SerializeField] private float reloadTime;

    [Header("Audio")]
    [SerializeField] private AudioSource fireClip;
    [SerializeField] private AudioSource emptyClip;
    [SerializeField] private AudioSource reloadClip;

    [Header("Bools")]
    [SerializeField] private bool canShoot = true;

    public void Shoot() {

        if(ammo <= 0) { emptyClip.Play(); return; }
        if(!canShoot) { return; }

        if (!melee) {

            ammo--;

        }

        RaycastHit hit;

        if(Physics.Raycast(Player.instance.playerCamera.transform.position,
                           Player.instance.playerCamera.transform.forward,
                           out hit, maxDistance)) {
            try {

                Damagable damagable = hit.collider.gameObject.GetComponent<Damagable>();

                damagable.Hit(damage);

            } catch(Exception e) {}

            fireClip.Play();

        }

    }

    public IEnumerator Reload() {

        if (ammo < maxAmmo && ammoInInventory != 0 && !melee) {

            canShoot = false;

            reloadClip.Play();
            yield return new WaitForSeconds(reloadClip.clip.length);

            if(ammoInInventory >= maxAmmo) { //Player has MORE ammo in inventory then the weapons maxAmmo

                ammoInInventory -= maxAmmo - ammo;
                ammo = maxAmmo;

            } else { //Player has LESS ammo in inventory then the weapons maxAmmo

                int amount = maxAmmo - ammo;

                if(ammoInInventory - amount >= 0) {

                    ammoInInventory -= amount;
                    ammo += amount;

                } else {

                    ammo += ammoInInventory;
                    ammoInInventory = 0;

                }

            }

            canShoot = true;

        }

        yield return null;

    }

}