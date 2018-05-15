using UnityEngine;

public class Bow : Weapon {

	public Arrow arrow;


	public override int GetIndex(){
		return 1;
	}

	public override void PrimaryAttack(Vector2 direction) {
		ShootArrow(direction);
	}

	public override void AlternateAttack(Vector2 direction) {
		PrimaryAttack(direction);
	}

	private void ShootArrow(Vector2 direction){
		Vector2 spawnPoint = GetSpawnPoint(direction);
		Quaternion rotation = Quaternion.Euler(GetRotation(direction));
		Arrow instance = Instantiate<Arrow>(arrow, spawnPoint, rotation);
		instance.direction = direction;
	}

	private Vector2 GetSpawnPoint(Vector2 direction){
		if(direction==Vector2.up){ return new Vector2(transform.position.x, transform.position.y) + Vector2.up; }
		if(direction==Vector2.down){ return new Vector2(transform.position.x, transform.position.y) + Vector2.down; }
		if(direction==Vector2.left){ return new Vector2(transform.position.x, transform.position.y) + Vector2.left; }
		if(direction==Vector2.right){ return new Vector2(transform.position.x, transform.position.y) + Vector2.right; }
		return Vector2.zero;
	}

	private Vector3 GetRotation(Vector2 direction){
		// Surely there's a better way of gettings these
		if(direction==Vector2.up){ return new Vector3(0, 0, 90); }
		if(direction==Vector2.down){ return new Vector3(0, 0, -90); }
		if(direction==Vector2.left){ return new Vector3(0, 0, 180); }
		if(direction==Vector2.right){ return new Vector3(0, 0, 0); }
		return Vector3.zero;
	}

}
