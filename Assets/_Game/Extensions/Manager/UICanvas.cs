using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField] private bool destroyOnClose = false;

    //call first before canvas actived
    public virtual void SetUp()
    { }

    //call after canvas active
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    //close canvas after (time)
    public virtual void Close(float time)
    {
        Invoke(nameof(CloseDirectly), time);
    }

    //close canvas directly
    public virtual void CloseDirectly()
    {
        if (destroyOnClose)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

