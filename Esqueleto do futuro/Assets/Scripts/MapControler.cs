using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControler : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer groundSprite;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PsicodelicGroud());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator PsicodelicGroud()
    {
        int indexCollor = 0;
        float incrementCollor = 0;
        bool flip = true;


        while (true) 
        {
          if(indexCollor==0 && flip)
          {
                groundSprite.color = new Color(0, 1, incrementCollor, 1);
          }
          else if (indexCollor == 2 && !flip)
          {
                groundSprite.color = new Color(incrementCollor, 1, 0, 1);
          }
          else if (indexCollor==1 && flip) 
          {
                groundSprite.color = new Color(1, incrementCollor, 0, 1);
          }
          else if (indexCollor == 0 && !flip)
          {
                groundSprite.color = new Color(1, 0, incrementCollor, 1);
          }
          else if (indexCollor == 2 && flip)
          {
                groundSprite.color = new Color(incrementCollor, 0, 1, 1);
          }
            else if (indexCollor == 1 && !flip)
            {
                groundSprite.color = new Color(0, incrementCollor, 1, 1);
            }


            yield return new WaitForEndOfFrame();
            if(flip)
            {
                incrementCollor += 0.2f;
                if(incrementCollor>=1)
                {
                    flip = false;
                    indexCollor++;
                    if (indexCollor == 3)
                    {
                        indexCollor = 0;
                    }

                }
                
            }
            else
            {
                incrementCollor -= 0.2f;

                if ( incrementCollor <=0) 
                {
                    flip=true;
                    indexCollor++;
                    if (indexCollor == 3)
                    {
                        indexCollor = 0;
                    }
                }

               


               

               
            }

          

        }
      
    }
}
