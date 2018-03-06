using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : Creature {

	protected bool facingRight = false;
	public GameObject arrowPrefab;

	protected override void Start(){
		arrowPrefab = GameObject.Find ("arrow");
		baseMoveSpeed = 5;
		baseAttackSpeed = 1;
		maxHP = 1;
		base.Start();
    }

    // Update is called once per frame
	protected override void Update(){
		base.Update();
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
		rb2d.MovePosition(rb2d.position + movement * baseMoveSpeed * Time.deltaTime);

        playerAnimation(movement, moveHorizontal, moveVertical);

		if (Input.GetKey ("z")) {
			LaunchAttack();
		}
    }

    void playerAnimation(Vector2 movement, float moveHorizontal, float moveVertical){

        // If our hero is moving, set him to moving, else stop him moving.
        if (movement != Vector2.zero) {
            animator.SetBool("Moving", true);
        }
        else {
            animator.SetBool("Moving", false);
        }

        //  Horizontal stuff
        if (moveHorizontal < 0) {
            if (facingRight) { flip(); }
            animator.SetBool("MovingHoriz", true);
			creatureFacing = facing.right;
        } else if (moveHorizontal > 0) {
            if (!facingRight) { flip(); }
            animator.SetBool("MovingHoriz", true);
			creatureFacing = facing.left;
        } else if (moveHorizontal == 0) {
            animator.SetBool("MovingHoriz", false);
        }

        //  Vertical stuff
        if (moveVertical > 0) {
            animator.SetBool("MovingUp", true);
            animator.SetBool("MovingDown", false);
			creatureFacing = facing.up;
        } else if (moveVertical < 0) {
            animator.SetBool("MovingDown", true);
            animator.SetBool("MovingUp", false);
			creatureFacing = facing.down;
        } else if (moveVertical == 0) {
            animator.SetBool("MovingDown", false);
            animator.SetBool("MovingUp", false);
        }
    }

    void flip(){
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }



	private void LaunchAttack(){
		if (Time.time <= nextAttackTime){
			return;
		}

		nextAttackTime = Time.time + baseAttackSpeed;

		// Create the attack object
		Vector3 offset;
		Vector2 velocity;

		switch (creatureFacing) {
		case facing.up:
			offset = new Vector3 (0.0f, 1.0f, 0.0f);
			velocity = new Vector2 (0.0f, 8.0f);
			break;
		case facing.left:
			offset = new Vector3 (1.0f, 0.0f, 0.0f);
			velocity = new Vector2 (8.0f, 0.0f);
			break;
		case facing.down:
			offset = new Vector3 (0.0f, -1.0f, 0.0f);
			velocity = new Vector2 (0.0f, -8.0f);
			break;
		case facing.right:
			offset = new Vector3 (-1.0f, 0.0f, 0.0f);
			velocity = new Vector2 (-8.0f, 0.0f);
			break;
		default:
			print ("Invalid direction in Player::LaunchAttack()! :(");
			offset = new Vector3 (0.0f, 0.0f, 0.0f);
			velocity = new Vector2 (0.0f, 0.0f);
			break;
		}
		//Vector3 offset = new Vector3(1.0f, 1.0f, 0.0f);
		GameObject attack = (GameObject)Instantiate(arrowPrefab,
			rb2d.transform.position + (rb2d.transform.rotation * offset),
			rb2d.transform.rotation);

		// Give the attack some speed
		attack.GetComponent<Rigidbody2D>().velocity = velocity;
		attack.gameObject.tag = "playerBullet";

		// Destroy the attack
		Destroy(attack, 3.0f);
	}


}