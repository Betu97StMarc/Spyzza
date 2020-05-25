using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventary : MonoBehaviour
{

    public RawImage postItImage;
    public RawImage MugImage;
    public RawImage RedCardImage;
    public RawImage BlueCardImage;
    public RawImage GreenCardImage;

    public Texture postItTrue;
    public Texture postItFalse;

    public Texture mugTrue;
    public Texture mugFalse;
    
    public Texture RedCardTrue;
    public Texture RedCardFalse;

    public Texture BlueCardTrue;
    public Texture BlueCardFalse;

    public Texture GreenCardTrue;
    public Texture GreenCardFalse;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
       if (GameManager.Instance.mug == true)
        {
            MugImage.texture = mugTrue;
        }
       else
        {
            MugImage.texture = mugFalse;
        }
        
        if (GameManager.Instance.postIt == true)
        {
            postItImage.texture = postItTrue;
        }
       else
        {
            postItImage.texture = postItFalse;
        }
        
        if (GameManager.Instance.redCard == true)
        {
            RedCardImage.texture = RedCardTrue;
        }
       else
        {
            RedCardImage.texture = RedCardFalse;
        }
        
        if (GameManager.Instance.blueCard == true)
        {
            BlueCardImage.texture = BlueCardTrue;
        }
       else
        {
            BlueCardImage.texture = BlueCardFalse;
        }
        
        if (GameManager.Instance.greenCard == true)
        {
            GreenCardImage.texture = GreenCardTrue;
        }
       else
        {
            GreenCardImage.texture = GreenCardFalse;
        }

        
    }
}
