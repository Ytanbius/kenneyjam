using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public string[] Elements;
    //Misc
    public Camera cam;
    public Transform pos1;
    public Transform pos2;
    public GameObject enemyOBJ;
    public GameObject player;
    private bool battle = false;
    private bool cooldownb = false;
    private float cooldownt = 2;
    private void OnTriggerStay(Collider other)
    {
        string name = other.gameObject.name;
        battleButton(name);
    }
    IEnumerator cooldown()
    {
        cooldownb = true;
        yield return new WaitForSeconds(cooldownt);
        cooldownb = false;
    }
    void battleButton(string name)
    {
        PlayerController pCtrl = player.GetComponent<PlayerController>();
        Animator pAnim = pCtrl.GetComponentInChildren<Animator>();
        if (name == "Player" && Input.GetKey("e") && !battle && !cooldownb && enemies.Count != 0)
        {
            pAnim.SetInteger("moving", 0);
            pCtrl.enabled = false;
            for (int i = 0; i < enemiesChoice.Length; i++)
            {
                enemiesChoice[i] = enemies[i];
                enemiesChoice[i].transform.position = enemiesPos[i].transform.position;
                enemiesChoice[i].transform.rotation = enemiesPos[i].transform.rotation;
            }
            cam.transform.position = pos2.position;
            cam.transform.rotation = pos2.rotation;
            battle = true;
            StartCoroutine(cooldown());
        }
        else if (Input.GetKey("e") && battle && !cooldownb)
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
        if (enemies.Count == 0 && button == 0)
        {
            int num = Random.Range(1, 7);
            for (int i = 0; i < num; i++)
            {
                enemies.Add(Instantiate(enemyOBJ, new Vector3(100, 100, 100), Quaternion.Euler(0, 0, 0)));
                generateEnemyStats(i);
            }
        }
        else if (button == 0)
        {
            for (int i = 0; i < enemies.Count; i++)
                Destroy(enemies[i]);
            enemies.Clear();
            generateButton(0);
        }
        if (button == 1)
        {
            enemies.Add(Instantiate(enemyOBJ, new Vector3(0, -5, 0), Quaternion.Euler(0, 0, 0)));
            generateEnemyStats(enemies.Count-1);
        }
        if (enemies.Count != 0 && button == 2)
        {
            Destroy(enemies[enemies.Count - 1]);
            enemies.Remove(enemies[enemies.Count - 1]);
        }
    }
    public void generateEnemyStats(int i)
    {
        EnemyController eCtrl = enemies[i].GetComponent<EnemyController>();
        eCtrl.enemyHP = Random.Range(5, 21);
        eCtrl.enemyDMG = Random.Range(1, 6);
        eCtrl.enemySPD = Random.Range(1, 6);
        eCtrl.enemyElement = Elements[Random.Range(0, 3)];
    }
}
