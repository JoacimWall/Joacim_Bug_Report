using System;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClickEventBug.Models;

public class PhotoBookV2 : ObservableObject
{
    public PhotoBookV2()
    {

    }
    #region api properties   
    public int PhotoBookId { get; set; }
    public bool HasPlacedOrder { get; set; }

    //public string Title { get; set; } This is picked up from cover front page

    public BookTemplateType BookTemplateType { get; set; }

    //public List<PhotoBookPageV2> Pages { get; set; }


    //No database Fields

    #endregion
    #region non-api properties
    [JsonIgnore]
    public int LiberoPoints { get; set; }
    //[JsonIgnore]
    public string EditAction { get; set; }
    [JsonIgnore]
    public string Title { get; set; }


    #endregion
}

public enum BookTemplateType
{
    Standard = 1
}