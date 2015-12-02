
namespace LoginTestApp.Business.Contracts.Models
{
    public class Module : ModelBase<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}
