using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int playerScore;
    private int playerXP;
    private int difficultyLevel = 5;

    public OptionsManager OptionsManager { get; private set; }
    public AudioManager AudioManager { get; private set; }
    public DeckManager DeckManager { get; private set; }
    public TarotDeckManager TarotDeckManager { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeManagers();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void InitializeManagers()
    {
        OptionsManager = GetComponentInChildren<OptionsManager>();
        AudioManager = GetComponentInChildren<AudioManager>();
        DeckManager = GetComponentInChildren<DeckManager>();
        TarotDeckManager = GetComponentInChildren<TarotDeckManager>();

        if (OptionsManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/OptionsManager");
            if (prefab == null)
            {
                Debug.LogError("OptionsManager prefab not found in Resources/Prefabs.");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                OptionsManager = GetComponentInChildren<OptionsManager>();
            }
        }

        if (AudioManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/AudioManager");
            if (prefab == null)
            {
                Debug.LogError("AudioManager prefab not found in Resources/Prefabs.");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                AudioManager = GetComponentInChildren<AudioManager>();
            }
        }

        if (DeckManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/DeckManager");
            if (prefab == null)
            {
                Debug.LogError("DeckManager prefab not found in Resources/Prefabs.");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                DeckManager = GetComponentInChildren<DeckManager>();
            }
        }

        if (TarotDeckManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/TarotDeckManager");
            if (prefab == null)
            {
                Debug.LogError("TarotDeckManager prefab not found in Resources/Prefabs.");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                TarotDeckManager = GetComponentInChildren<TarotDeckManager>();
            }
        }
    }

    public int PlayerScore
    {
        get { return playerScore; }
        set { playerScore = value; }
    }

    public int PlayerXP
    {
        get { return playerXP; }
        set { playerXP = value; }
    }

    public int DifficultyLevel
    {
        get { return difficultyLevel; }
        set { difficultyLevel = value; }
    }
}
