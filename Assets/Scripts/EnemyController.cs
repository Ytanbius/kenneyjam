using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //STATS
    public float enemyHP;
    public float enemyDMG;
    public float enemySPD;
    //INFO
    public string enemyElement;
    public string enemyType;
    public string enemyName;
    //Arrays
    public float[] dmgVar;
    public float[] spdVar;
    public float[] hpVar;
    //MISC
    public bool cooldownB = false;
    IEnumerator atkCooldown()
    {
        cooldownB = true;
        yield return new WaitForSeconds(100 / enemySPD);
        cooldownB = false;
    }}
