using System.Collections.Generic;

namespace MessageBoard.Models
{
  public class EndUser
  {
    public EndUser()
    {
      this.JoinEntities = new HashSet<Message>();
    }
    public int EndUserId { get; set; }
    public string Name { get; set; }
    public string Language { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<Message> JoinEntities { get; }
  }
}