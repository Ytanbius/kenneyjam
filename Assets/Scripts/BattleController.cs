using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class BattleController : MonoBehaviour
{
    //Listas
    public List<GameObject> heroes;
    public List<GameObject> enemies;
    //Arrays
    public Transform[] heroesPos;
    public Transform[] enemiesPos;
    public GameObject[] heroesChoice;
    public GameObject[] enemiesChoice;
    public GameObject[] enemyModels;
    public GameObject[] heroesModels;
    public string[] Elements;
    //Misc
    public Camera cam;
    public Transform pos1;
    public Transform pos2;
    public GameObject enemyObj;
    public GameObject heroObj;
    public GameObject player;
    public GameObject enemyTarget;
    public GameObject heroTarget;
    private bool battle = false;
    private bool cdBool = false;
    private float cdTime = 2;
    public int round = 1;
    private void OnTriggerStay(Collider other)
    {
        string name = other.gameObject.name;
        battleButton(name);
    }
    IEnumerator cooldown()
    {
        cdBool = true;
        yield return new WaitForSeconds(cdTime);
        cdBool = false;
    }
    void battleButton(string name)
    {
        PlayerController pCtrl = player.GetComponent<PlayerController>();
        if (name == "Player" && Input.GetKey("e") && !battle && !cdBool && enemies.Count != 0)
        {
            RoundStart();
            StartCoroutine(cooldown());
        }
        else if (Input.GetKey("e") && battle && !cdBool)
        {
            pCtrl.enabled = true;
            cam.transform.position = pos1.position;
            cam.transform.rotation = pos1.rotation;
            battle = false;
            StartCoroutine(cooldown());
        }
    }
    public void generateButton(int button)
    {
        if (enemies.Count == 0 && heroes.Count == 0 && button == 0)
        {
            int num = Random.Range(1, 7);
            for (int i = 0; i < num; i++)
            {
                enemies.Add(Instantiate(enemyObj, new Vector3(0, -5, 0), Quaternion.Euler(0, 0, 0)));
                genRandomStats(i, true, enemies[i]);
                getModel(enemies[i], enemies[i].GetComponent<EnemyController>().enemyType);
            }
            for (int i = 0; i < num; i++)
            {
                heroes.Add(Instantiate(heroObj, new Vector3(0, -5, 0), Quaternion.Euler(0, 0, 0)));
                genRandomStats(i, true, heroes[i]);
                getModel(heroes[i], heroes[i].GetComponent<HeroController>().heroClass);
            }
        }
        else if (button == 0)
        {
            for (int i = 0; i < enemies.Count; i++)
                Destroy(enemies[i]);
            for (int i = 0; i < heroes.Count; i++)
                Destroy(heroes[i]);
            enemies.Clear();
            heroes.Clear();
            generateButton(0);
        }
        if (button == 1)
        {
            enemies.Add(Instantiate(enemyObj, new Vector3(0, -5, 0), Quaternion.Euler(0, 0, 0)));
            getModel(enemies[enemies.Count-1], Random.Range(0, enemyModels.Length));
            genRandomStats(enemies.Count - 1, true, enemies[enemies.Count - 1]);
        }
        if (enemies.Count != 0 && button == 2)
        {
            Destroy(enemies[enemies.Count - 1]);
            enemies.Remove(enemies[enemies.Count - 1]);
        }
    }
    public void genRandomStats(int i, bool element, GameObject target)
    {
        if (target.GetComponent<EnemyController>() != null)
        {
            EnemyController eCtrl = enemies[i].GetComponent<EnemyController>();
            eCtrl.enemyHP = Random.Range(5, 21);
            eCtrl.enemyDMG = Random.Range(1, 6);
            eCtrl.enemySPD = Random.Range(1, 6);
            if (element)
                eCtrl.enemyElement = Elements[Random.Range(0, Elements.Length)];
            else
                eCtrl.enemyElement = Elements[0];
            eCtrl.enemyType = Random.Range(0, enemyModels.Length);
            eCtrl.genType(round - 1);
        }
        else if (target.GetComponent<HeroController>() != null)
        {
            HeroController hCtrl = heroes[i].GetComponent<HeroController>();
            hCtrl.heroHP = Random.Range(5, 21);
            hCtrl.heroDMG = Random.Range(1, 6);
            hCtrl.heroSPD = Random.Range(1, 6);
            if (element)
                hCtrl.heroElement = Elements[Random.Range(0, Elements.Length)];
            else
                hCtrl.heroElement = Elements[0];
            hCtrl.heroClass = Random.Range(0, heroesModels.Length);
            hCtrl.genClass(round - 1);
        }
    }
    void RoundStart()
    {
        PlayerController pCtrl = player.GetComponent<PlayerController>();
        Animator pAnim = player.GetComponentInChildren<Animator>();
        if (!battle && enemies.Count != 0 && heroes.Count !=0)
        {
            pAnim.SetInteger("moving", 0);
            pCtrl.enabled = false;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (i < 3)
                {
                    EnemyController eCtrl = enemies[i].GetComponent<EnemyController>();
                    enemiesChoice[i] = enemies[i];
                    enemiesChoice[i].transform.position = enemiesPos[i].transform.position;
                    enemiesChoice[i].transform.rotation = enemiesPos[i].transform.rotation;
                    eCtrl.canAttack = true;
                    eCtrl.target = heroesChoice[0];
                }
            }
            for (int i = 0; i < heroes.Count; i++)
            {
                if (i < 3)
                {
                    HeroController hCtrl = heroes[i].GetComponent<HeroController>();
                    heroesChoice[i] = heroes[i];
                    heroesChoice[i].transform.position = heroesPos[i].transform.position;
                    heroesChoice[i].transform.rotation = heroesPos[i].transform.rotation;
                    hCtrl.canAttack = true;
                    hCtrl.target = enemiesChoice[0];
                }
            }
            cam.transform.position = pos2.position;
            cam.transform.rotation = pos2.rotation;
            battle = true;
        }
    }
    void PreparePhase()
    {

    }
    void RoundEnd()
    {

    }
    void GenEnemies()
    {
        int roundEnemiesQnt = 3;
        if(round > 1)
            roundEnemiesQnt += Random.Range(0 + round, 2 * round + 1);
        for (int i = 0; i < roundEnemiesQnt; i++)
        {
            enemies.Add(Instantiate(enemyObj, new Vector3(0, -5, 0), Quaternion.Euler(0, 0, 0)));
            if (round != 1)
            {
                genRandomStats(i, true, enemies[1]);
            }
            else
            {
                genRandomStats(i, false, enemies[1]);
            }
        }
    }
    void GenHeroes()
    {
        
    }
    public void getModel(GameObject side, int modelID)
    {
        if (side.GetComponent<EnemyController>() != null)
        {
            Debug.Log("enemyModel: " + modelID);
            if (side.transform.GetChild(0).childCount > 0)
            {
                Destroy(side.transform.GetChild(0).gameObject);
            }
            GameObject model = Instantiate(enemyModels[modelID], side.transform.position, Quaternion.identity);
            model.transform.SetParent(side.transform);
            model.layer = 0;
        }
        else if (side.GetComponent<HeroController>() != null)
        {
            Debug.Log("heroModel: " + modelID);
            if (side.transform.GetChild(0).childCount > 0)
            {
                Destroy(side.transform.GetChild(0).gameObject);
            }
            GameObject model = Instantiate(heroesModels[modelID], side.transform.position, Quaternion.identity);
            model.transform.SetParent(side.transform);
            model.layer = 0;
        }
        else
            Debug.LogError("ERROR getModel");
    }
    public void newTarget(GameObject target)
    {
        if (target.GetComponent<EnemyController>() != null)
        {
            for (int i = 0; i < heroes.Count; i++)
            {
                HeroController hCtrl = heroes[i].GetComponent<HeroController>();
                hCtrl.target = target;
            }
        }
        else if (target.GetComponent<HeroController>() != null)
        {

        }
    }
    public void destroyTarget(GameObject target)
    {
        if (target.GetComponent<EnemyController>() != null)
        {
            newTarget(enemies[1]);
            Destroy(enemies[0]);
            enemies.Remove(enemies[0]);
        }
        else if (target.GetComponent<HeroController>() != null)
        {
            newTarget(heroes[1]);
            Destroy(heroes[0]);
            heroes.Remove(heroes[0]);
        }
    }
    public void newPos(GameObject target)
    {
        if (target.GetComponent<EnemyController>() != null)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (i < 3)
                {
                    EnemyController eCtrl = enemies[i].GetComponent<EnemyController>();
                    enemiesChoice[i] = enemies[i];
                    enemiesChoice[i].transform.position = enemiesPos[i].transform.position;
                    enemiesChoice[i].transform.rotation = enemiesPos[i].transform.rotation;
                    eCtrl.canAttack = true;
                }
            }
        }
        else if (target.GetComponent<HeroController>() != null)
        {
            for (int i = 0; i < heroes.Count; i++)
            {
                if (i < 3)
                {
                    HeroController eCtrl = heroes[i].GetComponent<HeroController>();
                    heroesChoice[i] = heroes[i];
                    heroesChoice[i].transform.position = heroesPos[i].transform.position;
                    heroesChoice[i].transform.rotation = heroesPos[i].transform.rotation;
                    eCtrl.canAttack = true;
                }
            }
        }

    }
}
