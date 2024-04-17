using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private GameObject player, cam,leftPOint, rightPOint;

    [SerializeField]
    private Image junpUI, AtkUI,life;

    [SerializeField]    
    private float velocit;
   


    private bool jump,atak,takeDamage;
    private float x, y, z;
    private void Awake()
    {
        jump = false;
        atak = false;
        life.fillAmount = 1;
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enimy")
        {

            player.GetComponent<Animator>().SetBool("Damage", true);
            life.fillAmount -= 0.2f;
            StartCoroutine(Damage());


        }

    }
    // Start is called before the first frame update
  
  

    // Update is called once per frame
    void Update()
    {

        PlayerMove();
        if(life.fillAmount<=0)
        {
            player.GetComponent<Animator>().SetBool("Death", true);
            StartCoroutine(Death());

        }

    }
    void CamMove()
    {
        x = player.transform.position.x;
        y = cam.transform.position.y;
        z = player.transform.position.z;
        cam.transform.position = new Vector3(x, y, z - 10);

    }
    IEnumerator Death()
    {
        player.gameObject.GetComponent<SpriteRenderer>().color= Color.red;
        yield return new WaitForSeconds(0.2f);

        player.GetComponent<Animator>().SetBool("Death", true);
        player.SetActive(false);

    }
    IEnumerator Damage()
    {
       
            yield return new WaitForSeconds(0.2f);

        player.GetComponent<Animator>().SetBool("Damage", false);

    }
    IEnumerator PosCast(float seconds, Image image)
    {
        image.fillAmount = 0;
        for (int i = 0; i < 10; i++) 
        {
            yield return new WaitForSeconds(seconds / 10);
            image.fillAmount += 0.1f;
        }
      
    }


    void PlayerMove()
    {
        CamMove();
        if (Input.GetKey(KeyCode.D))
        {
            // player.GetComponent<Rigidbody2D>().mass = 1;
            // player.GetComponent<Rigidbody2D>().AddForce(Vector2.right*velocit,ForceMode2D.Force);
            player.transform.Translate(velocit * Time.deltaTime, 0, 0);
            player.GetComponent<Animator>().SetBool("Move", true);
            player.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            // player.GetComponent<Rigidbody2D>().mass = 1;
            player.transform.Translate(-velocit * Time.deltaTime, 0, 0);
            //player.GetComponent<Rigidbody2D>().AddForce(Vector2.left * velocit,ForceMode2D.Force);
            player.GetComponent<Animator>().SetBool("Move", true);

            player.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            //  player.GetComponent<Rigidbody2D>().mass = 1;
            player.GetComponent<Animator>().SetBool("Move", false);
            // player.GetComponent<Rigidbody2D>().mass = 10000000;
            //player.GetComponent<Rigidbody2D>().AddForceAtPosition
            //player.GetComponent<Animator>().SetBool("Jump", false);


        }

        if (Input.GetKey(KeyCode.Space) && !jump)
        {
            jump = true;

            player.GetComponent<Animator>().SetBool("Jump", true);
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * (velocit + 10), ForceMode2D.Impulse);
            StartCoroutine(Jump());


        }
        if (Input.GetKey(KeyCode.RightArrow) && !atak)
        {
            atak = true;

            player.GetComponent<Animator>().SetBool("Atak", true);
            
            StartCoroutine(Atak());

            if(!player.GetComponent<SpriteRenderer>().flipX)
            {
                rightPOint.SetActive(true);
            }
            else
            {
                leftPOint.SetActive(true);
            }



        }
    }

    IEnumerator Jump()
    {
        StartCoroutine(PosCast(2.8f, junpUI));
        yield return new WaitForSeconds(0.8f);
        player.GetComponent<Animator>().SetBool("Jump", false);

      
        yield return new WaitForSeconds(2);

        jump = false;


    }

    IEnumerator Atak()
    {
        StartCoroutine(PosCast(2.4f, AtkUI));
        yield return new WaitForSeconds(0.4f);
        player.GetComponent<Animator>().SetBool("Atak", false);
        rightPOint.SetActive(false);
        leftPOint.SetActive(false);
       
        yield return new WaitForSeconds(2);

        atak = false;


    }
}
