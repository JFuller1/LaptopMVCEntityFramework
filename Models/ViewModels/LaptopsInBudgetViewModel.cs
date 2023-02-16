namespace LaptopMVCEntityFramework.Models.ViewModels
{
    public class LaptopsInBudgetViewModel
    {
        public int Budget { get; set; }

        public List<Laptop> ValidLaptops = new List<Laptop>();
    }
}
