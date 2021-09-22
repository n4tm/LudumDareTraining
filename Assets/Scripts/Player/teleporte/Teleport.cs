using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Teleport : MonoBehaviour
{
    private GameObject nextStep;
    private Transform posicaoInicial;
    float timer = 0;
    private CapsuleCollider2D colisao;
    private bool _isColl;

    private bool facingFront = true; 
    // Start is called before the first frame update
    void Start()
    {
        
        nextStep = gameObject.transform.GetChild(0).gameObject;
        nextStep.SetActive(true);
        posicaoInicial = nextStep.transform;
        colisao = nextStep.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2)
        {

      
            timer = 0;
        }

       
    }


        

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("ground")){
        nextStep.GetComponent<SpriteRenderer>().color = Color.red;
        nextStep.transform.position -= Vector3.left;
        _isColl = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ground"))
        {
            nextStep.GetComponent<SpriteRenderer>().color = Color.magenta;
            _isColl = false;
            nextStep.transform.position += Vector3.left * 0.7f;   
        }
    }

 

    

}
