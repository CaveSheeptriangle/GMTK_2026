using UnityEngine;

public class Smoke_script : MonoBehaviour
{

    public int smoke_duration = 0;
    public int smoke_desire_duration = 2;
    public GameObject smoke_assest;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if(smoke_duration > 0){


                    smoke_duration--;

            }else{

                smoke_assest.SetActive(false);
            }


    }

    public void Shoot()
    {
        smoke_assest.SetActive(true);
        smoke_duration = smoke_desire_duration;
    }

}
