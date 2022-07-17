using SecondHand.Models.Advertisement;

namespace SecondHand.Web.ViewModel
{
    public class CreateAdViewModel
    {
        public List<Category>? Categories{ get; set; }
        public List<Product>? Products { get; set; }
        public List<Mark>? Marks { get; set; }
    }
}