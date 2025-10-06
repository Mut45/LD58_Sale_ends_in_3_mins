using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreSessionTimer : MonoBehaviour
{
    public static StoreSessionTimer Instance { get; private set; }
    public static event System.Action OnSwapTick;
    [Header("Timer Settings")]
    [SerializeField] private float durationSeconds = 180f;
    [SerializeField] float swapIntervalSeconds = 10f;
    [SerializeField] private Text timerText;


    private float timeLeft;
    private float swapTimer;
    private float startBalance;
    private bool hasSpent;
    private bool sessionEnded;

    private CurrencyManager CM => CurrencyManager.Instance;


    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (CM == null) { Debug.LogError("CurrencyManager not found"); enabled = false; return; }
        swapTimer = swapIntervalSeconds;
        if (timeLeft <= 0f)
        {
            timeLeft = durationSeconds;
            startBalance = CM.BalanceInCents;
            hasSpent = false;
            sessionEnded = false;
        }
        if (swapTimer <= 0f)
        {
            swapTimer = swapIntervalSeconds;
        }

        CM.OnBalanceChanged += OnBalanceChanged;
        CM.OnDepleted += OnDepleted;
        UpdateTimerUI();
    }

    private void Update()
    {
        if (sessionEnded) return;
        timeLeft -= Time.unscaledDeltaTime;
        swapTimer -= Time.unscaledDeltaTime;
        if (swapTimer <= 0f)
        {
            Debug.Log("Swap Tick happened.");
            swapTimer += swapIntervalSeconds;
            OnSwapTick?.Invoke(); // Tell StoreUI to rotate 3 cards
        }
        if (timeLeft <= 0f) { timeLeft = 0f; EndSession(); }
        UpdateTimerUI();
    }

    private void OnDestroy()
    {
        if (CM != null) { CM.OnBalanceChanged -= OnBalanceChanged; CM.OnDepleted -= OnDepleted; }
    }

    private void OnBalanceChanged(float newBalance)
    {
        if (!hasSpent && newBalance < startBalance) hasSpent = true;
        if (newBalance <= 0 && !sessionEnded)
        {
            sessionEnded = true;
            Debug.Log("Money ran out. Ending 1");
            // money ran out �� Ending 1
        }
    }

    private void OnDepleted()
    {
        if (sessionEnded) return;
        sessionEnded = true;
        Debug.Log("Money ran out. Ending 1");
        // -> Ending 1
    }

    private void EndSession()
    {
        if (sessionEnded) return;
        sessionEnded = true;
        if (!hasSpent)
        {
            Debug.Log("Time ran out. Ending 2");
            GameSceneManager.Instance.Ending2();
        }
        else
        {
            Debug.Log("Time ran out. Ending 1");
            GameSceneManager.Instance.LoadConclusion1();
        }
        
        // time��s up: if anything was spent �� Ending 1, else Ending 2
        //GoToScene(hasSpent ? endingSpentScene : endingNoSpendScene);
    }

    private void UpdateTimerUI()
    {
        if (!timerText) return;
        int s = Mathf.CeilToInt(timeLeft);
        timerText.text = $"{s / 60:0}:{s % 60:00}";
    }

    // Optional: rebind a scene-local text
    public void AssignTimerText(Text newText) 
    { 
        timerText = newText; 
        UpdateTimerUI();
        Debug.Log($"TimerText manually assigned to: {newText?.name ?? "null"}");
    }
}