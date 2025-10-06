using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Simple Steam Style Applier Script
/// Designed specifically for traditional Unity UI system, one-click Steam style application
/// </summary>
public class SimpleSteamStyleApplier : MonoBehaviour
{
    [Header("One-Click Application Settings")]
    [SerializeField] private bool autoApplyOnGameStart = true;
    [SerializeField] private bool applyToAllCanvas = true;
    
    [Header("Manual Object Specification (Optional)")]
    [SerializeField] private Canvas[] targetCanvas;
    [SerializeField] private Button[] targetButtons;
    [SerializeField] private Text[] targetTexts;
    [SerializeField] private Image[] targetImages;
    
    void Start()
    {
        if (autoApplyOnGameStart)
        {
            ApplySteamStyle();
        }
    }
    
    /// <summary>
    /// One-click application of Steam style to all UI elements
    /// </summary>
    [ContextMenu("Apply Steam Style")]
    public void ApplySteamStyle()
    {
        Debug.Log("🎨 Starting Steam style application...");
        
        if (applyToAllCanvas)
        {
            // Apply styles to all Canvas
            Canvas[] allCanvas = FindObjectsOfType<Canvas>();
            foreach (Canvas canvas in allCanvas)
            {
                ApplyCanvasStyle(canvas);
                Debug.Log($"✅ Applied Steam style to Canvas: {canvas.name}");
            }
        }
        else
        {
            // Apply styles only to specified Canvas
            foreach (Canvas canvas in targetCanvas)
            {
                if (canvas != null)
                {
                    ApplyCanvasStyle(canvas);
                    Debug.Log($"✅ Applied Steam style to specified Canvas: {canvas.name}");
                }
            }
        }
        
        // Apply styles to specified buttons
        foreach (Button button in targetButtons)
        {
            if (button != null)
            {
                ApplyButtonStyle(button);
                Debug.Log($"✅ Applied Steam style to button: {button.name}");
            }
        }
        
        // Apply styles to specified texts
        foreach (Text text in targetTexts)
        {
            if (text != null)
            {
                ApplyTextStyle(text);
                Debug.Log($"✅ Applied Steam style to text: {text.name}");
            }
        }
        
        // Apply styles to specified images
        foreach (Image image in targetImages)
        {
            if (image != null)
            {
                ApplyImageStyle(image);
                Debug.Log($"✅ Applied Steam style to image: {image.name}");
            }
        }
        
        Debug.Log("🎉 Steam style application completed!");
    }
    
    /// <summary>
    /// Apply Steam style to Canvas and all its child objects
    /// </summary>
    private void ApplyCanvasStyle(Canvas canvas)
    {
        // Set Canvas background color to Steam dark
        Image canvasBackground = canvas.GetComponent<Image>();
        if (canvasBackground != null)
        {
            canvasBackground.color = new Color(0f, 0.059f, 0.094f, 1f); // Steam darkest color
        }
        
        // Recursively apply styles to all child objects
        ApplyStyleRecursively(canvas.gameObject);
    }
    
    /// <summary>
    /// Recursively apply Steam style to all child objects
    /// </summary>
    private void ApplyStyleRecursively(GameObject obj)
    {
        // Process components of current object
        Image image = obj.GetComponent<Image>(); 
        Text text = obj.GetComponent<Text>();
        Button button = obj.GetComponent<Button>();
        
        // Apply image style (exclude icons)
        if (image != null && !obj.name.ToLower().Contains("icon"))
        {
            ApplyImageStyle(image);
        }
        
        // Apply text style
        if (text != null)
        {
            ApplyTextStyle(text);
        }
        
        // Apply button style
        if (button != null)
        {
            ApplyButtonStyle(button);
        }
        
        // Recursively process child objects
        foreach (Transform child in obj.transform)
        {
            ApplyStyleRecursively(child.gameObject);
        }
    }
    
