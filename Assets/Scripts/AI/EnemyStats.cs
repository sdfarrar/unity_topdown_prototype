using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="AI/EnemyStats")]
public class EnemyStats : ScriptableObject {

    public float MoveSpeed = 1;

    public float ViewRadius = 3.5f;
    public float ViewAngle = 60f;
    public float SearchDuration = 4f;

    public float AttackRange = 1.0f;
    public float AttackRate = 0.5f;
    public int AttackDamage = 10;
    public int ContactDamage = 20;


}
