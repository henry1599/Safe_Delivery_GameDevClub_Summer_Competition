using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingShipper : MonoBehaviour
{
    public GameObject petFollowing;
    enum SHIPPERS
    {
        SHOPEE = 0,
        BEE = 1,
        GRAB = 2,
        FAST = 3
    }

    private List<Transform> Shippers = new List<Transform>();
    public List<GameObject> HealthBarShipper = new List<GameObject>();
    public List<GameObject> transformEffect;
    public List<GameObject> Lights = new List<GameObject>();
    private static SwitchingShipper instance;
    public int currentIdx;
    public float timeBtwSwitchingValue;
    private float timeBtwSwitching;
    public SwitchTiming timeBar;

    public static SwitchingShipper Instance { get => instance; private set => instance = value; }

    // Start is called before the first frame update
    void Start()
    {
        timeBtwSwitching = timeBtwSwitchingValue;
        timeBar.SetMaxValue(timeBtwSwitchingValue);
        instance = this;
        Shared.IS_ALL_DEATH = false;
        Shared.CURRENT_SHIPPER = (int)SHIPPERS.SHOPEE;
        Shared.SHIPPER_STATE = new List<bool>() { true, true, true, true };
        currentIdx = 0;
        GetAllChildren();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwSwitching >= timeBtwSwitchingValue)
        {
            LightUp();
        }
        else
        {
            LightDown();
        }
        if (Shared.IS_ENABLE_TO_SWITCH == false) return;
        if (Input.GetKeyDown(KeyCode.Z) && timeBtwSwitching >= timeBtwSwitchingValue)
        {
            if (Shared.CURRENT_SHIPPER == (int)SHIPPERS.SHOPEE) return;
            PickShipper((int)SHIPPERS.SHOPEE);
            ShareVariables.EXTRA_JUMP = ShareVariables.EXTRA_JUMP_VALUE;
        }
        else if (Input.GetKeyDown(KeyCode.X) && timeBtwSwitching >= timeBtwSwitchingValue)
        {
            if (Shared.CURRENT_SHIPPER == (int)SHIPPERS.BEE) return;
            PickShipper((int)SHIPPERS.BEE);
        }
        else if (Input.GetKeyDown(KeyCode.C) && timeBtwSwitching >= timeBtwSwitchingValue)
        {
            if (Shared.CURRENT_SHIPPER == (int)SHIPPERS.GRAB) return;
            PickShipper((int)SHIPPERS.GRAB);
        }
        else if (Input.GetKeyDown(KeyCode.V) && timeBtwSwitching >= timeBtwSwitchingValue)
        {
            if (Shared.CURRENT_SHIPPER == (int)SHIPPERS.FAST) return;
            PickShipper((int)SHIPPERS.FAST);
        }
        timeBtwSwitching += Time.deltaTime * 20;
        Mathf.Clamp(timeBtwSwitching, 0, timeBtwSwitchingValue);
        timeBar.SetValue(timeBtwSwitching);
    }
    
    void GetAllChildren()
    {
        foreach(Transform shipper in transform)
        {
            Shippers.Add(shipper);
        }
    }

    void PickShipper(int idx)
    {
        if (Shared.SHIPPER_STATE[idx] == false)
        {
            // Dev log
            Debug.Log("Cannot switch to this player, player is death");
            return;
        }
        for (int i = 0; i < Shippers.Count; i++)
        {
            if (i == idx)
            {
                currentIdx = idx;
                Instantiate(transformEffect[idx], Shippers[Shared.CURRENT_SHIPPER].gameObject.transform.position, Quaternion.identity);
                Shippers[idx].gameObject.SetActive(true);
                HealthBarShipper[idx].SetActive(true);
                Shippers[idx].gameObject.transform.position = Shippers[Shared.CURRENT_SHIPPER].gameObject.transform.position;
                Shared.CURRENT_SHIPPER = idx;
                timeBtwSwitching = 0;
                timeBar.SetValue(timeBtwSwitching);
            }
            else
            {
                HealthBarShipper[i].SetActive(false);
                Shippers[i].gameObject.SetActive(false);
            }
        }
    }

    public void SwitchImmediately()
    {
        int nextSwitchedShipper = GetAvailableShipper();
        if (nextSwitchedShipper == -1)
        {
            Shared.IS_ALL_DEATH = true;
            return;
        }
        for (int i = 0; i < Shippers.Count; i++)
        {
            if (i == nextSwitchedShipper)
            {
                currentIdx = nextSwitchedShipper;
                Instantiate(transformEffect[nextSwitchedShipper], Shippers[Shared.CURRENT_SHIPPER].gameObject.transform.position, Quaternion.identity);
                Shippers[nextSwitchedShipper].gameObject.SetActive(true);
                HealthBarShipper[nextSwitchedShipper].SetActive(true);
                Shippers[nextSwitchedShipper].gameObject.transform.position = Shippers[Shared.CURRENT_SHIPPER].gameObject.transform.position;
                Shared.CURRENT_SHIPPER = nextSwitchedShipper;
            }
            else
            {
                HealthBarShipper[i].SetActive(false);
                Shippers[i].gameObject.SetActive(false);
            }
        }
    }

    private int GetAvailableShipper()
    {
        int result = -1;
        for (int i = 0; i < Shared.SHIPPER_STATE.Count; i++)
        {
            if (Shared.SHIPPER_STATE[i] == true)
            {
                result = i;
                break;
            }
        }
        return result;
    }

    private void LightUp()
    {
        foreach (GameObject light in Lights)
        {
            light.SetActive(true);
        }
    }
    
    private void LightDown()
    {
        foreach (GameObject light in Lights)
        {
            light.SetActive(false);
        }
    }
}






