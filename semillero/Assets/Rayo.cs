using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayo : MonoBehaviour
{

    public Transform indexFinger;
    private GameObject objetoTomado;
    public float offsetRay = 0.0f;

    public GameObject laserObject;
    LineRenderer laserRender;

    public GameObject cubo2;
    float GrabbedObjectSize;

    bool isTacked;


    // Use this for initialization
    void Start()
    {
        laserRender = laserObject.GetComponent<LineRenderer>();
        isTacked = false;  
    }

    // Update is called once per frame


    void Update()
    {
        indexFinger = GameObject.Find("hands:b_r_index_ignore").transform;

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            ShootRay();
        }


        cubo2.transform.position = new Vector3(indexFinger.position.x + 20, indexFinger.position.y, indexFinger.position.z + 20);
        //Set laser render pos
        laserObject.transform.position = indexFinger.position;
        laserRender.SetPosition(1, indexFinger.right*200.0f);

        Debug.DrawRay(indexFinger.position, indexFinger.right*200.0f, Color.red);


        // Tomar objet
        if (isTacked)
        {
            var FingerPos = indexFinger.position;
            objetoTomado.transform.localPosition = new Vector3(FingerPos.x, FingerPos.y, FingerPos.z) + (indexFinger.transform.right * 25.0f);
        }



    }


    GameObject getMouseHover(float range)
    {
        Vector3 position = indexFinger.transform.position;
        RaycastHit hit;

        Vector3 target = position + indexFinger.transform.forward;
        if (Physics.Linecast(position, target, out hit))
        {
            return hit.collider.gameObject;
        }
        return null;
    }


    public void mostarRayo(LineRenderer laser, Vector3 pos)
    {
        laser.SetPosition(1, pos);
    }


    public void ShootRay()
    {
        Ray ray = new Ray(indexFinger.position, indexFinger.right);
        RaycastHit hit;

        //Longitud del rayo
        float rayLenght = 800.0f;
        



        if (Physics.Raycast(ray, out hit, rayLenght))
        {
            if (hit.collider != null)
            {
                //Distancia entre el rayo y el objeto
                float distance = Vector3.Distance(ray.origin, hit.transform.position);
                offsetRay = distance;
                //distanceText.text = distance.ToString();

                //Nombre del objeto
                string nombreObjeto = hit.transform.gameObject.name;
                Debug.Log("obj: "+nombreObjeto);
                //nombre.text = nombreObjeto;

                //Cambia el color del objeto
                cambiarColor(hit.transform.gameObject);

                //Tomar objeto
                float xOffset = 20.0f;
                tomarObjeto(hit.transform.gameObject, new Vector3(indexFinger.position.x, hit.transform.gameObject.transform.position.y, hit.transform.gameObject.transform.position.z));

                // Toma el objeto
                objetoTomado = hit.transform.gameObject;
                isTacked = true;

                //Pega el objeto al mouse
                float offset = distance;
                Vector3 pos = ray.origin + (ray.direction * offset);

                
                //hit.transform.gameObject.transform.position = pos;


            }
        }
    }



    public void tomarObjeto(GameObject objeto, Vector3 pos)
    {
        objeto.transform.localPosition = pos;
    }


    public void Cambiarobj(GameObject objeto) {

        Vector3 obj1 = this.transform.position;
        Vector3 obj2 = objeto.transform.position;

        float distancia = Vector3.Distance(obj1 , obj2) ;

        
        




    }

    public void cambiarColor(GameObject objeto)
    {
        objeto.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

    }


}