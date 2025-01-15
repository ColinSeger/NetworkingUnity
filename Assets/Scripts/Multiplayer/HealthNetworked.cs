using UnityEngine;

public class HealthNetworked : MonoBehaviour
{
    [SerializeField] short health;
    void Start(){
        if(health == 0){
            health = 1;
        }
    }
    public void DealDamage(short damage){
        health -= damage;
    }
}
