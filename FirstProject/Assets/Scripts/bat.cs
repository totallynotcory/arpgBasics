using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bat : Creature {

	Vector2 movement;
	int moveDelay;
	public int initMoveDelay;

	protected override void Start () {
		maxHP = 1;
		baseMoveSpeed = 3;
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		moveDelay = initMoveDelay;
		base.Start ();
	}

	protected override void Update () {
		base.Update();
		if (moveDelay > 0) {
			--moveDelay;
		} else {
			movement = chooseRandomDirection();
			moveDelay = initMoveDelay;
		}
		rb2d.MovePosition(rb2d.position + movement * baseMoveSpeed * Time.deltaTime);
	}

	Vector2 chooseRandomDirection(){

		int direction = Random.Range (0, 4);

		switch (direction) {
		case 0:
			return new Vector2 (0, 1);
		case 1:
			return new Vector2 (1, 0);
		case 2:
			return new Vector2 (0, -1);
		case 3:
			return new Vector2 (-1, 0);
		default:
			return new Vector2 (0, 0);
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "playerArrow") {
			takeDamage(1);
		}
	}




}
