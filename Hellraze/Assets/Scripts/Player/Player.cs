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

    private void Update() {

        //

    }

}
