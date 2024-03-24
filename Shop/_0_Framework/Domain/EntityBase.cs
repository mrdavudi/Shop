namespace _0_Framework.Domain
{
    public class EntityBase
    {
        public long Id { get; private set; }
        public DateTime CreatetionDateTime { get; private set; }

        public EntityBase()
        {
            CreatetionDateTime = DateTime.Now;
        }
    }
}
