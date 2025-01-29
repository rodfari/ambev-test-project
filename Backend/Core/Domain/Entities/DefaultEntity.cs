using System.Linq.Expressions;
using Core.Domain.Contracts;
using Core.Domain.Entities;

namespace Core.Domain.Entities;

public abstract class DefaultEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime DeletedAt { get; set; }
    public string DeletedBy { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
}