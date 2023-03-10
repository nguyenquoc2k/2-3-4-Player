using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// Class này sẽ phải control việc 1 lá được lật hay ko luôn
/// </summary>
public class MatchingGameController : MonoBehaviour
{
    /// <summary>
    /// List thể hiện của các card.
    /// Cái list này thích tạo ra kiểu éo gì cũng được, thậm chí gen luôn ra bằng code cho nó pro vip cũng được. Tạm thời tôi sẽ kéo bên ngoài
    /// </summary>
    public List<MatchingCard> cards;

    /// <summary>
    /// List này thì định nghĩa các cái hình sẽ có.
    /// </summary>
    public List<Sprite> spriteList;

    /// <summary>
    /// Cái này chỉ để phục vụ cho vieehc code bot
    /// </summary>
    public List<MatchingCard> showedCards = new List<MatchingCard>();

    /// <summary>
    /// Các card đang lật
    /// </summary>
    public List<MatchingCard> flippingCard = new List<MatchingCard>();

    int currentTurnIndex = 0;

    /// <summary>
    /// Cái này là số người chơi là con người.
    /// </summary>
    public int playerCount;

    public List<Color> listColor = new List<Color>();
    public SpriteRenderer backGround;
    public static event Action<int> IncreaseScore;
    public static event Action HandleEndGame;
    bool isPlayerTurn => currentTurnIndex % 4 < playerCount;
    public bool isDoneGame { get; private set; } = false;

    private void Awake()
    {
        if (SelectAmountPlayer.Instances.option1 == true)
        {
            playerCount = 1;
        }
        else if (SelectAmountPlayer.Instances.option2 == true)
        {
            playerCount = 2;
        }
        else if (SelectAmountPlayer.Instances.option3 == true)
        {
            playerCount = 3;
        }
        else
        {
            playerCount = 4;
        }

        InitGame();
    }


    /// <summary>
    /// Khởi tạo game mới, gán sprite cho card
    /// </summary>
    public void InitGame()
    {
        // đầu tiên là phải test và đảm bảo là số card ko lẻ
        if (cards.Count % 2 != 0)
        {
            Debug.LogError("Kéo thiếu rồi!");
            return;
        }

        currentTurnIndex = 0;

        isDoneGame = false;
        showedCards.Clear();
        flippingCard.Clear();
        // Tạo 1 list mới từ list cũ để gán sprite
        var notInitedList = new List<MatchingCard>(cards);
        // đây là cách để khởi tạo 1 cách chắn chắn  các cặp card dựa trên spriteList. Muốn suffle thì suffle spriteList là xong
        int currentSpriteIndex = 0;
        while (notInitedList.Count > 0)
        {
            int cardIndex = Random.Range(0, notInitedList.Count);
            notInitedList[cardIndex].InitData(spriteList[currentSpriteIndex], this);
            if (notInitedList.Count % 2 != 0) // nếu mà list đang lẻ thì hiểu là đang gán cho cái còn lại của cặp card
            {
                currentSpriteIndex++;
                if (currentSpriteIndex >= spriteList.Count)
                {
                    currentSpriteIndex = 0;
                }
            }

            notInitedList.RemoveAt(cardIndex);
        }

        backGround.gameObject.SetActive(true);
        backGround.color = listColor[currentTurnIndex];
        // thế là init xong nhé
    }

    /// <summary>
    /// Chỗ này là business logic của việc flip card
    /// </summary>
    public void OnFlip(MatchingCard card)
    {
        if (!isPlayerTurn) return;
        if (flippingCard.Count < 2 && !flippingCard.Contains(card))
        {
            card.Flip(false); // xong rồi thì controller mới điều khiển view show ra. Cái này là quan trọng nếu muốn code dạng MVC
        }
    }

    public void BotFlip(MatchingCard card)
    {
        if (flippingCard.Count < 2 && !flippingCard.Contains(card) && !card.isFlipping)
        {
            card.Flip(false); // xong rồi thì controller mới điều khiển view show ra. Cái này là quan trọng nếu muốn code dạng MVC
        }
        else
        {
            Debug.LogError("asd");
        }
    }

    /// <summary>
    /// Method này để check xem là 2 lá được lật có giống nhau ko
    /// </summary>
    void MatchCheck()
    {
        Debug.Log(flippingCard.Count);
        if (flippingCard.Count != 2) return;
        var backupList = new List<MatchingCard>(flippingCard);
        if (flippingCard[0].holdingImage == flippingCard[1].holdingImage)
        {
            Debug.Log("ok");
            SetTimeout(() =>
            {
                // đây là nếu hai sprite của 2 cái này giống nhau. Ông có thể custom khác đi nêu smuoons
                LamCaiEoGiDoNeuHaiCaiGiongNhau(backupList);
                // clear khỏi showdCard
                IncreaseScore?.Invoke(currentTurnIndex % 4);
                backupList.ForEach(x => showedCards.Remove(x));
                BotMoving();
            }, 0.3f);
        }
        else
        {
            SetTimeout(() =>
            {
                // còn nếu ko giống thì chỉ úp xuống
                backupList.ForEach(x => x.Flip(true));
                Debug.Log("false");
            }, 0.3f);
            currentTurnIndex++;
            backGround.color = listColor[currentTurnIndex % 4];
            TurnUpdate();
        }

        flippingCard.Clear();
        Invoke("CheckAmountCardInList",0.5f);
        
    }

