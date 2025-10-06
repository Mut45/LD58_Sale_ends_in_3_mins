using UnityEngine;
using UnityEngine.UI;

public class GradeEvaluator : MonoBehaviour
{
    [Header("Grade Thresholds")]
    [SerializeField] private float minThreshold = 0f;      // D级阈值
    [SerializeField] private float cThreshold = 100f;     // C级阈值
    [SerializeField] private float bThreshold = 500f;     // B级阈值
    [SerializeField] private float aThreshold = 1000f;    // A级阈值
    // S级：大于aThreshold

    [Header("Grade Textures (5个等级对应的图片 - 支持JPG/PNG等格式)")]
    [SerializeField] private Texture2D gradeDTexture;
    [SerializeField] private Texture2D gradeCTexture;
    [SerializeField] private Texture2D gradeBTexture;
    [SerializeField] private Texture2D gradeATexture;
    [SerializeField] private Texture2D gradeSTexture;

    [Header("Grade Text Content (每个等级2个文本内容，共10个)")]
    [SerializeField] private string gradeDText1 = "D级评价";
    [SerializeField] private string gradeDText2 = "需要改进";
    [SerializeField] private string gradeCText1 = "C级评价";
    [SerializeField] private string gradeCText2 = "一般水平";
    [SerializeField] private string gradeBText1 = "B级评价";
    [SerializeField] private string gradeBText2 = "良好表现";
    [SerializeField] private string gradeAText1 = "A级评价";
    [SerializeField] private string gradeAText2 = "优秀表现";
    [SerializeField] private string gradeSText1 = "S级评价";
    [SerializeField] private string gradeSText2 = "完美表现";

    [Header("Display Objects (场景中显示的对象)")]
    [SerializeField] private Image displayImage;
    [SerializeField] private Text displayText1;
    [SerializeField] private Text displayText2;

    [Header("EndingSummaryManager Reference")]
    [SerializeField] private EndingSummaryManager endingSummaryManager;

    private void Start()
    {
        // 如果没有手动分配EndingSummaryManager，尝试自动查找
        if (endingSummaryManager == null)
        {
            endingSummaryManager = FindObjectOfType<EndingSummaryManager>();
        }

        // 延迟执行评估，确保EndingSummaryManager已经计算完成
        Invoke(nameof(EvaluateGrade), 0.1f);
    }

    private void EvaluateGrade()
    {
        if (endingSummaryManager == null)
        {
            Debug.LogError("GradeEvaluator: 找不到EndingSummaryManager！");
            return;
        }

        // 获取total值
        float total = GetTotalFromEndingSummary();
        
        // 判断等级
        GradeLevel grade = DetermineGrade(total);
        
        // 显示对应等级的内容
        DisplayGrade(grade);
        
        Debug.Log($"GradeEvaluator: Total = {total}, Grade = {grade}");
    }

    private float GetTotalFromEndingSummary()
    {
        // 通过GameLibrary获取owned games并重新计算total
        var library = GameLibrary.Instance;
        if (library == null)
        {
            Debug.LogError("GradeEvaluator: GameLibrary实例为空！");
            return 0f;
        }

        var owned = library.GetOwnedGames();
        if (owned == null || owned.Count == 0)
        {
            return 0f;
        }

        float total = 0f;
        foreach (var gameData in owned)
        {
            float currentScore = 0;
            float moneySaved = gameData.originalPrice - gameData.originalPrice * gameData.discount;
            float ratingMultiplier = RatingToMultiplier((int)gameData.rating);
            int seriesCount = 0;
            float seriesCountMultiplier = SeriesCountToMultiplier(seriesCount);
            string type = gameData.type;
            int typeCount = 0;
            foreach (var i in owned)
            {
                if (i.type == type)
                {
                    typeCount++;
                }
            }
            float typeMultiplier = TypeCountToMultiplier(typeCount);
            currentScore = moneySaved * ratingMultiplier * seriesCountMultiplier * typeMultiplier;
            total += currentScore;
        }

        return total;
    }

    private GradeLevel DetermineGrade(float total)
    {
        if (total < minThreshold)
        {
            return GradeLevel.D;
        }
        else if (total < cThreshold)
        {
            return GradeLevel.C;
        }
        else if (total < bThreshold)
        {
            return GradeLevel.B;
        }
        else if (total < aThreshold)
        {
            return GradeLevel.A;
        }
        else
        {
            return GradeLevel.S;
        }
    }

    private void DisplayGrade(GradeLevel grade)
    {
        switch (grade)
        {
            case GradeLevel.D:
                SetDisplayContent(gradeDTexture, gradeDText1, gradeDText2);
                break;
            case GradeLevel.C:
                SetDisplayContent(gradeCTexture, gradeCText1, gradeCText2);
                break;
            case GradeLevel.B:
                SetDisplayContent(gradeBTexture, gradeBText1, gradeBText2);
                break;
            case GradeLevel.A:
                SetDisplayContent(gradeATexture, gradeAText1, gradeAText2);
                break;
            case GradeLevel.S:
                SetDisplayContent(gradeSTexture, gradeSText1, gradeSText2);
                break;
        }
    }

    private void SetDisplayContent(Texture2D gradeTexture, string gradeText1, string gradeText2)
    {
        if (displayImage != null && gradeTexture != null)
        {
            // 将Texture2D转换为Sprite
            Sprite sprite = Sprite.Create(gradeTexture, new Rect(0, 0, gradeTexture.width, gradeTexture.height), new Vector2(0.5f, 0.5f));
            displayImage.sprite = sprite;
        }

        if (displayText1 != null)
        {
            displayText1.text = gradeText1;
        }

        if (displayText2 != null)
        {
            displayText2.text = gradeText2;
        }
    }

    // 复制EndingSummaryManager中的计算方法
    private float RatingToMultiplier(int rating)
    {
        if (rating == 0) return 0.01f;
        else if (rating == 1) return 0.1f;
        else if (rating == 2) return 0.3f;
        else if (rating == 3) return 0.5f;
        else if (rating == 4) return 0.7f;
        else return 1f;
    }

    private float SeriesCountToMultiplier(int count)
    {
        if (count >= 5) return 2.0f;
        else if (count >= 4) return 1.5f;
        else if (count >= 3) return 1.2f;
        else return 1f;
    }

    private float TypeCountToMultiplier(int count)
    {
        if (count >= 10) return 1.5f;
        else if (count >= 5) return 1.3f;
        else if (count >= 3) return 1.1f;
        else return 1f;
    }

    // 手动重新评估等级（可在Inspector中调用）
    [ContextMenu("Re-evaluate Grade")]
    public void ReEvaluateGrade()
    {
        EvaluateGrade();
    }
}

public enum GradeLevel
{
    D, C, B, A, S
}