    /// <summary>
    /// Apply Steam style to button
    /// </summary>
    private void ApplyButtonStyle(Button button)
    {
        if (button == null) return;
        
        // Set button colors
        ColorBlock colors = button.colors;
        
        // Determine type based on button name
        string buttonName = button.name.ToLower();
        
        if (buttonName.Contains("purchase") || buttonName.Contains("buy"))
        {
            // Purchase button - Steam blue
            colors.normalColor = new Color(0.102f, 0.624f, 1f, 1f); // #1A9FFF
            colors.highlightedColor = new Color(0f, 0.733f, 1f, 1f); // #00BBFF
        }
        else if (buttonName.Contains("add") || buttonName.Contains("cart"))
        {
            // Add button - Steam green
            colors.normalColor = new Color(0.357f, 0.639f, 0.169f, 1f); // #5ba32b
            colors.highlightedColor = new Color(0.349f, 0.749f, 0.251f, 1f); // #59BF40
        }
        else if (buttonName.Contains("remove") || buttonName.Contains("delete"))
        {
            // Delete button - Steam red
            colors.normalColor = new Color(0.851f, 0.255f, 0.149f, 1f); // #D94126
            colors.highlightedColor = new Color(0.933f, 0.337f, 0.231f, 1f); // #EE563B
        }
        else
        {
            // Default button - Steam blue
            colors.normalColor = new Color(0.102f, 0.624f, 1f, 1f); // #1A9FFF
            colors.highlightedColor = new Color(0f, 0.733f, 1f, 1f); // #00BBFF
        }
        
        colors.pressedColor = new Color(colors.normalColor.r * 0.8f, colors.normalColor.g * 0.8f, colors.normalColor.b * 0.8f, 1f);
        colors.colorMultiplier = 1f;
        colors.fadeDuration = 0.1f;
        button.colors = colors;
        
        // Set button text color
        Text buttonText = button.GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            buttonText.color = Color.white;
     
        }
    }
    
    /// <summary>
    /// Apply Steam style to text
    /// </summary>
    private void ApplyTextStyle(Text text)
    {
        if (text == null) return;
        
        string textName = text.name.ToLower();
        
        if (textName.Contains("title") || textName.Contains("name"))
        {
            // Title text - bright white
            text.color = new Color(1f, 1f, 1f, 0.9f);
          
        }
        else if (textName.Contains("price"))
        {
            // Price text - bright white, slightly larger
            text.color = new Color(1f, 1f, 1f, 0.9f);
           
        }
        else if (textName.Contains("description") || textName.Contains("dev"))
        {
            // Description text - light gray
            text.color = new Color(0.545f, 0.573f, 0.604f, 1f); // #8B929A
            
        }
        else
        {
            // Default text - muted gray
            text.color = new Color(0.404f, 0.439f, 0.482f, 1f); // #67707B
          
        }
    }
    
    /// <summary>
    /// Apply Steam style to image
    /// </summary>
    private void ApplyImageStyle(Image image)
    {
        if (image == null) return;
        
        string imageName = image.name.ToLower();
        if (imageName.Contains("shadow"))
        {
            Debug.Log("Found shadow");
            return;
        }
        if (imageName.Contains("container"))
        {
  
            return;
        }

        if (imageName.Contains("card") || imageName.Contains("panel"))
        {
            // Card/Panel background - Steam store dark
            image.color = new Color(0.165f, 0.278f, 0.369f, 1f); // #2A475E
        }
        else if (imageName.Contains("background") || imageName.Contains("bg"))
        {
            // Page background - Steam darkest
            image.color = new Color(0f, 0.059f, 0.094f, 1f); // #000F18
        }
        else
        {
            // Default panel - Steam system dark
            image.color = new Color(0.239f, 0.267f, 0.314f, 0.7f); // #3D4450
        }
    }
    
    /// <summary>
    /// Clear Steam style (restore defaults)
    /// </summary>
    [ContextMenu("Clear Steam Style")]
    public void ClearSteamStyle()
    {
        Debug.Log("🧹 Clearing Steam style...");
        
        Canvas[] allCanvas = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in allCanvas)
        {
            ClearCanvasStyle(canvas);
        }
        
        Debug.Log("✅ Steam style cleared!");
    }
    
    /// <summary>
    /// Clear Canvas style
    /// </summary>
    private void ClearCanvasStyle(Canvas canvas)
    {
        Image canvasBackground = canvas.GetComponent<Image>();
        if (canvasBackground != null)
        {
            canvasBackground.color = Color.white;
        }
        
        ClearStyleRecursively(canvas.gameObject);
    }
    
    /// <summary>
    /// Recursively clear styles
    /// </summary>
    private void ClearStyleRecursively(GameObject obj)
    {
        Image image = obj.GetComponent<Image>();
        Text text = obj.GetComponent<Text>();
        Button button = obj.GetComponent<Button>();
        
        if (image != null)
        {
            image.color = Color.white;
        }
        
        if (text != null)
        {
            text.color = Color.black;
        }
        
        if (button != null)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = new Color(0.9f, 0.9f, 0.9f, 1f);
            colors.pressedColor = new Color(0.8f, 0.8f, 0.8f, 1f);
            button.colors = colors;
        }
        
        foreach (Transform child in obj.transform)
        {
            ClearStyleRecursively(child.gameObject);
        }
    }
}
