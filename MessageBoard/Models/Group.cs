using System.Collections.Generic;

namespace MessageBoard.Models

{
  public class Group
  {
    public Group()
    {
      this.JoinEntities = new HashSet<Message>();
    }
    public int GroupId { get; set;}
    public string GroupName { get; set; }
    public string Description { get; set; }
    public virtual ICollection<Message> JoinEntities { get; set; }
  }
}