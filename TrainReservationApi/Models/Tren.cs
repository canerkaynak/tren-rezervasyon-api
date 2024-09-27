namespace TrainReservationApi.Models
{
    public class Tren
    {
        public required string Ad { get; set; }
        public required List<Vagon> Vagonlar { get; set; }
    }
}
