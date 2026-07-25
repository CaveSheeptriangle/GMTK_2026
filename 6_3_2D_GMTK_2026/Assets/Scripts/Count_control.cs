using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Count_control : MonoBehaviour
{

    public GameObject L_eye;
    public GameObject R_eye;
    public GameObject current_target;
    public GameObject ze_player;
    
    //V this is needed to get the gunnery positions cause those are on the spawn controller and not the character itself
    public GameObject Spawn_script;
    public float enemy_speed = 0;
    public float enemy_leg_oomph = 10;
    public float leash_dist = 0;
    public GameObject man_to_move;
    public bool dont_freeze_flag = true;
    public bool gun_spot_locked = false;
    public List<GameObject> gun_spots = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Spawn_script.SendMessage("gun_spot_req", this, SendMessageOptions.DontRequireReceiver);
    }

    // Update is called once per frame
    void Update()
    {
        /*
            current enemy theory
             to use a series of game emptys all over the palce to acts as the points the enemy must moves towards
            for example take this cube [], we can place 2 emptys * [] * either side of it and the enemy, if it was one the right, should try to center itself on the
            right empty (*) such that wed have * [] E, the enemy would then begin firing and shielding techniques.
            we can also set the cube up with many empties such as * * * [] * * * to allow the enemy to pick different depths

            the enemy has 2 emptys on either side (these are the "eyes") given as * E * which will raycast down to make sure that enemy always has floor to keep going on
            {currently the enemy has no rigid body and does not respect nor need the floor to stand on}
            the enemy should pace in one direction until a distance calculation between it and the player engages the move towards player setup
        */
        //enemy_speed = Vector3.MoveTowards(man_to_move.transform.position, current_player.transform.position, enemy_leg_oomph * Time.deltaTime);
        
        /*
        RaycastHit L_hit;
        
        RaycastHit R_hit;
        Testfloor(L_eye.transform.position, out L_hit);
        Testfloor(L_eye.transform.position, out R_hit);
        Debug.Log("L eye see " + L_hit.collider);
        Debug.Log("R eye see " + R_hit.collider);
        */

        if(Vector3.Distance(man_to_move.transform.position, ze_player.transform.position) < leash_dist && dont_freeze_flag){
            // needs expansion for the empty system
            if(!gun_spot_locked){
                    current_target = Find_good_gun_spot(gun_spots, man_to_move);
            }        
            // the MoveTowards function can move the enemy toward the player through all 3 dimensions, Alden wants just to move on X axis & this "does that"
            man_to_move.transform.position =  new Vector2(Vector3.MoveTowards(man_to_move.transform.position, current_target.transform.position, enemy_leg_oomph * Time.deltaTime).x, man_to_move.transform.position.y);
            
            Debug.Log(" differential is " + Vector3.MoveTowards(man_to_move.transform.position, current_target.transform.position, enemy_leg_oomph * Time.deltaTime));
        
        }
           
           
           
           
            //man_to_move.transform.position =  new Vector2(Vector3.MoveTowards(man_to_move.transform.position, current_target.transform.position, enemy_leg_oomph * Time.deltaTime).x, man_to_move.transform.position.y);
        
        /*
        else{

                /*                
                if(Testfloor(man_to_move.transform.position, out hit)){ //&& Vector3.Distance(man_to_move.transform.position, current_target.transform.position) < 1f){
                   
                }else{
                */
                /*
                RaycastHit hit;
                if(current_target == L_fencepost){
                    if(Testfloor(L_eye.transform.position, out hit)){

                        Debug.Log("L eye see " + hit.collider);
                        current_target = R_fencepost;

                    }
                }else if (!Testfloor(R_eye.transform.position, out hit)){

                        Debug.Log("R eye see " + hit.collider);
                        current_target = L_fencepost;
                    }


                //}

                man_to_move.transform.position =  new Vector2(Vector3.MoveTowards(man_to_move.transform.position, current_target.transform.position, enemy_leg_oomph * Time.deltaTime).x, man_to_move.transform.position.y);



        }
        */
    }

    private bool Testfloor(Vector3 start, out RaycastHit hit)
    {
        return Physics.Raycast(start, -transform.up, out hit, 10f);
    }

    private GameObject Find_good_gun_spot(List<GameObject> pos_list, GameObject sender){
                GameObject best_choice = null;

                foreach(GameObject pos in pos_list){

                        if(best_choice == null){
                                best_choice = pos;

                        }else if(Vector3.Distance(pos.transform.position, sender.transform.position) < Vector3.Distance(best_choice.transform.position, sender.transform.position) ){
                                best_choice = pos;

                        }

                }
                gun_spot_locked = true;
                return best_choice;

    }

    private GameObject Find_good_gun_spot(List<GameObject> pos_list, GameObject sender, GameObject blocked){
                GameObject best_choice = null;

                foreach(GameObject pos in pos_list){


                        if(pos != blocked){
                             

                            if(Vector3.Distance(pos.transform.position, sender.transform.position) < Vector3.Distance(best_choice.transform.position, sender.transform.position))
                                    best_choice = pos;

                        }

                }
                gun_spot_locked = true;
                return best_choice;

    }

    public void Set_gun_list(List<GameObject> x){

            gun_spots = x;


    }







}
