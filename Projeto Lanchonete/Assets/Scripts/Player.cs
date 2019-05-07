using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Aurea;

    public bool moving = false;
    public bool selected = false;

    public Transform hand;
    public Transform objective = null;
    public Slot slot = null;
    public GameObject ItemGrabed=null;

    private void Start()
    {
        Aurea.SetActive(false);
    }

    void Update()
    {
        if (selected) SelectObjective();

        if (moving) Moving(objective);

    }

    void Moving(Transform pos)
    {
            transform.position = Vector3.MoveTowards(transform.position, pos.position, 5f * Time.deltaTime);
            transform.forward = pos.position - transform.position;    

            Debug.Log(Vector3.Distance(transform.position, pos.position));
            Debug.DrawLine(transform.position, pos.position, Color.red);    

            if (Vector3.Distance(transform.position, pos.position)<=0.2f)
            {
                
                moving = false;

                if(slot!=null) Grab();
        }
    }

    void SelectObjective()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);

            if (hit.transform != null)
                if (hit.collider.tag == "Object")
                {
                    slot = hit.transform.GetComponent<Slot>();
                    objective = slot.PosicaoDoPlayer.transform;
                    objective.position = new Vector3(objective.position.x, transform.position.y, objective.position.z);
                    moving = true;
                }
        }
    }

    void Grab()
    {
        if(ItemGrabed == null && slot.ObjetoNoSlot != null) //Se nao tenho nada na mao e algo no slot, eu pego o item e coloco na mao
        {

            
            ItemGrabed = slot.ObjetoNoSlot;
            slot.ObjetoNoSlot = null;
            ItemGrabed.transform.parent = gameObject.transform;
            ItemGrabed.transform.position = hand.position;
            slot = null;
            
        }

        if(ItemGrabed != null && slot.ObjetoNoSlot == null) //Se tenho item na mao mas nao tem item no slot, eu coloco o item no slot
        {            
            slot.ObjetoNoSlot = ItemGrabed;
            slot.ObjetoNoSlot.transform.parent = slot.transform;
            slot.ObjetoNoSlot.transform.position = slot.transform.position;
            ItemGrabed = null;
            
        }

        if(ItemGrabed!=null && slot.ObjetoNoSlot != null)//Se eu tenho item nao mao e tem item no slot, eu misturo os itens
        {
            Debug.Log("Mix");
            if (slot.ObjetoNoSlot.GetComponent<Ingrediente>() != null)
                slot.ObjetoNoSlot.GetComponent<Ingrediente>().AdicionarIngrediente(ItemGrabed);
            else Debug.Log("Não é possivel misturar");
        }
        
    }




}
