using UnityEngine;

public class Spawn_n_timer_control : MonoBehaviour
{


    public GameObject current_player;
    public GameObject player_template;
    public double current_time = 10;
    public double max_time = 10;
    public Transform spawnpoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (current_time > 0)
        {
            current_time -= Time.deltaTime;
            if (current_time <= 0)
            {
                // current_player sendmessage for rigidbody to lock position/turn rigidbody off
                //instantiate new palyer template at spawnpoint
                current_player = Instantiate(player_template, spawnpoint);

                current_time = max_time;
            }
        }
        
    }
}
