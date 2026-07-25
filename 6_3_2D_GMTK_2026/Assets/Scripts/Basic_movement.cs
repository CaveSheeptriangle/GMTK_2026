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

    //gun control
    public GameObject gunside_smoke;
    public GameObject Hit_marker;
    public int trigger_refresh = 0;
    //public GameObject the_ui; //look at using get element by Tag for the call to the UI
    public int ammo_total = 5;
    public int ammo_curr = 5;

    //statue gameplay
    public Transform statue_spawn;
    public GameObject sculpt_template;
    

    // Spawn n timer control
    public GameObject player_template;
    public double current_time = 10;
    public double max_time = 10;
    public GameObject spawnpoint;
    
    public bool spawning = false;
    public List<GameObject> gun_spots;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if(trigger_refresh > 0)
                    trigger_refresh--;

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

                    //instantiate new player template at spawnpoint
                            target_player = Instantiate(player_template, spawnpoint.transform);

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

        if(trigger_refresh <= 0){
              // summon the gun side smoke particle here
            gunside_smoke.SendMessage("Shoot", SendMessageOptions.DontRequireReceiver);
            
            //this is the actual refresh rate in frames of the trigger can define it at the editor end cause of this
            trigger_refresh = 2;
            ammo_curr--;

             RaycastHit hit;
            if (TestDirection(right_shoot.transform.position, Vector3.right, out hit, shoot_distance))
            {
            
                // shoot should tell the enemy to summon their smoke particle or we could move our particle there
                Hit_marker.transform.position = hit.transform.position;
                Hit_marker.SendMessage("Shoot", SendMessageOptions.DontRequireReceiver);
                //Debug.Log("line away " + hit.collider);
            //GenerateHitSpot(hit.point);
            }

            if(ammo_curr == 0){
                    //do summon statue
                    sculpt_statue();
                    ammo_curr = ammo_total;
            }
        }

    }

    private bool TestDirection(Vector3 start, Vector3 direction, out RaycastHit hit, float request_d)
    {
        
        return Physics.Raycast(start, transform.TransformDirection(direction), out hit, request_d);
    }

    private void SetShootLine(LineRenderer line, float distance)
    {
        
        
        //line.SetPosition(2, Vector3.right * (distance - 5));
        //line.SetPosition(3, Vector3.right * distance);
        line.SetPosition(1, Vector3.right * distance);
        shot_life = true;
    }
    public void gun_spot_req(GameObject Sender){

            Sender.SendMessage("Set_gun_list",gun_spots, SendMessageOptions.DontRequireReceiver);
            //Save 

    }

    private void sculpt_statue(){

            //this uses unity physics to place the next statue higher, by spawning inside it
            // may need code on spawn to have it raycast chain to find the topmost and teleport there
            Instantiate(sculpt_template, statue_spawn.position, sculpt_template.transform.rotation, spawnpoint.transform);


    }

}
