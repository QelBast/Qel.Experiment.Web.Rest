using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Qel.Ef.Models.Bases;

public class BaseEntity<T>
{
    public T? Id { get; set; }
}