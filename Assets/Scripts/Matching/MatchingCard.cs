using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Đặt tên là matching card để khỏi lẫn với card của game kahsc
/// </summary>
public class MatchingCard : MonoBehaviour
{
    RectTransform rectTransform;
    // Một cái image, 1 game object là quá đủ
    public Image cardImage;
    [SerializeField] float animDuration = 0.5f;
    /// <summary>
    /// Này là cái lưng lá bài nhé
    /// </summary>
    public Sprite defaultImage;
    /// <summary>
    /// Bình thường, nếu game cần độ chính xác cao thì chỗ này sẽ giữ data của card, bao gồm id của card và sprite. Nhưng game đơn giản thì thôi, so sánh tham chiếu sprite là được.
    /// </summary>
    public Sprite holdingImage { get; private set; }
    MatchingGameController gameController;

    public bool isFlipping { get; private set; } = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        cardImage.sprite = defaultImage;
    }

    public void InitData(Sprite cardImage, MatchingGameController controller)
    {
        holdingImage = cardImage;
        gameController = controller;
    }

    public bool Flip(bool isBackward)
    {
        if (isFlipping) Debug.LogWarning("aaa");
        isFlipping = true;
        if (!isBackward)
        gameController.OnCardFlipUp(this);
        // Vì là 2d nên thực tế chỉ cần lật 90 độ xong lật lại thôi
        rectTransform.DORotate(new Vector3(0, 90, 0), animDuration / 2).OnComplete(() =>
        {
            // xong thì đổi hình và lật lại
            cardImage.sprite = isBackward ? defaultImage : holdingImage;
            rectTransform.DORotate(Vector3.zero, animDuration / 2).OnComplete(() =>
            {
                gameController.OnCardFlipDone(this,isBackward);
                isFlipping = false;
            });
        });
        return true;
    }

    /// <summary>
    /// Cái này nên được call từ button hơn, vì nó là 1 UI element, như thế sẽ dễ dàng cho việc controll trong tương lai xa lẫn gần, đừng làm như kiểu nãy ông làm
    /// </summary>
    public void ClickFlip()
    {
        if (isFlipping) return;
        // và vì cái này là view, ta sẽ cần đẩy logic sang controller
        gameController.OnFlip(this);
    }



}
