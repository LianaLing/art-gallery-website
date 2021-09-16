namespace ArtGalleryWebsite.Models.DTO
{
    public class ShoppingCartDTO
    {
        public int Id { get; set; }
        public int Total { get; set; }
        public int UserId { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Total: {Total}, UserId: {UserId}";
        }
    }
}