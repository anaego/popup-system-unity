using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PopupData 
{
    // TODO make readonly maybe?
    public string PopupTitle { get; set; }
    public string PopupContent { get; set; }
    public string ButtonText { get; set; }
    public string BackgroundImageUrl { get; set; }
    public string ButtonImageUrl { get; set; }
    public Action ButtonAction { get; set; }
    public bool IsContentVisible { get; set; }
    public bool IsButtonVisible { get; set; }
}
