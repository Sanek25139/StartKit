namespace StarterKit.EF.Model
{
    public class ManyToMany<T1,T2> where T1 : BaseEntity where T2 : BaseEntity
    {
        public T1? Entity1 { get; set; }
        public ulong Entity1Id { get; set; }

        public T2? Entity2 { get; set; }
        public ulong Entity2Id { get; set; }
    }
}
