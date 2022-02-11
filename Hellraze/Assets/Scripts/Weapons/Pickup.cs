using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    enum Type {

        WeaponPickup,
        AmmoPickup,
        HealthPickup

    }

    [SerializeField] private Type type;

    [SerializeField] private GameObject pickup;

    private void OnTriggerEnter(Collider other) {
        
        if(other.tag == "Player") {

            if(type == Type.WeaponPickup) {

                if (pickup != null) {

                    Player.instance.InstantiateWeapon(pickup);
                    Destroy(gameObject);

                    return;

                }

                Debug.LogError("Pickup is null!");

            }

        }

    }

}