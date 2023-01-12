namespace Football.Domain.Entities.Commons;
public abstract class AudiTable
{
    public Guid Id { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdateAt { get; set; }
}
