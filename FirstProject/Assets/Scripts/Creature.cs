using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour {

	public enum state {alive, dead};
	public enum facing {up, down, left, right};

	protected Rigidbody2D rb2d;
	protected Animator animator;

	// Common stats
	protected int currentHP,
					maxHP,
					baseOffense,
					baseDefense;

	protected float baseMoveSpeed,
					baseAttackSpeed,
					nextAttackTime;

	public state myState = state.alive;
	public facing creatureFacing = facing.down;

	// Use this for initialization
	protected virtual void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		currentHP = maxHP;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if (myState == state.dead) {
			Destroy(this, 3.0f);
			return;
		} else if (currentHP == 0){
			myState = state.dead;
			return;
		}
	}

	protected virtual void takeDamage(int damage){
		currentHP = currentHP - damage;
		if (currentHP <= 0) {
			myState = state.dead;
		}
	}

	protected int getOffense(){
		return baseOffense;
	}

	protected int getDefense(){
		return baseDefense;
	}
}
