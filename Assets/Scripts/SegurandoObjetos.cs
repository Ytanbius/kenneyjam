using UnityEngine;

public class SegurandoObjetos : MonoBehaviour
{
    public Transform spawn;
    public GameObject tomate;
    public GameObject batata;
    public GameObject carne;
    public Transform fogao;
    public bool pegando = false;
    private bool maoocupada = false;
    private GameObject segurandoitem;
    

    private void Update()
    {
        if(maoocupada == true && Input.GetKeyDown(KeyCode.Q))
        {
            maoocupada= false;
            Destroy(GameObject.FindWithTag("segurando"));
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("tomate"))
        {
            Debug.Log("entrado");
            if (Input.GetKeyDown(KeyCode.E) && maoocupada == false)
            {
                pegando = true;
                segurandoitem = Instantiate(tomate, spawn.position, spawn.rotation);
                Debug.Log("criado");
                segurandoitem.transform.parent = spawn.transform;
                maoocupada = true;
                
            }
        }

        else if (other.CompareTag("carne"))
        {
            Debug.Log("entrado");
            if (Input.GetKeyDown(KeyCode.E) && maoocupada == false)
            {
                pegando = true;
                segurandoitem = Instantiate(carne, spawn.position, spawn.rotation);
                Debug.Log("criado");
                segurandoitem.transform.parent = spawn.transform;
                maoocupada = true;
            }
        }

        else if (other.CompareTag("batata"))
        {
            if (Input.GetKeyDown(KeyCode.E) && maoocupada == false)
            {
                pegando = true;
                segurandoitem = Instantiate(batata, spawn.position, spawn.rotation);
                Debug.Log("criado");
                segurandoitem.transform.parent = spawn.transform;
                maoocupada = true;
            }
        }

        else if (other.CompareTag("maca"))
        {
            if(Input.GetKeyDown(KeyCode.E) && maoocupada == true)
            {
                pegando = false;
                maoocupada = false;
                Debug.Log("colocado");
                segurandoitem.transform.parent = fogao.transform;
                segurandoitem.transform.SetPositionAndRotation(fogao.position, fogao.rotation);
            }
        }
    }
   
}
