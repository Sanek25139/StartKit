namespace StarterKit.EF.Model
{
    public abstract class ManyToMany<TLeft, TRight>
    where TLeft : BaseEntity
    where TRight : BaseEntity
    {
        public ulong LeftEntityId { get; set; }
        public ulong RightEntityId { get; set; }

        public TLeft? LeftEntity { get; set; }
        public TRight? RightEntity { get; set; }
    }
}
