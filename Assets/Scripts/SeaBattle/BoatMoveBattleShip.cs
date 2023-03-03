using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BoatMoveBattleShip : MonoBehaviour
{
    [SerializeField] int myIndex;

    float speed = 150;
    float maxSpeed = 100;

    float rotationTime = 0.945f;

    //[SerializeField] GameObject deadEffect;
    //[SerializeField] Animator anim;
    [SerializeField] FixedJoystick Joystick;
    [SerializeField] Transform roof;

    Collider2D cl2;
    Rigidbody2D rb2;
    [SerializeField] SpriteRenderer spriteRenderer;
    bool isDead = false;

    //[SerializeField] Transform shadowByWater;
    Vector2 addShadowPos;

    private void Awake()
    {
        cl2 = GetComponent<Collider2D>();
        rb2 = GetComponent<Rigidbody2D>();
        roof = transform.GetChild(1);
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        //shadowByWater.position = transform.position + new Vector3(0.7f, -.4f, 0);
        //addShadowPos = shadowByWater.position - transform.position;
        //Joystick.gameObject.SetActive(true);

        Transform parentTransform = UIInGameController.Instances.transform;
        if (transform.parent.name == "Player1")
        {
            Joystick = parentTransform.GetChild(0).GetComponent<FixedJoystick>();
            parentTransform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(230, 180, 10);
            parentTransform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 255);
            parentTransform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 255);
            myIndex = 0;
        }
        else if (transform.parent.name == "Player2")
        {
            Joystick = parentTransform.GetChild(1).GetComponent<FixedJoystick>();
            parentTransform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector3(230, -180, 10);
            parentTransform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255, 255);
            parentTransform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 255);
            myIndex = 1;
        }
        else if (transform.parent.name == "Player3")
        {
            Joystick = parentTransform.GetChild(2).GetComponent<FixedJoystick>();
            parentTransform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector3(-240, -180, 10);
            parentTransform.GetChild(2).GetComponent<Image>().color = new Color(255, 255, 255, 255);
            parentTransform.GetChild(2).GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 255);
            myIndex = 2;
        }
        else if (transform.parent.name == "Player4")
        {
            Joystick = parentTransform.GetChild(3).GetComponent<FixedJoystick>();
            parentTransform.GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector3(-240, 180, 10);
            parentTransform.GetChild(3).GetComponent<Image>().color = new Color(255, 255, 255, 255);
            parentTransform.GetChild(3).GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 255);
            myIndex = 3;
        }
    }

    private void LateUpdate()
    {
        //shadowByWater.position = (Vector2)transform.position + addShadowPos;
    }

    void FixedUpdate()
    {
        MoveBoat();
    }

    void MoveBoat()
    {
        if (isDead) return;

        Vector2 direction = Joystick.Direction;

        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(new Vector3(0, 0, angle)), transform.rotation,
                rotationTime);

            rb2.AddForce(-transform.right * speed * Time.fixedDeltaTime);
            rb2.velocity = Vector3.ClampMagnitude(rb2.velocity, maxSpeed);

            // if (!anim.enabled)
            // {
            //     anim.enabled = true;
            // }
        }
        // else if(anim.enabled)
        // {
        //     anim.enabled = false;
        // }
    }

    void ShipWreck()
    {
        Joystick.gameObject.SetActive(false);
        //BattleSea.AudioManagerBattleSea.instance.Play(BattleSea.AudioManagerBattleSea.instance.hitShip);
        //GameManagerBattleSea2.instance.ShipWreck(myIndex);
        DShakePosition(Camera.main.transform, 0.2f, 1, 30, 40);
        //shadowByWater.gameObject.SetActive(false);
        //Instantiate(deadEffect, transform.position, Quaternion.identity);
        cl2.enabled = false;
        //anim.enabled = false;
        roof.gameObject.SetActive(false);
        rb2.velocity = Vector2.zero;
        isDead = true;

        DFadeSpriteRenderer(spriteRenderer, 0, 5, 0, DG.Tweening.Ease.OutSine, () =>
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
            Destroy(gameObject.transform.parent.gameObject);
        });
        DScale(transform, Vector3.one * 0.3f, 5, DG.Tweening.Ease.InSine);
    }

    void DShakePosition(Transform mtranform, float duration, float strength, int vibrato, float randomNess,
        bool snapping = false, bool fadeout = true, System.Action action = null)
    {
        mtranform.DOShakePosition(duration, strength, vibrato, randomNess).SetEase(Ease.InOutSine);
    }

    void DFadeSpriteRenderer(SpriteRenderer mtransform, float fadeTo, float duration, float delay, Ease eas,
        System.Action action = null)
    {
        mtransform.DOFade(fadeTo, duration).SetDelay(delay).SetEase(eas).OnComplete(() => { action?.Invoke(); });
    }

    void DScale(Transform mtransform, Vector3 scaleTo, float duration, Ease eas, System.Action action = null)
    {
        mtransform.DOScale(scaleTo, duration).SetEase(eas).OnComplete(() => { action?.Invoke(); });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
        // Instantiate(deadEffect, collision.transform.position, Quaternion.identity);
        ShipWreck();
    }
}