namespace TrainReservationApi.Models
{
    public class Tren
    {
        public required string Ad { get; set; } = string.Empty;
        public required List<Vagon> Vagonlar { get; set; } = new List<Vagon>();
    }
}
