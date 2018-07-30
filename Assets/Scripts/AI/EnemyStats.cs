using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="AI/EnemyStats")]
public class EnemyStats : ScriptableObject {

    public float MoveSpeed = 1;

    public int ContactDamage = 20;
    public int AttackDamage = 10;

    public float ViewRadius = 3.5f;
    public float ViewAngle = 60f;

    public float SearchDuration = 4f;

}
