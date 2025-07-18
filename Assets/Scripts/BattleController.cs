using UnityEngine;

public class BattleController : MonoBehaviour
{
    Camera camera;
    Transform pos1;
    Transform pos2;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" && Input.GetKey("E"))
        {
            camera.transform.position = pos2.position;
            camera.transform.rotation = pos2.rotation;
        }
    }
}
