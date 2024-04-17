using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnimyController : MonoBehaviour
{
    [SerializeField]
    private float velocit;


    private bool direction,damage;
    // Start is called before the first frame update
    void Awake()
    {
        damage = false;
        direction = false;  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag== "Wall") 
        {
            direction = (direction == true) ? false : true;   
            this.gameObject.GetComponent<SpriteRenderer>().flipX = !direction;
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Atak")
        {
           damage=true;
            this.gameObject.GetComponent<Rigidbody2D>().AddForce((this.transform.position - collision.transform.position  ),ForceMode2D.Impulse);
            StartCoroutine(Death());
        }

    }
    // Update is called once per frame
    void Update()
    {
        Direction(direction);
    }


    void Direction(bool direction)
    {
        if (!damage) 
        {
            if (direction)
            {

                this.transform.Translate(Vector3.left * velocit * Time.deltaTime);
            }
            else
            {
                this.transform.Translate(Vector3.right * velocit * Time.deltaTime);
            }

        }
      
    }

    private IEnumerator Death()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }
}
