using UnityEngine;

public class SegurandoObjetos : MonoBehaviour
{
    public GameObject spawn;
    public GameObject tomate;
    public bool pegando;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Update()
    {
      
        
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("tomate"))
        {

            Debug.Log("entrado");
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                Debug.Log("criado");
            }
        }

        else if (other.CompareTag("abacaxi"))
        {
            Debug.Log("entrado");
        }

        else if (other.CompareTag("carne"))
        {
            Debug.Log("entrado");
        }

        else if (other.CompareTag("blueberry"))
        {
            Debug.Log("entrado");
        }

        else if (other.CompareTag("maca"))
        {
            Debug.Log("entrado");
        }

        else if (other.CompareTag("batata"))
        {
            Debug.Log("entrado");
        }

    }
   
}
