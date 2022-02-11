using System.Collections;
using UnityEngine;

public class Damagable : MonoBehaviour {

    [SerializeField] private int health = 100;

    [SerializeField] private AudioSource hitClip;
    [SerializeField] private AudioSource breakClip;

    public void Hit(int damage) {

        hitClip.Play();
        health -= damage;

        if(health <= 0) {

            Die();

        }

    }

    private void Die() {

        breakClip.Play();

        Debug.Log(gameObject.name + " destroyed");
        Destroy(gameObject);

        if(transform.parent != null) {

            Destroy(transform.parent.gameObject, 5);

        }

    }

}