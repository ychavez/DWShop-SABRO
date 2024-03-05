namespace DWShop.Domain.Contracts
{
    public interface IAuditableEntity: IEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set;}
    }

    public interface IAuditableEntity<T> : IAuditableEntity, IEntity<T> { }
}
