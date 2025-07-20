using UnityEngine;

public class AssarECortar : MonoBehaviour
{

    public bool assando= false ;
    public bool pronto= false ;
    public GameObject item;
    public float cooldown;
    public SegurandoObjetos segur;

    private void Update()
    {
        

        if (assando == true)
        {
            cooldown-= Time.deltaTime;
        }

        else
        {
            cooldown = 6;
        }
        if (cooldown <= 0)
        {
            assando = false;
            pronto = true;
            cooldown = 6;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        Debug.Log(cooldown);
        segur = other.GetComponent<SegurandoObjetos>();

        if(segur.segurandoitem.CompareTag("abacaxi") && !assando && !pronto)
        {
            assando = true ;
            item.gameObject.SetActive(true);
        }
    }
}

