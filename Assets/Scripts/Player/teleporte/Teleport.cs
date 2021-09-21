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
        if (timer >= 1)
        {
            resetTeleport();
            print("resetou");
            timer = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        nextStep.GetComponent<SpriteRenderer>().color = Color.red;
        nextStep.transform.position -= Vector3.left;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        nextStep.GetComponent<SpriteRenderer>().color = Color.magenta;
        nextStep.transform.position += Vector3.left * 0.7f;
    }

    public void resetTeleport()
    {
        if (facingFront)
        {
            nextStep.transform.position = transform.position + new Vector3(7, 0, 0);
        }
        else
        {
            nextStep.transform.position = transform.position - new Vector3(7, 0, 0);
        }
    }

    public void flipFace()
    {
        facingFront = !facingFront;
    }
    

}
