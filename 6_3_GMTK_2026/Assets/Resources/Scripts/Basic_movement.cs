using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Basic_movement : MonoBehaviour
{

     public float speed = 5;
     public float jump_power = 10;
     public int jump_frames = 0;
     public int  jump_max = 15;
     public Vector2 playr_movement_flag = Vector2.zero;
     public float shoot_distance;
    public LineRenderer right_shoot;
    public LineRenderer left_shoot;
    public bool shot_life = false;
    public GameObject target_player;

    // Spawn n timer control
    public GameObject player_template;
    public double current_time = 10;
    public double max_time = 10;
    public Transform spawnpoint;
    public bool spawning = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
            if(playr_movement_flag.x != 0)
                target_player.transform.position += target_player.transform.right * (speed * playr_movement_flag.x) * Time.deltaTime;
            
            //horizontal movement if we feel like it
            /*
            if(playr_movement_flag.y != 0)
                transform.position += transform.forward * (speed * playr_movement_flag.y) * Time.deltaTime;
            //*/

            if(jump_frames > 0){
                     target_player.transform.position += target_player.transform.up * jump_power * jump_frames * Time.deltaTime;
                        jump_frames--;
            }

            // Spawn n timer control
            if (spawning && current_time > 0)
            {
                current_time -= Time.deltaTime;
                if (current_time <= 0)
                {
                    // current_player sendmessage for rigidbody to lock position/turn rigidbody off
                    //target_player.GetComponent<Rigidbody>().isKinematic = true;
                    //instantiate new palyer template at spawnpoint
                    target_player = Instantiate(player_template, spawnpoint);

                    current_time = max_time;
                }


           
            }
         // Line renderer Shot update section
         /*
            if(shot_life){

                        shot_life -= 2;
                        Debug.Log("line away " + shot_life);
            }else{
                
                if(shot_life_2){

                    SetShootLine(right_shoot, 0f);
                }
            }
           */ 

    }


    void OnMove(InputValue value){

           playr_movement_flag = value.Get<Vector2>();

    }

    void OnJump(){

            jump_frames = jump_max;

    }

    void OnAttack(InputValue value)
    {
        //SetShootLine(right_shoot, 150f);

         RaycastHit hit;
        if (TestDirection(right_shoot.transform.position, Vector3.right, out hit))
        {
            //SetShootLine(right_shoot, hit.distance);
            hit.transform.SendMessage("Shoot", SendMessageOptions.DontRequireReceiver);
            //GenerateHitSpot(hit.point);
        }

    }

    private bool TestDirection(Vector3 start, Vector3 direction, out RaycastHit hit)
    {
        
        return Physics.Raycast(start, transform.TransformDirection(direction), out hit, shoot_distance);
    }

    private void SetShootLine(LineRenderer line, float distance)
    {
        
        
        //line.SetPosition(2, Vector3.right * (distance - 5));
        //line.SetPosition(3, Vector3.right * distance);
        line.SetPosition(1, Vector3.right * distance);
        shot_life = true;
    }

}
