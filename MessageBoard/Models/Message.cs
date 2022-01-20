using System;
using System.ComponentModel.DataAnnotations;


namespace MessageBoard.Models
{
  public class Message
  {
    public int GroupId { get; set; }
    public int EndUserId { get; set; }
    public int MessageId { get; set; }
    public string Post { get; set; }
    [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }
    public virtual Group Group { get; set; }
    public virtual EndUser EndUser { get; set; }
  }
}
