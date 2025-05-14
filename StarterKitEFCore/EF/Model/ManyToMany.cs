namespace StarterKit.EF.Model
{
    public class ManyToMany<TLeft, TRight>
    where TLeft : BaseEntity
    where TRight : BaseEntity
    {
        public ulong LeftEntityId { get; set; }
        public ulong RightEntityId { get; set; }

        public TLeft? LeftEntity { get; set; }
        public TRight? RightEntity { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj is ManyToMany<TLeft,TRight> mtm)
            {
                return LeftEntityId == mtm.LeftEntityId && RightEntityId == mtm.RightEntityId;
            }
            else if(obj is TLeft left) 
            {
                return LeftEntity == left;
            }
            else if(obj is TRight right)
            {
                return RightEntity == right;
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(LeftEntityId, RightEntityId);
        }   
    }
}
