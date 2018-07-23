using UnityEngine;
using UnityEngine.Events;

//TODO perhaps this is better a GameEventListener that listens for player health changes,
//then move damaged event to damagedealer script
public class PlayerHealth : MonoBehaviour, IDamageable {

	public IntegerVariable HP;
	public bool ResetHP;
	public IntegerVariable MaxHP;

	[Tooltip("Event to raise when being healed")]
	public UnityEvent HealEvent;
	[Tooltip("Event to raise when being damaged")]
	public UnityEvent DamageEvent;
	[Tooltip("Event to raise upon death")]
	public UnityEvent DeathEvent;

	private void Start () {
		if(ResetHP){ HP.SetValue(MaxHP); }
	}
	
	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	private void OnTriggerEnter2D(Collider2D other) {
		//DamageDealer damage = other.gameObject.GetComponent<DamageDealer>();
		//if(damage!=null){ TakeDamage(damage); }

		HealthReplenisher replenisher = other.gameObject.GetComponent<HealthReplenisher>();
		if(replenisher!=null){ GetHealth(replenisher); }
	}

	public void TakeDamage(DamageDealer damage){
		HP.ApplyChange(-damage.DamageAmount);
		if(HP.Value>0){
			DamageEvent.Invoke();
		}else{
			HP.Value = 0;
			Debug.Log("PLAYERDED");
			DeathEvent.Invoke();
		}
	}

	private void GetHealth(HealthReplenisher replenisher){
		HP.ApplyChange(replenisher.HealAmount);
		if(HP.Value>MaxHP.Value){ HP.SetValue(MaxHP); }

		HealEvent.Invoke();
	}
}
