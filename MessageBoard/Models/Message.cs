using System;

namespace MessageBoard.Models
{
  public class Message
  {
    public int MessageId { get; set; }
    public string Post { get; set; }
    public DateTime Date { get; set; }
  }
}