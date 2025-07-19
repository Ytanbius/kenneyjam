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
    public int enemyType;
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
    }
    public void genType(int rounds)
    {
        rounds -= 1;
        switch (enemyType)
        {
            case 1: //minion
                enemyHP += enemyHP * ((int)(rounds * 0.2));
                enemyDMG += enemyDMG * ((int)(rounds * 0.2));
                enemySPD += enemySPD * ((int)(rounds * 0.2));
                break;
            case 2: //warrior
                enemyHP += enemyHP * ((int)(rounds * 0.5));
                enemyDMG += enemyDMG * ((int)(rounds * 0.2));
                enemySPD += enemySPD * ((int)(rounds * 0.2));
                break;
            case 3: //ranger
                enemyHP += enemyHP * ((int)(rounds * 0.2));
                enemyDMG += enemyDMG * ((int)(rounds * 0.2));
                enemySPD += enemySPD * ((int)(rounds * 0.5));
                break;
            case 4: //mage
                enemyHP -= enemyHP * ((int)(rounds * 0.5));
                enemyDMG += enemyDMG * ((int)(rounds * 1));
                enemySPD -= enemySPD * ((int)(rounds * 0.5));
                break;
        }
    }
}
