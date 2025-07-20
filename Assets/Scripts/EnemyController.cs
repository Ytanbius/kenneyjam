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
    public bool canAttack = false;
    public GameObject target;
    public GameObject battleController;
    public BattleController bCtrl;
    private void Start()
    {
        bCtrl = battleController.GetComponent<BattleController>();
    }
    private void Update()
    {
        Attack();
        CheckStatus();
    }
    void Attack()
    {
        HeroController eCrtl = target.GetComponent<HeroController>();
        if (!cooldownB && canAttack)
        {
            eCrtl.heroHP -= enemyDMG;
            StartCoroutine(atkCooldown());
        }
    }
    void CheckStatus()
    {
        if(enemyHP <= 0)
        {
            enemyHP = 0;
            bCtrl.destroyTarget(gameObject);
        }
    }
    IEnumerator atkCooldown()
    {
        cooldownB = true;
        yield return new WaitForSeconds((int)(30 / enemySPD));
        cooldownB = false;
    }
    public void genType(int rounds)
    {
        switch (enemyType)
        {
            case 0: //minion
                enemyHP = 1 + (enemyHP + enemyHP * ((int)(rounds * 0.2)));
                enemyDMG = 1 + (enemyDMG + enemyDMG * ((int)(rounds * 0.2)));
                enemySPD = 1 + (enemySPD + enemySPD * ((int)(rounds * 0.2)));
                break;
            case 1: //warrior
                enemyHP = 1 + (enemyHP + enemyHP * ((int)(rounds * 0.5)));
                enemyDMG = 1 + (enemyDMG + enemyDMG * ((int)(rounds * 0.2)));
                enemySPD = 1 + (enemySPD + enemySPD * ((int)(rounds * 0.2)));
                break;
            case 2: //ranger
                enemyHP = 1 + (enemyHP + enemyHP * ((int)(rounds * 0.2)));
                enemyDMG = 1 + (enemyDMG + enemyDMG * ((int)(rounds * 0.2)));
                enemySPD = 1 + (enemySPD + enemySPD * ((int)(rounds * 0.5)));
                break;
            case 3: //mage
                enemyHP = 1 + (enemyHP - enemyHP * ((int)(rounds * 0.5)));
                enemyDMG = 1 + (enemyDMG + enemyDMG * ((int)(rounds * 1)));
                enemySPD = 1 + (enemySPD - enemySPD * ((int)(rounds * 0.5)));
                break;
        }
    }
}