    void CheckAmountCardInList()
    {
        if (!cards.Exists(x => x.gameObject.activeInHierarchy))
        {
            LamCaiEoGiDoNeuHet();
        }
    }
    /// <summary>
    /// ông thích làm cái đéo gì ở đây cũng được, cộng điểm hoặc làm gì đó. Tạm thời tôi sẽ cho nó biến mất.
    /// </summary>
    void LamCaiEoGiDoNeuHaiCaiGiongNhau(List<MatchingCard> cards)
    {
        cards.ForEach(x => x.gameObject.SetActive(false));
    }

    /// <summary>
    /// Method này làm 1 cái gì đó nếu không còn cặp nào
    /// </summary>
    void LamCaiEoGiDoNeuHet()
    {
        HandleEndGame?.Invoke();
        isDoneGame = true;
        Debug.Log("het game");
    }

    /// <summary>
    /// Bắt sự kiện card được lật xong
    /// </summary>
    public void OnCardFlipDone(MatchingCard card, bool isBackward)
    {
        if (isBackward)
        {
            // còn nếu là úp xuống thì kệ mẹ nó.
            // À ko, đổi turn chứ?
        }
        else
        {
            // Nếu là được lật lên thì check xem có match ko
            if (flippingCard.Count > 1 && card == flippingCard[1])
                MatchCheck();
        }
    }

    public void OnCardFlipUp(MatchingCard card)
    {
        Debug.Log("onFlip");
        flippingCard.Add(card);
        if (!showedCards.Contains(card))
            showedCards.Add(card);
    }

    public void TurnUpdate()
    {
        // show ra một cái thông báo cho người chơi nó biết là turn vừa thay đổi hoặc là làm cái éo gì đó tương tự.

        BotMoving();
    }

    Coroutine botMove;

    /// <summary>
    /// Đoạn này là cho bot nó chơi. Tôi ko tách ra vì copy từng class sang bên ông sẽ mệt đấy. Triển khai như nào thì tùy
    /// </summary>
    void BotMoving()
    {
        if (botMove != null)
            StopCoroutine(botMove);
        // nếu là người chơi bình thường, phải xem là có lá nào trong 2 lá ban đầu 
        if (!isPlayerTurn && !isDoneGame)
            botMove = StartCoroutine(IBotMoving());
    }

    /// <summary>
    /// Cái này để cho chân thực XD
    /// </summary>
    /// <returns></returns>
    IEnumerator IBotMoving()
    {
        yield return new WaitForSeconds(1f); // bot sẽ hành động mỗi 0.5s
        if (flippingCard.Count >= 2 || isDoneGame)
        {
            yield break;
        } // tôi ngu vl -_- Đoạn này nếu được thì ông cố cho nó ghép chung logic check flip với player nhá 

        if (flippingCard.Count == 0)
        {
            Debug.Log("Card 0");
            // nếu chưa lật lá nào, check xem từng lộ ra 2 lá giống nhau chưa, nếu có thì ăn luôn
            var canWinList = showedCards.GroupBy(x => x.holdingImage) // nhóm theo holdingImage
                .Where(x => x.Count() > 1) // rồi lấy ra nhóm có trên 2 cái
                .SelectMany(x => x).ToList();
            if (canWinList.Count > 0) // nếu có
            {
                // ăn luôn
                var readyFlip = canWinList.Find(x => !flippingCard.Contains(x));
                if (readyFlip != null)
                    BotFlip(readyFlip);
            }
            else
            {
                // nếu là bot trung bình, khi không có thì cho ngẫu nhiên 1 lá. 
                // sửa đoạn này thành chọn một lá mới (nếu trên 50% đã show) hoặc 2 lá cũ (nếu dưới 50%) để tạo ra bot hard
                RandomFlip();
            }

            BotMoving();
        }
        else
        {
            Debug.Log("Card 1");
            // nếu là lá thứ 2 rồi thì check xem là có lá nào giống lá trước ko, đương nhiên là trong mấy cái đã show, ko thì cheat quá
            MatchingCard second = showedCards.Find(x =>
                x != flippingCard[0] && x.gameObject.activeInHierarchy &&
                x.holdingImage == flippingCard[0].holdingImage);
            if (second != null)
            {
                BotFlip(second);
            }
            else
            {
                RandomFlip();
            }
        }
    }

    void RandomFlip()
    {
        Debug.Log("Random");
        MatchingCard card;
        do
        {
            card = cards[Random.Range(0, cards.Count)];
            if (isDoneGame || !cards.Exists(x => x.gameObject.activeInHierarchy)) break;
        } while (flippingCard.Contains(card) || !card.gameObject.activeInHierarchy ||
                 card.isFlipping); // điều kiện hiện tại là miễn nó đang bật

        BotFlip(card);
    }

    void SetTimeout(Action action, float time)
    {
        StartCoroutine(ITimeOut(action, time));
    }

    /// <summary>
    /// Nếu được thì viết vô common. Cái này là setTimeout đấy
    /// </summary>
    IEnumerator ITimeOut(Action action, float time)
    {
        yield return new WaitForSeconds(time);
        action();
    }
}