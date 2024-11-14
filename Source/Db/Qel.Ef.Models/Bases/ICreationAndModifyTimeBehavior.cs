namespace Qel.Ef.Models.Bases;

public interface ICreationAndModifyTimeBehavior
{
    public DateTime? CreationTime { get; set; }
    public DateTime? ModifyTime { get; set; }
}