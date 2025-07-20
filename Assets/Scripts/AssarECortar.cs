using UnityEngine;

public class AssarECortar : MonoBehaviour
{
    public GameObject carne;
    public GameObject carnepronta;
    public bool assando= false ;
    public bool pronto= false ;
    public GameObject item;
    public float cooldown = 6;
    public SegurandoObjetos segur;
    public Transform panela;
    public bool colocado = false;

    private void Update()
    {
        if (colocado == true)
        {
            assando = true;
        }
        
        if (assando == true && colocado == true)
        {
            cooldown-= Time.deltaTime;
            
        }

        if (cooldown <= 0)
        {
            Destroy(item);
            assando = false;
            pronto = true;
            cooldown = 6;
            colocado = false;
            item = Instantiate(carnepronta, panela.position,panela.rotation);
        }

        
    }
    public void OnTriggerStay(Collider other)
    {
        Debug.Log(cooldown);
        segur = other.GetComponent<SegurandoObjetos>();

       if (other.CompareTag("Player") && Input.GetKey(KeyCode.E) && colocado == false)
        {
            item = Instantiate(carne, panela.position,panela.rotation);
            colocado = true;
        }
    }
}

