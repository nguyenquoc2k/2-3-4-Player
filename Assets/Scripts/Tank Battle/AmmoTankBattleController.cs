using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoTankBattleController : MonoBehaviour
{
    public int maxBullets = 5;  // Số lượng viên đạn tối đa của người chơi
    public float reloadTime = 3f;  // Thời gian để nạp lại đạn (giây)
    public int currentBullets;  // Số lượng viên đạn hiện có của người chơi
    public bool reloading;  // Biến kiểm tra người chơi đang nạp đạn hay không
    public static event Action<string, int> HandleAmmoEvent;
    private void Awake()
    {
        SetAmountBullet();
        HandleAmmoEvent?.Invoke(transform.name, currentBullets);
    }

    public void SetAmountBullet()
    {
        currentBullets = 5;
        DispatchNumberAmmo();
    }
    private void Update()
    {
        // Kiểm tra xem người chơi đang trong quá trình nạp đạn hay không
        if (reloading)
        {
            // Giảm thời gian nạp đạn
            reloadTime -= Time.deltaTime;

            // Nếu thời gian nạp đạn kết thúc
            if (reloadTime <= 0f)
            {
                // Nạp đạn
                currentBullets++;
                DispatchNumberAmmo();
                reloading = false;
            }
        }

        // Kiểm tra xem người chơi có đủ 5 viên đạn hay không để ngừng quá trình nạp đạn
        if (currentBullets >= maxBullets)
        {
            reloading = false;
        }
        // Nếu người chơi có ít hơn 5 viên đạn, nạp đạn và vẫn có thể bắn
        else if (currentBullets > 0 && currentBullets < maxBullets && !reloading)
        {
            StartReloading();
        }
        // Nếu người chơi không có đạn thì chỉ nạp đạn
        else if (currentBullets <= 0 && !reloading)
        {
            StartReloading();
        }
    }

    public void DispatchNumberAmmo()
    {
        HandleAmmoEvent?.Invoke(transform.name, currentBullets);
    }
    public void StartReloading()
    {
        // Thiết lập cờ hiệu đang nạp đạn và reset thời gian nạp đạn
        reloading = true;
        reloadTime = 3f;
    }
}
