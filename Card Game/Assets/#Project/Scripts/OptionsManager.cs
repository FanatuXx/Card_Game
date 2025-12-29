using System.Threading;
using UnityEngine;

public class OptionsManager: MonoBehaviour
{

    void Start()
    {
        AudioManager audioManager = GetComponentInParent<GameManager>().AudioManager;
    }

    void Update()
    {
        
    }
}

//bool pour muter l'audio
//reference dans le start pour trouver l'audio manager