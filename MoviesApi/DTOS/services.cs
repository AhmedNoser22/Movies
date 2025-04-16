namespace MoviesApi.DTOS
{
    public class services
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public byte[] poster { get; set; }
        public byte GenreId { get; set; }
        public string genra { get; set; }
    }
}
