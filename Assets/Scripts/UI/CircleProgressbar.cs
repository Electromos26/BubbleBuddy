using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CircleProgressbar : MonoBehaviour
{
    [SerializeField] private float indicatorTimer = 0f;
    [SerializeField] private float maxIndicatorTimer = 1f;
    [SerializeField] private float autoFillSpeed = 0.1f;
    [SerializeField] private float keyHoldFillSpeed = 0.5f;

    [SerializeField] private Image indicatorImageUI;
    [SerializeField] private KeyCode selectedKey = KeyCode.Mouse0;
    [SerializeField] private UnityEvent onIndicator;

    private bool shouldUpdate = false;

    void Awake()
    {
        indicatorImageUI.fillAmount = 0f;
    }

    void Update()
    {
        if(!enabled) return;
        if (Input.GetKey(selectedKey))
        {
            shouldUpdate = false;
            indicatorTimer += Time.deltaTime * keyHoldFillSpeed;
        }
        else
        {
            indicatorTimer += Time.deltaTime * autoFillSpeed;
        }

        indicatorImageUI.enabled = true;
        indicatorImageUI.fillAmount = indicatorTimer / maxIndicatorTimer;

        if (indicatorTimer >= maxIndicatorTimer)
        {
            indicatorTimer = 0f;
            indicatorImageUI.fillAmount = 0f;
            indicatorImageUI.enabled = false;
            onIndicator.Invoke();
        }

        if (Input.GetKeyUp(selectedKey))
        {
            shouldUpdate = true;
        }
    }
}