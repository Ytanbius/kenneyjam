using System.Collections;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    //STATS
    public float heroHP;
    public float heroDMG;
    public float heroSPD;
    //INFO
    public string heroElement;
    public int heroClass;
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
        EnemyController eCrtl = target.GetComponent<EnemyController>();
        if (!cooldownB && canAttack)
        {
            eCrtl.enemyHP -= heroDMG;
            StartCoroutine(atkCooldown());
        }
    }
    void CheckStatus()
    {
        if (heroHP <= 0)
        {
            heroHP = 0;
            bCtrl.destroyTarget(gameObject);
        }
    }
    IEnumerator atkCooldown()
    {
        cooldownB = true;
        yield return new WaitForSeconds((int)(30 / heroSPD));
        cooldownB = false;
    }
    public void genClass(int rounds)
    {
        switch (heroClass)
        {
            case 0: //warrior
                heroHP = 1 + (heroHP + heroHP * ((int)(rounds * 0.2)));
                heroDMG = 1 + (heroDMG + heroDMG * ((int)(rounds * 0.2)));
                heroSPD = 1 + (heroSPD + heroSPD * ((int)(rounds * 0.2)));
                break;
            case 1: //berserk
                heroHP = 1 + (heroHP + heroHP * ((int)(rounds * 0.2)));
                heroDMG = 1 + (heroDMG + heroDMG * ((int)(rounds * 0.5)));
                heroSPD = 1 + (heroSPD + heroSPD * ((int)(rounds * 0.2)));
                break;
            case 2: //ranger
                heroHP = 1 + (heroHP + heroHP * ((int)(rounds * 0.2)));
                heroDMG = 1 + (heroDMG + heroDMG * ((int)(rounds * 0.2)));
                heroSPD = 1 + (heroSPD + heroSPD * ((int)(rounds * 0.5)));
                break;
        }
    }
}
