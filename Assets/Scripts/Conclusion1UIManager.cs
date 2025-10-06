using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conclusion1UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button toConclusion2Button;

    // Update is called once per frame
    void Start()
    {
        InitializeButtons();
    }

    private void OpenConclusion2()
    {
        Debug.Log("Going to conclusion 2 page");
        if (GameSceneManager.Instance != null)
        {
            GameSceneManager.Instance.LoadConclusion2();
        }
        else
        {
            Debug.LogError("GameSceneManager not found");
        }
    }
    private void InitializeButtons()
    {
        if (toConclusion2Button != null)
        {
            toConclusion2Button.onClick.RemoveAllListeners();
            toConclusion2Button.onClick.AddListener(OpenConclusion2);
        }
    }
}
