using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance;
    int scoreHost = 0;
    int scoreClient = 0;
    void Awake(){
        Instance = this;
    }
    public void AddScoreHost(int points){
        scoreHost++;
        Debug.Log(scoreHost + "Host");
    }
    public void AddScoreClient(int points){
        scoreHost++;
        Debug.Log(scoreHost + "Client");
    }
}
