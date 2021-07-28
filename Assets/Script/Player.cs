using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform aimTarget;
    public Transform ball;
    public float speed;
    float targetSpeed = 5f;
    //float force = 13f;

    bool hitting;

    Animator animator;

    Vector3 aimTargetInitialPosition;

    ShotManager shotManager;
    Shot currentShot;

    private void Start()
    {
        animator = GetComponent<Animator>();
        aimTargetInitialPosition = aimTarget.position;
        shotManager = GetComponent<ShotManager>();
        currentShot = shotManager.topSpin;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        //Nos permite manipular el aimtarget y los diferentes tiros que hacemos 
        if (Input.GetKeyDown(KeyCode.R))
        {
            hitting = true;
            currentShot = shotManager.Flat;

        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            hitting = false;
            currentShot = shotManager.Flat;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hitting = true;
            currentShot = shotManager.topSpin;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            hitting = false;
            currentShot = shotManager.topSpin;

        }



        //Mueve el aimtarget
        if (hitting)
        {
            aimTarget.Translate(new Vector3(h, 0, v) * targetSpeed * Time.deltaTime);
        }

        //Encargado del movimiento del personaje y de cambiar entre movernos o mover el aimtarget
        if((h!=0 || v != 0) && !hitting)
        {
            transform.Translate(new Vector3(h, 0, v)*speed*Time.deltaTime);
        }
        //Debug.DrawRay(transform.position, ballDir);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            //Calcula la direccion en la que se encuentra el aimtarget y envia la pelota hacia ese lugar
            Vector3 dir = aimTarget.position - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0,currentShot.upForce,0);

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

            aimTarget.position = aimTargetInitialPosition;
        }
    }

}
