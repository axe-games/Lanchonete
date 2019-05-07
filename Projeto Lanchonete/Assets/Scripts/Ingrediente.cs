using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingrediente : MonoBehaviour
{

    public Transform posIngre;

    public GameObject filho;

    public void AdicionarIngrediente(GameObject novo)
    {
        if (filho == null) {
            filho = novo;
            novo.transform.parent = transform;
            novo.transform.position = posIngre.position;
        }
        else
        {
            Debug.Log("Entrou");
            filho.GetComponent<Ingrediente>().AdicionarIngrediente(novo);
        }


    }




}
