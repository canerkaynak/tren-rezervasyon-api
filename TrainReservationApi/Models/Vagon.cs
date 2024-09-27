namespace TrainReservationApi.Models
{
    public class Vagon
    {
        public required string Ad { get; set; } = string.Empty;
        public required int Kapasite { get; set; }
        public required int DoluKoltukAdet { get; set; }
    }
}
