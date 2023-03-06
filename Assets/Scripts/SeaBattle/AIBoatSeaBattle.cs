using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AIBoatSeaBattle : MonoBehaviour
{
    [SerializeField] int myIndex;

    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    [SerializeField] float rotationSpeed;


    //[SerializeField] GameObject deadEffect;
    //[SerializeField] Transform shadowByWater;
    //[SerializeField] Animator anim;
    [SerializeField] Transform roof;
    [SerializeField] SpriteRenderer spriteRenderer;

    Coroutine turnCoroutine;

    Collider2D cl2;
    Rigidbody2D rb2;

    Vector2 addShadowPos;

    bool isDead = false;

    void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
        cl2 = GetComponent<Collider2D>();

        //shadowByWater.position = transform.position + new Vector3(0.7f, -.4f, 0);
        //addShadowPos = shadowByWater.position - transform.position;
    }


    void FixedUpdate()
    {
        if (isDead) return;
        rb2.AddForce(transform.right * speed * Time.fixedDeltaTime);
        rb2.velocity = Vector3.ClampMagnitude(rb2.velocity, maxSpeed);
    }

    private void LateUpdate()
    {
        //shadowByWater.position = (Vector2)transform.position + addShadowPos;
    }


    IEnumerator TurnAway(Vector2 contactNomal)
    {
        float time = Random.Range(0.3f, 1);

        Vector2 turnDirection = contactNomal;
        while (time > 0)
        {
            yield return new WaitForFixedUpdate();

            time -= Time.deltaTime;

            float angle = Mathf.Atan2(turnDirection.y, turnDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(new Vector3(0, 0, angle)), transform.rotation,
                rotationSpeed);
        }
    }

    void ShipWreck()
    {
        if (turnCoroutine != null) StopCoroutine(turnCoroutine);
        SeaBattleMap2Controller.Instances.SetInfoDeathPlayer(myIndex);
        // BattleSea.AudioManagerBattleSea.instance.Play(BattleSea.AudioManagerBattleSea.instance.hitShip);
        //GameManagerBattleSea2.instance.ShipWreck(myIndex);
        DShakePosition(Camera.main.transform, 0.2f, 1, 30, 40);
        //shadowByWater.gameObject.SetActive(false);
        //Instantiate(deadEffect, transform.position, Quaternion.identity);
        cl2.enabled = false;
       // anim.enabled = false;
        roof.gameObject.SetActive(false);
        rb2.velocity = Vector2.zero;
        isDead = true;

        DFadeSpriteRenderer(spriteRenderer, 0, 5, 0, DG.Tweening.Ease.OutSine, () =>
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
            gameObject.SetActive(false);
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
        //Instantiate(deadEffect, collision.transform.position, Quaternion.identity);
        ShipWreck();
    }


    float lastTimeRotation = 0;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (lastTimeRotation + 1.2f > Time.time) return;

        lastTimeRotation = Time.time;
        turnCoroutine = StartCoroutine(TurnAway(collision.contacts[0].normal));
    }
}