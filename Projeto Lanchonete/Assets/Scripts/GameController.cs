using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{


    public GameObject EmpregadoSelecionado;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);

            if (hit.transform!=null)
            {
                Debug.Log(hit.transform.name);
                if (hit.collider.tag == "Player")
                {
                    Debug.Log(hit.collider.name);
                    ClicarEmpregado(hit.collider.gameObject);
                }
            }
            else
            {
                if (EmpregadoSelecionado != null)
                {
                    EmpregadoSelecionado.GetComponent<Player>().selected = false;
                    EmpregadoSelecionado.GetComponent<Player>().Aurea.SetActive(false);
                }
                
            }
                
            
        }
    }

    public void ClicarEmpregado(GameObject novo)
    {
        if (EmpregadoSelecionado != null)
        {
            EmpregadoSelecionado.GetComponent<Player>().selected = false;
            EmpregadoSelecionado.GetComponent<Player>().Aurea.SetActive(false);
        }

        EmpregadoSelecionado = novo;
        EmpregadoSelecionado.GetComponent<Player>().selected = true;
        EmpregadoSelecionado.GetComponent<Player>().Aurea.SetActive(true);
    }
}
