namespace PetShop.Core.Entity
{
    public class Color
    {
        public Color(string color)
        {
            this.GetColor = color;
        }

        public string GetColor { get; set; }
    }
}
