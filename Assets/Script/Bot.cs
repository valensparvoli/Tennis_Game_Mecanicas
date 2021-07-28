using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public float speed=50f;
    //float force = 13f;
    Animator animator;
    public Transform ball;
    public Transform aimTargetBot;

    Vector3 targetPosition;

    public Transform[] targets; //Contiene todos los targets que el bot puede usar

    ShotManager shotManager;


    void Start()
    {
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
        shotManager = GetComponent<ShotManager>();
    }

    void Update()
    {
        Move();
    }

    //Nos da nuestra posicion y permite que el enemigo se pueda mover para poder devolver la pelota, siempre sobre x
    void Move()
    {
        targetPosition.x = ball.position.x;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition,speed*Time.deltaTime);
    }

    Vector3 PickTarget()
    {
        int randomValue = Random.Range(0, targets.Length);
        return targets[randomValue].position;
    }
    Shot PickShot()
    {
        int randomValue = Random.Range(0, 2);
        if (randomValue == 0)
        {
            return shotManager.topSpin;
        }
        else 
        {
            return shotManager.Flat;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Shot currentShot = PickShot();

            //Calcula la direccion en la que se encuentra el aimtarget y envia la pelota hacia ese lugar
            Vector3 dir = PickTarget() - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);

            /*Codigo utilizado para calcular la direccion en la que se encuentra la pelota y asi 
            saber cual animacion vamosa a ejecutar*/
            Vector3 ballDir = ball.position - transform.position;
            if (ballDir.x >= 0)
            {
                animator.Play("ForeHand");
            }
            else
            {
                animator.Play("BackHand");
            }

            ball.GetComponent<Ball>().hitter = "Bot";
        }
    }
}
